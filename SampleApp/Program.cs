using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntitiesLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class Program
    {
        private static IHost _host;

        public static void Main(string[] args)
        {
            _host = CreateHostBuilder(args).Build();

            using (var scope = _host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    DbInitialize.Initialize(services.GetService<AppDbContext>());
                }
                catch (Exception e)
                {
                    var log = services.GetService<ILogger<Program>>();

                    log.LogError(e, "Can't initialize DB.");
                }
            }

            _host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(s => { s.AddSingleton<IHostedService, LifetimeEventsHostedService>(); })
                .ConfigureLogging(context =>
                {
                    context.ClearProviders();
                    context.AddConsole();
                })
                .ConfigureAppConfiguration((context, builder) => { })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIIS();
                    webBuilder.ConfigureLogging(config => { config.SetMinimumLevel(LogLevel.Trace); });
                });
    }
}