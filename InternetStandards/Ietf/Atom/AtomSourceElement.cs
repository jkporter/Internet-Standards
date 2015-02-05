using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace InternetStandards.Ietf.Atom
{
    public class AtomSourceElement : AtomElement
    {
        internal AtomSourceElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public XmlNodeList Authors
        {
            get
            {
                return CreateAtomElementList("author");
            }
        }

        public XmlNodeList Categories
        {
            get
            {
                return CreateAtomElementList("category");
            }
        }

        public XmlNodeList Contributors
        {
            get
            {
                return CreateAtomElementList("contributor");
            }
        }

        public AtomGeneratorElement Generator
        {
            get
            {
                return (AtomGeneratorElement)GetAtomElement("generator"); ;
            }
        }

        public System.Uri Icon
        {
            get
            {
                return GetUri("icon");
            }
        }

        public string Id
        {
            get
            {
                return GetInnerText("id");
            }
        }

        public XmlNodeList Links
        {
            get
            {
                return CreateAtomElementList("link");
            }
        }

        public System.Uri Logo
        {
            get
            {
                return GetUri("logo");
            }
        }

        public AtomTextConstruct Rights
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("rights");
            }
        }

        public AtomTextConstruct Subtitle
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("subtitle");
            }
        }

        public AtomTextConstruct Title
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("title");
            }
        }

        public AtomDateConstruct Updated
        {
            get
            {
                return (AtomDateConstruct)GetAtomElement("updated");
            }
        }
    }
}
