using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			using IHost host = CreateHostBuilder(args).Build();

			var myClass = host.Services.GetService<MyClass>();

			Console.WriteLine(myClass.GetValue());

			// await host.RunAsync();
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) => 
				{
					var configuration = context.Configuration;

					services.Configure<MyOptions>(configuration.GetSection(nameof(MyOptions)));

					services.AddSingleton<MyClass>();
				});
	}
}
