using System.Collections.Generic;
using System.Web.Mvc;
using BrickPile.UI;
using EckeSnuff.Entities;
using EckeSnuff.Helpers;
using EckeSnuff.Models;

namespace EckeSnuff.Controllers
{
    public class AjaxController : Controller {
        private readonly IStructureInfo _structureInfo;
        public AjaxController(IStructureInfo structureInfo) {
            _structureInfo = structureInfo;
        }
        public JsonResult Tweets(long? ticks) {
            var home = (Home)_structureInfo.RootModel;
            var tweets = XmlList.GetFeed<RssItem>(home.TwitterFeed, false);
            List<RssItem> result = tweets.FindAll(x => x.Ticks > ticks);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
