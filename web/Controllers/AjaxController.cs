using System.Linq;
using System.Web.Mvc;
using EckeSnuff.Services;

namespace EckeSnuff.Controllers
{
    public class AjaxController : Controller {
        public JsonResult Tweets(long? ticks) {
            var tweets = Twitter.Instance.GetTweets(10);
            var result = tweets.Where(x => x.CreatedDate.Ticks > ticks);
            return
                Json(
                    result.Select(
                        x =>
                        new
                        {
                            CreatedDate = x.CreatedDate.ToLocalTime().ToString(),
                            x.CreatedDate.Ticks,
                            Description = x.TextAsHtml
                        }), JsonRequestBehavior.AllowGet);
        }
    }
}
