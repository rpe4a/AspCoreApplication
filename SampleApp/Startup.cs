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
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<MessageService>();
            services.AddTimeService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            MessageService messageService,
            TimeService timeService)
        {
            //env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.Run(async (context) =>
            {
                //не рекомендуется так делать, но можно
                var messageSender = context.RequestServices.GetService<IMessageSender>();

                messageSender = app.ApplicationServices.GetService<IMessageSender>();

                await context.Response.WriteAsync(messageService.Send()+ messageSender.Send() + timeService.GetTime())
                    .ConfigureAwait(false);
            });
        }
    }
}