using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleApp.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];

            if (token.Equals("12345"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid.").ConfigureAwait(false);
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }
    }
}