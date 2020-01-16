using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleApp.Middleware;
using SampleApp.MiddlewareExtensions;
using SampleApp.ServiceExtensions;
using SampleApp.Services;

namespace SampleApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<ICounter, RandomCounter>();
            //services.AddTransient<CounterService>();

            //services.AddScoped<ICounter, RandomCounter>();
            //services.AddScoped<CounterService>();

            services.AddSingleton<ICounter, RandomCounter>();
            services.AddSingleton<CounterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CounterMiddleware>();

            app.Run(async (context) =>
            {
                //�� ������������� ��� ������, �� �����
                var messageSender = context.RequestServices.GetService<IMessageSender>();

                messageSender = app.ApplicationServices.GetService<IMessageSender>();

                await context.Response.WriteAsync(messageSender.Send())
                    .ConfigureAwait(false);
            });
        }
    }
}