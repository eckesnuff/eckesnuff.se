using System;
using System.Collections.Generic;

namespace EckeSnuff.Helpers.Xml {
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class XmlMappingAttribute : Attribute {
        #region public XmlMappingAttribute(string path)
        /// <summary>
        /// Initializes a new instance of the <b>XmlMappingAttribute</b> class.
        /// </summary>
        /// <param name="path"></param>
        public XmlMappingAttribute(string path)
            : this(path, false) {
        }
        #endregion
        #region public XmlMappingAttribute(string path,bool required)
        /// <summary>
        /// Initializes a new instance of the <b>XmlMappingAttribute</b> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="required"></param>
        public XmlMappingAttribute(string path, bool required) {
            XPath = path;
            Required = required;
        }
        #endregion
        #region public string XPath
        /// <summary>
        /// Get/Sets the XPath of the XmlMappingAttribute
        /// </summary>
        /// <value></value>
        public string XPath {
            get;
            private set;
        }
        #endregion
        #region public bool Required
        /// <summary>
        /// Get/Sets the Required of the XmlMappingAttribute
        /// </summary>
        /// <value></value>
        public bool Required {
            get;
            private set;
        }
        #endregion
    }

    /// <summary>
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class XmlNameSpacesAttribute : Attribute
    {
        #region public XmlNameSpacesAttribute(string nameSpaces)
        /// <summary>
        /// Initializes a new instance of the <b>XmlNameSpacesAttribute</b> class.
        /// </summary>
        /// <param name="nameSpaces"></param>
        public XmlNameSpacesAttribute(string nameSpaces) {
            var keyValue = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(nameSpaces)) {
                foreach (string kvps in nameSpaces.Split(',')) {
                    if (kvps!=null && kvps.Contains(";")) {
                        string[] kvp = kvps.Split(';');
                        keyValue.Add(new KeyValuePair<string, string>(kvp[0], kvp[1]));
                    }
                }
            }
            NameSpaces = keyValue.ToArray();

        }
        #endregion
        #region public KeyValuePair<string, string>[] NameSpaces

        /// <summary>
        /// 
        /// Get/Sets the NameSpaces of the XmlNameSpacesAttribute
        /// </summary>
        /// <value></value>
        public KeyValuePair<string, string>[] NameSpaces { get; private set; }

        #endregion
    }
}