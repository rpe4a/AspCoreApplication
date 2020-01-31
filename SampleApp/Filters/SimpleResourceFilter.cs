using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SampleApp.Filters
{
    public class SimpleResourceFilter: Attribute, IAsyncResourceFilter
    {
        private readonly int _id;
        private readonly string _token;

        private ILogger<SimpleResourceFilter> logger;

        public SimpleResourceFilter(ILoggerFactory loggerFactory, int id, string token)
        {
            logger = loggerFactory.CreateLogger<SimpleResourceFilter>();
            _id = id;
            _token = token;
            logger.LogCritical("dsdada");
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
            context.HttpContext.Response.Headers.Add("Id", _id.ToString());
            context.HttpContext.Response.Headers.Add("Token", _token);

            await next().ConfigureAwait(false);
        }
    }
}
