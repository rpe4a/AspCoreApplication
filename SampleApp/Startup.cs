using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
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
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var myRouteHandler = new RouteHandler(Handle);
            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            routeBuilder.MapRoute("default", "{action=Index}/{name}-{year}");
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new
                {
                    action = new CompositeRouteConstraint(new IRouteConstraint[]
                    {
                        new AlphaRouteConstraint(),
                        new MinLengthRouteConstraint(5)
                    })
                });
            routeBuilder.MapRoute("default", "{controller:regex(^H.*)}/{action}/{id?}");
            routeBuilder.MapRoute("default", "{controller:length(4)}/{action:alpha}/{id:range(4,100)}");
            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action:alpha:minlength(5)}/{id?}",
                defaults: new { controller = "Home", action = "Index" });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private async Task Handle(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var action = routeValues["action"].ToString();
            var name = routeValues["name"].ToString();
            var year = routeValues["year"].ToString();
            await context.Response.WriteAsync($"action: {action} | name: {name} | year:{year}");
        }
    }
}