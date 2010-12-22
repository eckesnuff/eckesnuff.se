using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using EckeSnuff.Entities;

namespace EckeSnuff.Web.Mvc.Html {
    public static class RssFeedHelper {
        public static string FeedMe(this HtmlHelper helper, List<RssItem> items) {
            return FeedMe(helper, items,0);
        }
        public static string FeedMe(this HtmlHelper helper, List<RssItem> items, int maxCount) {
            var sb = new StringBuilder("<ul>");
            int count = 0;
            foreach(var item in items) {
                sb.AppendLine("<li>");
                sb.AppendLine("<span class=\"ticks\">" + item.Ticks + "</span>");
                sb.AppendLine(item.Description);
                sb.AppendLine("</li>");
                if (++count==maxCount&&maxCount!=0) {
                    break;
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}