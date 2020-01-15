using Microsoft.AspNetCore.Builder;
using SampleApp.Middleware;

namespace SampleApp.MiddlewareExtensions
{
    public static class TokenExtension
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
        {
            return builder.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}