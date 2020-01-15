using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
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
            //env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/error");
            //}

            //app.Map("/error",
            //    builder => builder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Divide by zero!").ConfigureAwait(false);
            //    }));


            //app.Run(async (context) =>
            //{
            //    int x = 0;
            //    int y = 8 / x;
            //    await context.Response.WriteAsync($"Result = {y}");
            //});

            //app.UseStatusCodePages();

            app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync($"Err: {context.Request.Query["code"]}");
            }));

            app.Map("/hello", ap => ap.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello ASP.NET Core");
            }));
        }
    }
}