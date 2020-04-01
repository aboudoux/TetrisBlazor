using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorAnimation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WasmApp {
	public class Program {
		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddBaseAddressHttpClient();
			builder.Services.Configure<AnimationOptions>(Guid.NewGuid().ToString(),c=>{});

			await builder.Build().RunAsync();
		}
	}
}
