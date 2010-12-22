using System;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace EckeSnuff.Helpers {
    public class XmlCacheManager {
        public static XmlDocument Get(string path, uint cacheTimeInMinutes) {
            var doc = HttpContext.Current.Cache.Get(path) as XmlDocument;
            if(doc!=null) {
                return doc;
            }
            else {
                doc = new XmlDocument();
                doc.Load(path);
                HttpContext.Current.Cache.Add(path, doc, null, DateTime.Now.AddMinutes(cacheTimeInMinutes),
                                              Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                return doc;
            }


        }
    
    }
}