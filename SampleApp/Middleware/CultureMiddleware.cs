using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleApp.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Query["lang"].ToString();

            if (!string.IsNullOrEmpty(lang))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(lang);
                    CultureInfo.CurrentUICulture = new CultureInfo(lang);
                }
                catch (CultureNotFoundException) { }
            }

            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}