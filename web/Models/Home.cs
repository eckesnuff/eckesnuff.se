using System.ComponentModel.DataAnnotations;
using BrickPile.Domain;
using BrickPile.Domain.Models;

namespace EckeSnuff.Models {
    [PageModelAttribute("Home")]
    public class Home : BaseModel {
        [UIHint("Url to Flickr Feed")]
        public virtual string FlickrFeed { get; set; }
        [UIHint("Url to twitter feed")]
        public virtual string TwitterFeed { get; set; }
    }

}