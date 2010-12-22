using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Stormbreaker.Models;

namespace EckeSnuff.Web.Mvc.Html {
    public static class NavigationHelper {
        public static string Navigation(this HtmlHelper helper,string attribute, IList<ContentItem> items ) {
            var sb = new StringBuilder();
            sb.AppendLine("<ul " + attribute + ">");
            int count = 1;
            foreach (var item in items) {
                sb.AppendLine("<li>");
                sb.AppendLine("<a class=\"item_"+count++ +"\" href=\"/"+item.UrlSegment+"\">"+ item.Name+"</a>");
                sb.AppendLine("</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}