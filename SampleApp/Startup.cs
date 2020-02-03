using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EntitiesLib;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using SampleApp.Filters;
using SampleApp.Middleware;
using SampleApp.MiddlewareExtensions;
using SampleApp.Requirement;
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
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(AppConfiguration.GetConnectionString("DefaultConnection")));
            services.AddTransient<TimeService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/Account/Register");
                    o.AccessDeniedPath = new PathString("/Account/Register");
                });

            services.AddTransient<IAuthorizationHandler, AgeHandler>();

            services.AddAuthorization(opts => {
                opts.AddPolicy("OnlyForLondon", policy => {
                    policy.RequireClaim(ClaimTypes.Locality, "Лондон", "London");
                });
                opts.AddPolicy("OnlyForMicrosoft", policy => {
                    policy.RequireClaim("company", "Microsoft");
                });
                opts.AddPolicy("AgeLimit",
                    policy => policy.Requirements.Add(new AgeRequirement(50)));
            });

            services.AddMvc(o =>
            {
                // глобально - все сервисы MVC - и контроллеры, и Razor Page
                //o.Filters.Add<AuthorizeFilter>();
                //o.Filters.Add(new AuthorizeFilter()); //или так

            }).AddXmlDataContractSerializerFormatters();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}