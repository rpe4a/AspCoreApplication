using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using EntitiesLib;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using SampleApp.Hubs;
using SampleApp.JwtBearer;
using SampleApp.Middleware;
using SampleApp.Services;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
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
            services.AddTransient<IUserRepository, UserRepository>(p =>
                new UserRepository(AppConfiguration.GetConnectionString("DefaultConnection")));

            services.AddCors(o =>
            {
                o.AddPolicy("Test", builder => builder
                    .WithOrigins("https://localhost:44321")
                    .AllowAnyHeader()
                    .AllowAnyMethod());

                o.AddPolicy("Production", builder => builder
                    .WithOrigins("http://localhost.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddMvc(o =>
            {
                // глобально - все сервисы MVC - и контроллеры, и Razor Page
                //o.Filters.Add<AuthorizeFilter>();
                //o.Filters.Add(new AuthorizeFilter()); //или так
            }).AddDataAnnotationsLocalization();


            services.AddMemoryCache();
            //services.AddControllers();

            services.AddResponseCompression(o =>
            {
                o.EnableForHttps = true;
                //дополнительные миме типы для сжатия
                o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "image/svg+xml",
                    "application/atom+xml"
                });
                o.Providers.Add<BrotliCompressionProvider>();
                o.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddTransient<IStringLocalizer, CustomStringLocalizer>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.AddSignalR().AddHubOptions<ChatHub>(o =>
                {
                    o.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
                    o.EnableDetailedErrors = true;
                }
            );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseExceptionHandler("/error");
            //else app.UseExceptionHandler("/error");

            var server = app.ServerFeatures.Get<IServerAddressesFeature>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("Test");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseResponseCompression();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-GB"),
                new CultureInfo("en"),
                new CultureInfo("ru-RU"),
                new CultureInfo("ru"),
                new CultureInfo("de-DE"),
                new CultureInfo("de")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapHub<ChatHub>("/chathub", o =>
                {
                    o.ApplicationMaxBufferSize = 64;
                    o.TransportMaxBufferSize = 64;
                    o.LongPolling.PollTimeout = System.TimeSpan.FromMinutes(1);
                    o.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                });

                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}