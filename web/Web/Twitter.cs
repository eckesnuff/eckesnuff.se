using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EckeSnuff.Web {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    public class Twitter {
        /* *******************************************************************
		*  Properties
		* *******************************************************************/
        public string Message { get; set; }
        public DateTime PubDate { get; set; }
        /* *******************************************************************
		*  Constructors
		* *******************************************************************/
        /* *******************************************************************
		*  Methods
		* *******************************************************************/
        /* *******************************************************************
		*  Event methods
		* *******************************************************************/
        public static List<Twitter> Parse(string user) {
            var rv = new List<Twitter>();
            var url = "http://twitter.com/statuses/user_timeline/" + user + ".rss";
            var element = XElement.Load(url);
            foreach (var node in element.Element("channel").Elements("item")) {
                var twit = new Twitter();
                var message = node.Element("description").Value;
                //remove username information
                twit.Message = message.Replace(user + ": ", string.Empty);
                twit.PubDate = DateTime.Parse(node.Element("pubDate").Value);
                rv.Add(twit);
            }

            return rv;

        }
    }
}