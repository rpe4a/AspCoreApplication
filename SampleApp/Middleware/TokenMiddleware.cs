﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleApp.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string pattern;

        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this.next = next;
            this.pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];

            if (!token.Equals(pattern))
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