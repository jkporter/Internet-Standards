using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace InternetStandards.Oasis.XmlCatalog
{
    public class XmlCatalogResolver : XmlResolver
    {
        public const string XmlCatalogNamespace = "urn:oasis:names:tc:entity:xmlns:xml:catalog";
        private List<string> catalogFiles = new List<string>();
        private XmlResolver baseXmlResolver = null;

        public XmlCatalogResolver(IEnumerable<string> catalogFiles, XmlResolver baseXmlResolver)
        {
            this.catalogFiles.AddRange(catalogFiles);
            this.baseXmlResolver = baseXmlResolver;
        }

        public XmlCatalogResolver(IEnumerable<string> catalogFiles):this(catalogFiles, new XmlUrlResolver())
        {
        }

        public override System.Net.ICredentials Credentials
        {
            set { baseXmlResolver.Credentials = value; }
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            throw new NotImplementedException();
        }

        public object GetEntity(string publicIdentifier, string systemIdentifier, Type ofObjectToReturn)
        {
            throw new NotImplementedException();
        }

        public object GetEntity(string identifier, bool isPublic, Type ofObjectToReturn)
        {
            if (isPublic)
                return GetEntity(identifier, null, ofObjectToReturn);
            else
                return GetEntity(null, identifier, ofObjectToReturn);
        }

        public object GetEntity(string systemIdentifier, Type ofObjectToReturn)
        {
            return GetEntity(null, systemIdentifier, ofObjectToReturn);
        }

        public static Uri ResolveExternalIdentifier(IEnumerable<string> catalogFiles, string publicIdentifier, string systemIdentifier)
        {
            Queue<string> catalogFileQueue = new Queue<string>(catalogFiles);
            while (catalogFileQueue.Count > 0)
            {
                XPathDocument catalogDocument = new XPathDocument(catalogFileQueue.Dequeue());
                XPathNavigator navigator = catalogDocument.CreateNavigator();

                XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
                manager.AddNamespace("catalog", XmlCatalogNamespace);

                if (systemIdentifier != null)
                {
                    XPathNodeIterator systemIterator = navigator.Select("/catalog:catalog/catalog:system|/catalog:catalog/catalog:group/catalog:system", manager);
                    while (systemIterator.MoveNext())
                    {
                        if (systemIterator.Current.GetAttribute("uri", string.Empty) == systemIdentifier)
                            return new Uri(systemIterator.Current.GetAttribute("uri", string.Empty));
                    }
                }

                if (systemIdentifier != null)
                {
                    XPathNodeIterator systemIterator = navigator.Select("/catalog:catalog/catalog:rewriteSystem|/catalog:catalog/catalog:group/catalog:rewriteSystem", manager);
                    while (systemIterator.MoveNext())
                    {
                        if (systemIterator.Current.GetAttribute("uri", string.Empty) == systemIdentifier)
                            return new Uri(systemIterator.Current.GetAttribute("uri", string.Empty));
                    }
                }
            }
            return null;
        }

        public static Uri ResolveExternalIdentifier(IEnumerable<string> catalogFiles, string identifier, bool isPublic)
        {
            if (isPublic)
                return ResolveExternalIdentifier(catalogFiles, identifier, null);
            else
                return ResolveExternalIdentifier(catalogFiles, null, identifier);
        }

        private string SystemIdentifierAndUriNormalization(string input)
        {
            return null;
        }

        private string UnwrapUrn(string publicIdUrn)
        {
            Regex exp = new Regex("(%[0-9A-F]{2})|.");
            StringBuilder sb = new StringBuilder();
            foreach (string s in exp.Split(publicIdUrn.Remove(0, 13)))
            {
                switch (s)
                {
                    case "+":
                        sb.Append(" ");
                        break;
                    case ":":
                        sb.Append("//");
                        break;
                    case "%2B":
                        sb.Append("+");
                        break;
                    case "%3A":
                        sb.Append(":");
                        break;
                    case "%2F":
                        sb.Append("/");
                        break;
                    case "%3B":
                        sb.Append(";");
                        break;
                    case "%27":
                        sb.Append("'");
                        break;
                    case "%3F":
                        sb.Append("?");
                        break;
                    case "%23":
                        sb.Append("#");
                        break;
                    case "%25":
                        sb.Append("%");
                        break;
                    default:
                        sb.Append(s);
                        break;
                }
            }

            return sb.ToString();
        }

        public XmlResolver BaseXmlResolver
        {
            get
            {
                return baseXmlResolver;
            }
        }
    }
}
