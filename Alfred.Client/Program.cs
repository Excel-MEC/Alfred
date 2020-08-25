using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Alfred.Client.Data;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Helpers;
using Alfred.Client.Models.Components;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;
using AutoMapper;
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
            builder.Services.AddFileReaderService(options => { options.UseWasmSharedBuffer = true; });
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IStateService, StateService>();
            services.AddSingleton<IHighlightRepository, HighlightRepository>();
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IEventHeadRepository, EventHeadRepository>();
            services.AddSingleton<NotificationService>();
            services.AddScoped<DialogService>();
            services.AddSingleton<ICustomNotification, CustomNotification>();
            services.AddTransient(sp => new HttpClient());
            services.AddAutoMapper(opt => { opt.AddProfile(new AutoMapperProfiles()); });
        }
    }
}