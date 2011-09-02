using System.Collections.Generic;
using System.Xml;
using EckeSnuff.Helpers.Xml;

namespace EckeSnuff.Helpers {
    public class XmlList {
        public static  List<TItem> GetFeed<TItem>(string url, bool useCache) where TItem : IXmlMappedObject {
            try {
                if (url != null) {
                    var doc = new XmlDocument();
                    if (useCache) {
                        doc = XmlCacheManager.Get(url, 5);
                    }
                    else {
                        doc.Load(url);
                    }
                    return XmlMapper.Get<TItem>(doc);
                }
            }
            catch {
                return new List<TItem>();
            }
            return null;

        }
        public static List<TItem> GetFeed<TItem>(string url) where TItem : IXmlMappedObject {
            return GetFeed<TItem>(url, true);
        }
    }
}