using System.Collections.Generic;
using System.Linq;
using EckeSnuff.Entities;
using EckeSnuff.Models;
using Stormbreaker.Models;

namespace EckeSnuff.ViewModels {
    public class GuestBookModel :ContentModel {
        public GuestBookModel(ContentItem currentItem, IList<ContentItem> rootPages, IList<RssItem> tweets, IList<FlickrItem> flickr)
            : base(currentItem, rootPages, tweets, flickr) {
            Comments = currentItem.Children.Cast<Comment>();
        }

        public IEnumerable<Comment> Comments {
            get; private set;
        }
        public Comment CurrentComment {
            get; set;
        }
        public int CurrentPage {
            get; set;
        }
    }
}
