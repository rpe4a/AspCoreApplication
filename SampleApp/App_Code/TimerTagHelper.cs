using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SampleApp.Services;

namespace SampleApp.App_Code
{
    public class TimerTagHelper : TagHelper
    {
        private readonly TimeService _service;

        public TimerTagHelper(TimeService service)
        {
            _service = service;
        }

        public string Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "timer");
            output.Attributes.SetAttribute("style", $"color:{Color};");
            output.Content.SetContent($"Текущее время: {_service.GetTime()}");
        }
    }

    public class DateTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var font = ViewContext?.HttpContext.Request.Query["font"] ?? "Verdana";
            
            output.Attributes.SetAttribute("style", $"font-family:{font};font-size:18px;");
            output.TagName = "div";
            output.Content.SetContent($"Текущая дата: {DateTime.Now.ToString("dd/mm/yyyy")}");
        }
    }
    public class SummaryTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            // получаем вложенный контекст из дочерних tag-хелперов
            var target = await output.GetChildContentAsync();
            var content = "<h3>Общая информация</h3>" + target.GetContent();
            output.Content.SetHtmlContent(content);
        }
    }
}