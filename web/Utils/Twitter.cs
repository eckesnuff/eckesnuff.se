using System.Collections.Generic;
using TweetSharp;

namespace EckeSnuff.Utils {
    public class Twitter {
        private TwitterService _service;
        private static Twitter _instance=null;
        protected Twitter() {

            _service = new TwitterService();
            _service.AuthenticateWith("z8QjSW8DjdGulzV6HOiAPg", "cWlLPKci5I95ejzzbUlZqz5CXGDP0nXrTPpyuElgAg",
                                      "27216319-UClfYpcm0eqyrr7KhOHCMWzlIaIdU26hhinjVsnMm",
                                      "AzBjbgHXhQ2iLCfov5IHBZHZrOERba55Jf8bcHCICs");
            var t = new TwitterClientInfo();
            

        }
        public static Twitter Instance {
            get {
                if (_instance==null)
                    _instance = new Twitter();
                return _instance;
            }
        }
        public IEnumerable<TwitterStatus> GetTweets(int count) {
            return _service.ListTweetsOnUserTimeline(count);
        }
        public IEnumerable<TwitterStatus> GetTweetsSince(long id) {
            return _service.ListTweetsOnUserTimelineSince(id);
        }

    }
}