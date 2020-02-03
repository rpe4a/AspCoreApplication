using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleApp.Filters
{
    public class IEFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // получаем информацию о браузере пользователя
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (Regex.IsMatch(userAgent, "MSIE|Trident"))
            {
                context.Result = new ContentResult { Content = "Ваш браузер устарел" };
            }
        }
    }
}