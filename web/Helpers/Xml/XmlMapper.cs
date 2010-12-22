using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace EckeSnuff.Helpers.Xml {
    /// <summary>
    /// Xml helper
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    public class XmlMapper {
        private static string _rootXPath;
        #region public static List<T> Get<T>(XmlDocument document)
        /// <summary>
        /// Returns a list of instances of the current Provided Type whose Properties are mapped to their xpath expression
        /// </summary>
        /// <param name="document">Document to get Data from</param>
        /// <returns>A list of the instanced class</returns>
        public static List<T> Get<T>(XmlDocument document) where T : IXmlMappedObject {
            var classAtrInfo = GetClassAttributeInfo(typeof(T));
            var nsmgr = GetNamespaceManager(document, typeof (T));
            _rootXPath = classAtrInfo.XPath;
            var nodeList = document.SelectNodes(_rootXPath,nsmgr);
            var items = new List<T>();
            PropertyInfo[] info =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (nodeList != null) {
                foreach (XmlNode node in nodeList) {
                    var mapper = Activator.CreateInstance<T>();
                    foreach (var propertyInfo in info) {
                        if (PropertyIsMapped(propertyInfo)) {
                            propertyInfo.SetValue(mapper, GetValues(node, propertyInfo,nsmgr), null);
                        }
                    }
                    items.Add(mapper);
                }
            }
            return items;
        }
        #endregion
        #region private static XmlMappingAttribute GetAttributeInfo(ICustomAttributeProvider info)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static XmlMappingAttribute GetAttributeInfo(ICustomAttributeProvider info) {
            var classAttr = info.GetCustomAttributes(typeof(XmlMappingAttribute), false);
            return classAttr[0] as XmlMappingAttribute;
        }
        #endregion
        #region private static XmlMappingAttribute GetClassAttributeInfo(ICustomAttributeProvider info)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static XmlMappingAttribute GetClassAttributeInfo(ICustomAttributeProvider info) {
            var classAttr = info.GetCustomAttributes(typeof(XmlMappingAttribute), true);
            if (classAttr == null || classAttr.Length == 0)
                throw new ArgumentException("XmlMappingAttribute missing on class declaration");
            return classAttr[0] as XmlMappingAttribute;
        }
        #endregion

        #region private static XmlNamespaceManager GetNamespaceManager(XmlDocument doc, ICustomAttributeProvider info)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private static XmlNamespaceManager GetNamespaceManager(XmlDocument doc, ICustomAttributeProvider info) {
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            var classAttr = info.GetCustomAttributes(typeof(XmlNameSpacesAttribute), true);
            if (classAttr != null && classAttr.Length > 0) {
                var xmlNameSpacesAttribute = classAttr[0] as XmlNameSpacesAttribute;
                if (xmlNameSpacesAttribute!=null) {
                    foreach (KeyValuePair<string, string> kvp in xmlNameSpacesAttribute.NameSpaces) {
                        nsmgr.AddNamespace(kvp.Key, kvp.Value);
                    }
                }
            }
            return nsmgr;
        }
        #endregion

        #region private static bool PropertyIsMapped(ICustomAttributeProvider pInfo)
        /// <summary>
        /// return true if the property has a provided xpath
        /// </summary>
        /// <param name="pInfo">Property to check</param>
        /// <returns>True if xml is mapped, otherwise false.</returns>
        private static bool PropertyIsMapped(ICustomAttributeProvider pInfo) {
            var classAttr = pInfo.GetCustomAttributes(typeof(XmlMappingAttribute), false);
            return classAttr != null && classAttr.Length > 0;
        }
        #endregion
        #region private static object GetValues(XmlNode node, PropertyInfo pInfo)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node">The node to look in</param>
        /// <param name="pInfo">The property</param>
        /// <param name="nsmgr">name spacespace manager</param>
        /// <returns>A string[] or string depending on property</returns>
        private static object GetValues(XmlNode node, PropertyInfo pInfo, XmlNamespaceManager nsmgr)
        {
            var attrInfo = GetAttributeInfo(pInfo);
            XmlNodeList list = node.SelectNodes(attrInfo.XPath,nsmgr);
            var nodes = new List<string>();
            if (list != null && list.Count > 0)
            {
                if (pInfo.PropertyType == typeof (string[]))
                {
                    foreach (XmlNode n in list)
                    {
                        nodes.Add(n.InnerText);
                    }
                }
                else if (pInfo.PropertyType == typeof (string))
                {
                    return list[0].InnerText;
                }
                else
                {
                    var parseMethod = pInfo.PropertyType.GetMethod("Parse", new[] {typeof (string)});
                    if (parseMethod != null)
                        return parseMethod.Invoke(pInfo, new object[] {list[0].InnerText});
                    throw new Exception("No Parse(string) method found for Propery type");
                }

            }
            else if (attrInfo.Required)
            {
                throw new ArgumentException(
                    string.Format("Element is required and is not found in XmlDocument, XPath: {0}/{1}", _rootXPath,
                                  attrInfo.XPath));
            }
            return nodes.ToArray();
        }

        #endregion
    }
}