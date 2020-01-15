using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleApp.Middleware
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate next;

        public RoutingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();
            
            if (path == "/index")
            {
                await context.Response.WriteAsync("Home Page").ConfigureAwait(false);
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("About").ConfigureAwait(false);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            //await _next.Invoke(context);
        }
    }
}