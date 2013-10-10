using System.Collections.Generic;
using System.Xml;
using EckeSnuff.Entities;
using FriendlyWebRequester.Contracts;

namespace EckeSnuff.Services {
    public class XmlToFlickrConverter : IConverter<List<FlickrItem>> {
        public List<FlickrItem> FromString(string data) {
            var doc = new XmlDocument();
            doc.LoadXml(data);
            var nsmngr = new XmlNamespaceManager(doc.NameTable);
            nsmngr.AddNamespace("media", "http://search.yahoo.com/mrss/");
            var nodeList = doc.SelectNodes("rss/channel/item", nsmngr);
            var result = new List<FlickrItem>();
            if(nodeList==null) {
                return result;
            }
            foreach(XmlNode node in nodeList) {
                result.Add(new FlickrItem
                {
                    ImageLink = GetNodeValue("media:content/@url", node, nsmngr),
                    Description = GetNodeValue("description", node, nsmngr),
                    ImgDescription = GetNodeValue("media:title", node, nsmngr),
                    Link = GetNodeValue("link", node, nsmngr),
                    Thumbnail = GetNodeValue("media:thumbnail/@url", node, nsmngr),
                    Title = GetNodeValue("title", node, nsmngr)
                });
            }
            return result;
        }
        private string GetNodeValue(string xPath,XmlNode node,XmlNamespaceManager manager) {
            var selectedNode = node.SelectSingleNode(xPath, manager);
            return selectedNode != null ? selectedNode.InnerText : string.Empty;
        }
    }
}