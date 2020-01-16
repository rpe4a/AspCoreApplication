using Microsoft.Extensions.DependencyInjection;
using SampleApp.Services;

namespace SampleApp.ServiceExtensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
            services.AddTransient<TimeService>();
        }
    }
}