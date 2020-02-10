using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using EntitiesLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

            services.AddMvc(o =>
            {
                // ��������� - ��� ������� MVC - � �����������, � Razor Page
                //o.Filters.Add<AuthorizeFilter>();
                //o.Filters.Add(new AuthorizeFilter()); //��� ���
            });


            services.AddMemoryCache();
            services.AddControllers();

            services.AddResponseCompression(o =>
            {
                o.EnableForHttps = true;
                //�������������� ���� ���� ��� ������
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseResponseCompression();

            app.UseMiddleware<CultureMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}