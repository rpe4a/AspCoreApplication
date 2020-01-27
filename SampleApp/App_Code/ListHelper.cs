using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleApp.App_Code
{
    public static class ListHelper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            var result = "<ul>";
            foreach (var item in items) result = $"{result}<li>{item}</li>";
            result = $"{result}</ul>";
            return new HtmlString(result);
        }

        public static HtmlString CreateListWithTagBuilder(this IHtmlHelper html, string[] items)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (string item in items)
            {
                TagBuilder li = new TagBuilder("li");
                // добавляем текст в li
                li.InnerHtml.Append(item);
                // добавляем li в ul
                ul.InnerHtml.AppendHtml(li);
            }
            ul.Attributes.Add("class", "itemsList");
            var writer = new System.IO.StringWriter();
            ul.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}