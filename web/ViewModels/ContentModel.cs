using System.Collections.Generic;
using EckeSnuff.Entities;
using EckeSnuff.Helpers;
using EckeSnuff.Models;
using Stormbreaker.ContentManager;
using Stormbreaker.Models;
using Stormbreaker.Services;
using StructureMap;


namespace EckeSnuff.ViewModels {
    public class ContentModel : IItemContainer<ContentItem> {
        public ContentItem CurrentItem { get; private set; }

        public IList<ContentItem> RootPages { get; set; }
        public IList<RssItem> Tweets { get; set; }
        public IList<FlickrItem> Flickr { get; set; }

        public ContentModel(ContentItem currentItem, IList<ContentItem> rootPages, IList<RssItem> tweets, IList<FlickrItem> flickr) {
            CurrentItem = currentItem;
            RootPages = rootPages;
            Tweets = tweets;
            Flickr = flickr;
        }
    }
}