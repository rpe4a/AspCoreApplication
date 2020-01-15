using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleApp.Middleware;
using SampleApp.MiddlewareExtensions;

namespace SampleApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/",
            //        context => context.Response.WriteAsync($"Application name: {env.ApplicationName}"));
            //});

            //app.Map("/home", home =>
            //{
            //    home.Map("/index", Index);
            //    home.Map("/about", About);
            //});

            //app.UseToken("12345");
            //app.Run(async context => { await context.Response.WriteAsync("Page Not Found"); });

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<RoutingMiddleware>();

        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("Index"); });
        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("About"); });
        }
    }
}