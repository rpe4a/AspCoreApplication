using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SampleApp.App_Code
{
    public class ListTagHelper : TagHelper
    {
        public List<string> Elements { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            string listContent = "";
            foreach (string item in Elements)
                listContent += "<li>" + item + "</li>";

            output.Content.SetHtmlContent(listContent);
        }
    }
}