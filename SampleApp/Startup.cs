using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using SampleApp.Middleware;
using SampleApp.MiddlewareExtensions;
using SampleApp.Model;
using SampleApp.ServiceExtensions;
using SampleApp.Services;
using SampleApp.Settings;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"setting1", "true"}
                })
                .AddJsonFile("config.json")
                .AddConfiguration(configuration)
                .Build();
        }

        public IConfiguration AppConfiguration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>((p) => AppConfiguration);
            services.AddSingleton<Config>((p) => AppConfiguration.Get<Config>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var
                routeBuilder = new RouteBuilder(app);

            routeBuilder.MapRoute("{controller}/{action}",
                async context => {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("�������������� ������");
                });


            routeBuilder.MapRoute("{controller}/{action}/{id}",
                async context => {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("�������������� ������");
                });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}