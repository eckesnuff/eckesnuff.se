using System.Collections.Generic;
using System.Configuration;
using BrickPile.Domain.Models;
using BrickPile.UI.Web.ViewModels;
using EckeSnuff.Entities;
using EckeSnuff.Models;
using TweetSharp;

namespace EckeSnuff.ViewModels {
    public class BaseViewModel<TModel>: DefaultViewModel<TModel>,IBaseViewModel<TModel> where TModel: IPage {
        public string Title { get; set; }
        public IList<FlickrItem> Flickr { get; set; }
        public IEnumerable<TwitterStatus> Tweets { get; set; } 

        public BaseViewModel(TModel model){
            var page = model as BaseModel;
            if(page!=null) {
                Title = page.Heading + " - " + ConfigurationManager.AppSettings["SiteName"];
            }
            CurrentPage = model;
        }
    }
}