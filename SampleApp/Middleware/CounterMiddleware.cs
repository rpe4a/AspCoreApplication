using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SampleApp.Services;

namespace SampleApp.Middleware
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate next;
        private int i = 0;

        public CounterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService)
        {
            i++;
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"Запрос {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
        }
    }
}
