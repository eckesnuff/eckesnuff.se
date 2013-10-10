using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EckeSnuff.Entities;
using FriendlyWebRequester;
using FriendlyWebRequester.Services;

namespace EckeSnuff.Services {
    public class Flickr {
        private Request<List<FlickrItem>> _xmlService;
        private Uri _flickrUri;
        protected Flickr() {
            _xmlService = new Request<List<FlickrItem>>(
                new HttpCacheManager<List<FlickrItem>> { Timeout = 60 },
                new DiskStorageManager(),
                new XmlWebRequestRetriever(),
                new XmlToFlickrConverter()
                );
            _flickrUri = new Uri(ConfigurationManager.AppSettings["FlickrFeed"]);
        }
        private static Flickr _instance;
        public static Flickr Instance {
            get { return _instance ?? (_instance = new Flickr()); }
        }
        public List<FlickrItem> GetItems(int count = 10) {
            var response = _xmlService.GetWrappedResponse(_flickrUri);
            if(response.Data==null) {
                return new List<FlickrItem>();
            }
            return response.Data.Take(count).ToList();
        }
    }
}