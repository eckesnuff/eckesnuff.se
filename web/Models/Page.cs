using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;
using Stormbreaker.Web;

namespace EckeSnuff.Models {
    [PageType(Name="Page", Description="Standard page eckesnuff.se")]
    public class Page : ContentItem {
        
        [Required(ErrorMessage="Required field")]
        [DisplayName("Heading:")]
        public virtual string Heading {
            get { return GetDetail("Heading") as string; }
            set { SetDetail("Heading", value); }
        }

        [UIHint("TinyMCE")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Content:")]
        [Required(ErrorMessage="Required field")]
        public virtual string Content {
            get { return GetDetail("Content") as string; }
            set { SetDetail("Content", value); }
        }
    }

}
