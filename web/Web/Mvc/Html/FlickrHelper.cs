using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using EckeSnuff.Entities;

namespace EckeSnuff.Web.Mvc.Html {
    public static class FlickrHelper {
        public static string FeedMe(this HtmlHelper helper, List<FlickrItem> items) {
            return FeedMe(helper, items, 0);
        }
        public static string FeedMe(this HtmlHelper helper, List<FlickrItem> items, int maxCount) {
            var sb = new StringBuilder("<ul>");
            int count = 0;
            foreach (var item in items) {
                sb.AppendLine("<li>");
                sb.AppendLine("<a href=\"" + item.ImageLink + "\" title=\""+item.ImgDescription+" \">");
                sb.AppendLine("<img src=\"" + item.Thumbnail + "\" alt=\"" + item.ImgDescription + " \" />");
                sb.AppendLine("</a>");
                sb.AppendLine("</li>");
                if (++count==maxCount&&maxCount!=0) {
                    break;
                }
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}