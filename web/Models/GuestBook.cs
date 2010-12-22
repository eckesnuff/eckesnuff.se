using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;
using Stormbreaker.Web;

namespace EckeSnuff.Models {
    [PageType(Name="GuestBook", Description="My GuestBook")]
    public class GuestBook:ContentItem {
        [UIHint("Heading")]
        public virtual string Heading {
            get { return GetDetail("Heading") as string; }
            set { SetDetail("Heading", value); }
        }
        [DataType(DataType.MultilineText)]
        public virtual string Content {
            get { return GetDetail("Content") as string; }
            set { SetDetail("Content", value); }
        }
    }
}