using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace StatefulApi
{
	/// <summary>
	/// An instance of this class is created for each service replica by the Service Fabric runtime.
	/// </summary>
	internal sealed class StatefulApi : StatefulService
	{
		TaskCompletionSource<DemoQueue> _demoQueueCompletionSource = new TaskCompletionSource<DemoQueue>();

		public StatefulApi(StatefulServiceContext context)
			: base(context)
		{ }

		protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
		{
			return new ServiceReplicaListener[]
			{
				new ServiceReplicaListener(serviceContext =>
					new KestrelCommunicationListener(serviceContext, (url, listener) =>
					{
						var host = 
							new WebHostBuilder()
								.UseKestrel()
								.ConfigureServices(
									 services => services
										 .AddSingleton<StatefulServiceContext>(serviceContext)
										 .AddSingleton<IReliableStateManager>(this.StateManager)
										 .AddSingleton<DemoQueue>())
								.UseContentRoot(Directory.GetCurrentDirectory())
								.UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
								.UseStartup<Startup>()
								.UseUrls(url)
								.Build();

						_demoQueueCompletionSource.TrySetResult(host.Services.GetService<DemoQueue>());

						return host;
					}))
			};
		}

		protected override async Task RunAsync(CancellationToken token)
		{
			try
			{
				var demoQueue = await _demoQueueCompletionSource.Task;

				while (!token.IsCancellationRequested)
				{
					await demoQueue.ProcessQueueAsync(token);
				}
			}
			finally
			{
				_demoQueueCompletionSource = new TaskCompletionSource<DemoQueue>();
			}
		}
	}
}
