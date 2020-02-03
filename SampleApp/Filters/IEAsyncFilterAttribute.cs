using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleApp.Filters
{
    public class IEAsyncFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // получаем информацию о браузере пользователя
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (Regex.IsMatch(userAgent, "MSIE|Trident"))
            {
                context.Result = new ContentResult { Content = "Ваш браузер устарел" };
            }
            else    // если браузер не IE, передаем обаботку запроса дальше
                await next();
        }
    }
}