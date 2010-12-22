using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;
using Stormbreaker.Web;

namespace EckeSnuff.Models {
    [PageType(Name="Home", Description="Start page for eckesnuff.se")]
    public class HomePage : ContentItem {

        [UIHint("Heading")]
        public virtual string Heading {
            get { return GetDetail("Heading") as string; }
            set { SetDetail("Heading", value); }
        }
        [UIHint("TinyMCE")]
        [DataType(DataType.MultilineText)]
        public virtual string Content {
            get { return GetDetail("Content") as string; }
            set { SetDetail("Content", value); }
        }
        public virtual string FlickrFeed {
            get {return GetDetail("FlickrFeed") as string;}
            set {SetDetail("FlickrFeed", value);}
        }
        public virtual string TwitterFeed {
            get { return GetDetail("TwitterFeed") as string; }
            set { SetDetail("TwitterFeed", value); }
        }
    }

}