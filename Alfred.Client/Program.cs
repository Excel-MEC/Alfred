using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Alfred.Client.Data;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Radzen;
using Tewr.Blazor.FileReader;

namespace Alfred.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddFileReaderService(options =>
            {
                options.UseWasmSharedBuffer = true;
            });

            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IConstantRepository, ConstantRepository>();

            builder.Services.AddTransient(sp => new HttpClient());
            builder.Services.AddSingleton<NotificationService>();
            builder.Services.AddScoped<DialogService>();


            await builder.Build().RunAsync();
        }
    }
}