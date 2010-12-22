using System;
using System.Web;
using EckeSnuff.Helpers.Xml;

namespace EckeSnuff.Entities {
    /// <summary>
    /// Rss Item for u all
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    [XmlMapping("rss/channel/item")]
    [XmlNameSpaces("media;http://search.yahoo.com/mrss/")]
    public class FlickrItem :  IXmlMappedObject {
        #region public string Link
        /// <summary>
        /// Get/Sets the Link of the RssContainer
        /// </summary>
        /// <value></value>
        [XmlMapping("link", true)]
        public string Link {
            get;
            set;
        }
        #endregion
        #region public string Description
        /// <summary>
        /// Get/Sets the Description of the RssContainer
        /// </summary>
        /// <value></value>
        public string Description {
            get {
                return _description;
            }
        }

        [XmlMapping("description", true)]
        protected string _description { get; set; }
        #endregion
        #region public string Title
        /// <summary>
        /// Get/Sets the Title of the RssContainer
        /// </summary>
        /// <value></value>
        [XmlMapping("title", true)]
        public string Title {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// Get/Sets the Thumbnail of the RssItem
        /// </summary>
        /// <value></value>
        [XmlMapping("media:thumbnail/@url", true)]
        public string Thumbnail {
            get;
            set;
        }

        [XmlMapping("media:title", true)]
        public string ImgDescription {
            get;
            set;
        }
        [XmlMapping("media:content/@url", true)]
        public string ImageLink {
            get;
            set;
        }
    }
}