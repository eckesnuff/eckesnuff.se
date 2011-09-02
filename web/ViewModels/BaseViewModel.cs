using System.Collections.Generic;
using System.Configuration;
using BrickPile.Domain.Models;
using BrickPile.UI;
using BrickPile.UI.Web.ViewModels;
using EckeSnuff.Entities;
using EckeSnuff.Helpers;
using EckeSnuff.Models;

namespace EckeSnuff.ViewModels {
    public abstract class BaseViewModel<TModel>: DefaultViewModel<TModel>,IBaseViewModel<TModel> where TModel: IPageModel {
        public string Title { get; set; }
        public IList<RssItem> Tweets { get; set; }
        public IList<FlickrItem> Flickr { get; set; }


        public BaseViewModel(TModel model, IStructureInfo structureInfo) : base(model, structureInfo) {
            var page = model as BaseModel;
            if(page!=null) {
                Title = page.Heading + " - " + ConfigurationManager.AppSettings["SiteName"];
            }
            
            var home = (Home) structureInfo.RootModel;
            Flickr = XmlList.GetFeed<FlickrItem>(home.FlickrFeed);
            Tweets = XmlList.GetFeed<RssItem>(home.TwitterFeed);
        }
    }
}