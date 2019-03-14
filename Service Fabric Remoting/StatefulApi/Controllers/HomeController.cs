using ActoreNetCore.Interfaces;
using ActorNetFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;

namespace StatefulApi.Controllers
{
	[Route("")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		public async void Index()
		{
			var actorNetFrameworkProxy = ActorProxy.Create<IActorNetFramework>(new ActorId(Guid.NewGuid()));
			var actorNetCoreProxy = ActorProxy.Create<IActoreNetCore>(new ActorId(Guid.NewGuid()));

			try
			{
				await actorNetFrameworkProxy.DoSomething(new FrameworkMessage { Content = "message", Index = 1 });
			}
			catch (Exception ex)
			{
			}

			try
			{
				await actorNetCoreProxy.DoSomethingElse(new CoreMessage { Content = "message", Index = 1 });
			}
			catch (Exception ex)
			{
			}

		}
	}
}
