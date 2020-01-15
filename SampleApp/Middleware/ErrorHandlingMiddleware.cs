using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleApp.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next.Invoke(context).ConfigureAwait(false);
            
            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied").ConfigureAwait(false);
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found").ConfigureAwait(false);
            }
        }
    }
}