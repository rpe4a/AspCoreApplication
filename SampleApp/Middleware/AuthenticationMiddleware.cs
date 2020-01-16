using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SampleApp.Services;

namespace SampleApp.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageSender sender)
        {
            var token = context.Request.Query["token"];

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(sender.Send()).ConfigureAwait(false);
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }
    }
}