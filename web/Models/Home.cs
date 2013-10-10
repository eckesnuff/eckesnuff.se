using System.ComponentModel.DataAnnotations;
using BrickPile.Domain;
using EckeSnuff.Controllers;

namespace EckeSnuff.Models {
    [ContentType(Name="Home",ControllerType=typeof(HomeController))]
    public class Home : BaseModel {
        [UIHint("Url to Flickr Feed")]
        public virtual string FlickrFeed { get; set; }
    }

}