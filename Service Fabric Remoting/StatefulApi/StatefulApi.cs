﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;

namespace StatefulApi
{
	/// <summary>
	/// The FabricRuntime creates an instance of this class for each service type instance. 
	/// </summary>
	internal sealed class StatefulApi : StatefulService
	{
		public StatefulApi(StatefulServiceContext context)
			: base(context)
		{ }

		/// <summary>
		/// Optional override to create listeners (like tcp, http) for this service instance.
		/// </summary>
		/// <returns>The collection of listeners.</returns>
		protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
		{
			return new ServiceReplicaListener[]
			{
				new ServiceReplicaListener(serviceContext =>
				// TODO: document "ServiceEndpoint" - see ServiceManifest Endpoints
					new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
					{
						ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

						return new WebHostBuilder()
									.UseKestrel()
									.ConfigureServices(
										services => services
											.AddSingleton<StatefulServiceContext>(serviceContext)
											.AddSingleton<IReliableStateManager>(this.StateManager))
									.UseContentRoot(Directory.GetCurrentDirectory())
									.UseStartup<Startup>()
									.UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
									.UseUrls(url)
									.Build();
					}))
			};
		}
	}
}
