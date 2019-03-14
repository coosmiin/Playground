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
			var actorProxy = ActorProxy.Create<IActorNetFramework>(new ActorId(Guid.NewGuid()));

			try
			{
				await actorProxy.DoSomething();
			}
			catch (Exception ex)
			{
			}
		}
	}
}
