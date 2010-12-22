using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EckeSnuff.Entities;
using EckeSnuff.Helpers;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;
using Stormbreaker.Services;
using StructureMap;

namespace EckeSnuff.Controllers
{
    public class AjaxController : Controller {
        public JsonResult Tweets(long? ticks) {
            var services = ObjectFactory.GetInstance<IStormbreakerServices>();
            var root = (HomePage) services.ContentManager.GetByUrlSegment("home");
            var tweets = XmlList.GetFeed<RssItem>(root.TwitterFeed, false);
            List<RssItem> result = tweets.FindAll(x => x.Ticks > ticks);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GuestBookEntries(string pagePath, int page) {
        //    var services = ObjectFactory.GetInstance<IStormbreakerServices>();
        //    var gb = (GuestBook)services.ContentManager.GetByUrlSegment(pagePath);
        //    var model = new GuestBookModel(gb, null, null, null) {CurrentPage = page};

        //}
    }
}
