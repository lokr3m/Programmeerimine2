using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KooliProjekt.BlazorApp;

namespace KooliProjekt.BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7136/api/") });
            builder.Services.AddScoped<IApiClient, ApiClient>();

            await builder.Build().RunAsync();
        }
    }
}