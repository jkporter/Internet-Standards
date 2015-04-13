using System;
using System.Collections.Generic;
using System.Xml;

namespace InternetStandards.Ietf.Atom
{
    public class AtomElement : XmlElement
    {
        protected const string AtomNS = "http://www.w3.org/2005/Atom";
        protected const string XhtmlNS = "http://www.w3.org/1999/xhtml";

        protected XmlNamespaceManager Manager = null;

        protected internal AtomElement(string prefix, string localname, string ns, XmlDocument doc)
            : base(prefix, localname, ns, doc)
        {
            Manager = new XmlNamespaceManager(OwnerDocument.NameTable);
            Manager.AddNamespace("atom", AtomNS);
            Manager.AddNamespace("xhtml", XhtmlNS);
        }

        public string Language
        {
            get
            {
                XmlNode currentNode = this;
                while (currentNode is XmlElement)
                {
                    if (((XmlElement)currentNode).HasAttribute("xml:lang"))
                        return ((XmlElement)currentNode).GetAttribute("xml:lang");
                    currentNode = currentNode.ParentNode;
                }

                if (OwnerDocument is AtomDocument)
                    return ((AtomDocument)OwnerDocument).Language;

                return string.Empty;
            }
            set
            {
                if (value == null)
                    RemoveAttribute("xml:lang");
                else
                    SetAttribute("xml:lang", value);
            }
        }

        public override string BaseURI
        {
            get
            {
                return GetBaseUri(this);
            }
        }

        protected XmlNodeList CreateAtomElementList(string localName)
        {
            return SelectNodes(Manager.LookupPrefix(AtomNS) + ":" + localName, Manager);
        }

        protected AtomElement GetAtomElement(string localName)
        {
            return (AtomElement)SelectSingleNode(Manager.LookupPrefix(AtomNS) + ":" + localName, Manager);
        }

        protected System.Uri GetUri(string localName)
        {
            return GetUri(GetAtomElement(localName));
        }

        protected string GetInnerText(string localName)
        {
            return GetInnerText(GetAtomElement(localName));
        }

        protected static System.Uri GetUriFromAttribute(XmlElement element, string attribute)
        {
            var attribValue = element.GetAttribute(attribute);
            return attribValue != null ? GetUri(element, attribValue) : null;
        }

        protected static System.Uri GetUri(XmlElement element)
        {
            var innerText = GetInnerText(element);
            return innerText != null ? GetUri(element, innerText) : null;
        }

        protected static string GetInnerText(XmlElement element)
        {
            return element.FirstChild != null ? element.FirstChild.Value : null;
        }

        protected static string GetBaseUri(XmlElement element)
        {
            System.Uri baseUri = null;
            XmlNode currentNode = element;
            var uriReferences = new Stack<string>();

            while (currentNode is XmlElement && baseUri == null)
            {
                var currentElement = (XmlElement)currentNode;
                if (currentElement.HasAttribute("xml:base"))
                {
                    if (System.Uri.IsWellFormedUriString(currentElement.GetAttribute("xml:base"), UriKind.Absolute))
                        baseUri = new System.Uri(currentElement.GetAttribute("xml:base"));
                    else
                        uriReferences.Push(currentElement.GetAttribute("xml:base"));
                }
                currentNode = currentNode.ParentNode;
            }

            if (baseUri == null && System.Uri.IsWellFormedUriString(element.OwnerDocument.BaseURI, UriKind.Absolute))
                baseUri = new System.Uri(element.OwnerDocument.BaseURI);

            if (baseUri == null) return string.Empty;
            while (uriReferences.Count > 0)
                baseUri = new System.Uri(baseUri, uriReferences.Pop());

            return baseUri.ToString();
        }

        protected static System.Uri GetUri(XmlElement startElement, string uriReference)
        {
            var elementBaseUri = GetBaseUri(startElement);
            return System.Uri.IsWellFormedUriString(elementBaseUri, UriKind.Absolute)
                ? new System.Uri(new System.Uri(elementBaseUri, UriKind.Absolute), uriReference)
                : new System.Uri(uriReference);
        }
    }
}
