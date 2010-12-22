using System.Collections.Generic;
using System.Web.Mvc;
using EckeSnuff.Entities;
using EckeSnuff.Helpers;
using EckeSnuff.Models;
using Stormbreaker.Models;
using Stormbreaker.Services;

namespace EckeSnuff.Controllers {
    public class BaseController : Controller {
        public IList<ContentItem> RootPages { get; set; }
        public IList<RssItem> Tweets { get; set; }
        public IList<FlickrItem> Flickr { get; set; }

        public BaseController(IStormbreakerServices services) {
            //var services = ObjectFactory.GetInstance<IStormbreakerServices>();
            var root = (HomePage)services.ContentManager.GetByUrlSegment("home");
            //var root = (HomePage)services.ContentManager.GetByUrlSegment("home");
            RootPages = root.Children;
            Flickr = XmlList.GetFeed<FlickrItem>(root.FlickrFeed);
            Tweets = XmlList.GetFeed<RssItem>(root.TwitterFeed);
        }
    }
}