using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SampleApp.App_Code
{
    public class TimerTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Текущее время: {DateTime.Now.ToShortTimeString()}");
        }
    }

    public class DateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
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