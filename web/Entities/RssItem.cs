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
    public class RssItem :  IXmlMappedObject {
        #region public DateTime PubDate
        /// <summary>
        /// Get/Sets the PubDate of the RssContainer
        /// </summary>
        /// <value></value>
        [XmlMapping("pubDate",true)]
        public DateTime PubDate {
            get;
            set;
        }
        #endregion
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
        public long Ticks {
            get {
                return PubDate.Ticks;
            }
        }
    }
}