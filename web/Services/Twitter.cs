using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FriendlyWebRequester;
using FriendlyWebRequester.Contracts;
using FriendlyWebRequester.Services;
using Newtonsoft.Json;
using TweetSharp;

namespace EckeSnuff.Services {
    public class Twitter {
        private Request<List<TwitterStatus>> _service;
        private static Twitter _instance;
        protected Twitter() {
            _service = new Request<List<TwitterStatus>>(new HttpCacheManager<List<TwitterStatus>>{Timeout = 10},
                                                        new DiskStorageManager(), new TweetRequester(),
                                                        new TweetConverter());

        }
        public static Twitter Instance {
            get {
                if (_instance==null)
                    _instance = new Twitter();
                return _instance;
            }
        }
        public List<TwitterStatus> GetTweets(int count) {
            var tweets = _service.GetWrappedResponse(new Uri("http://example.com"));
            if(tweets.Data!=null) {
                return tweets.Data.Take(count).ToList();
            }
            return null;
        }
        
    }
    public class TweetRequester: IDataRetriever{
        private TwitterService _service;
        public string Get(Uri uri) {
            _service = new TwitterService();
            _service.AuthenticateWith(
                ConfigurationManager.AppSettings["TwitterConsumerKey"],
                ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                ConfigurationManager.AppSettings["TwitterToken"],
                ConfigurationManager.AppSettings["TwitterTokenSecret"]);
            var tweets = _service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions {Count = 100}).ToList();
            return JsonConvert.SerializeObject(tweets);
        }
    }
    public class TweetConverter : IConverter<List<TwitterStatus>>  {
        public List<TwitterStatus> FromString(string data) {
            return JsonConvert.DeserializeObject<List<TwitterStatus>>(data);
        }
    }
}