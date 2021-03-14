using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

namespace BlazorPL.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("BlazorPL.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorPL.ServerAPI"));
            builder.Services.AddHttpClient("BlazorPL.PublicServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            //builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            //builder.Services.AddOptions();
            builder.Services.AddApiAuthorization();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
