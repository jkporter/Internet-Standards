using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
// using InternetStandards.Ietf.Http;

namespace InternetStandards.Ietf.Atom
{
    public class AtomContentElement : AtomElement
    {
        internal AtomContentElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public System.Uri Source
        {
            get
            {
                if (HasAttribute("src"))
                    return new System.Uri(new System.Uri(BaseURI), GetAttribute("src"));
                return null;
            }
        }

        public string Type
        {
            get
            {
                string type = GetAttribute("type");
                if (type == null)
                    return "text";

                return type;
            }
        }

        public string Content
        {
            get
            {
                if (Type == "xhtml")
                    try
                    {
                        return GetXhtmlDiv().InnerXml;
                    }
                    catch
                    {
                        return null;
                    }
                else if (Type == "text" | Type == "html")
                    return InnerText;
                else
                    return InnerXml;
            }
        }

        public Byte[] BinaryContent
        {
            get
            {
                if (IsContentBase64Encoded())
                    return Convert.FromBase64String(InnerText);

                return null;
            }
        }

        public string Xml
        {
            get
            {
                if (Type == "xhtml")
                    return FirstChild.InnerXml;
                return InnerXml;
            }
        }

        protected bool IsContentBase64Encoded()
        {
            switch (Type)
            {
                case "text":
                case "html":
                case "xhtml":
                    return false;
            }

            /* MediaType[] xmlMediaTypes = new MediaType[] {
                new MediaType("text/xml"),
                new MediaType("application/xml"),
                new MediaType("text/xml-external-parsed-entity"),
                new MediaType("application/xml-external-parsed-entity"),
                new MediaType("application/xml-dtd")
            };
            
            try
            {
                MediaType mediaType = new MediaType(Type);
                foreach (MediaType xmlMediaType in xmlMediaTypes)
                    if (mediaType.Type.Equals(xmlMediaType.Type, StringComparison.InvariantCultureIgnoreCase) && mediaType.Subtype.Equals(xmlMediaType.Subtype, StringComparison.InvariantCultureIgnoreCase))
                        return false;
            }
            catch
            {
            } */

            return !(Type.StartsWith("text/", StringComparison.InvariantCultureIgnoreCase)
                | Type.EndsWith("+xml", StringComparison.InvariantCultureIgnoreCase)
                | Type.EndsWith("/xml", StringComparison.InvariantCultureIgnoreCase));
        }

        private XmlNode GetXhtmlDiv()
        {
            if (FirstChild != null
              && FirstChild.NodeType == XmlNodeType.Element
              && FirstChild.NamespaceURI == "http://www.w3.org/1999/xhtml"
              && FirstChild.LocalName == "div")
                return FirstChild;

            return null;
        }
    }
}
