using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace InternetStandards.Ietf.Atom
{
    public class AtomEntryElement : AtomElement
    {
        internal AtomEntryElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public XmlNodeList Authors
        {
            get
            {
                var authors = CreateAtomElementList("author");
                if (authors.Count != 0) return authors;
                if (Source != null && Source.Authors.Count != 0)
                    return Source.Authors;
                if(ParentNode is AtomFeedElement)
                    return ((AtomFeedElement)ParentNode).Authors;

                return authors;
            }
        }

        public XmlNodeList Categories
        {
            get
            {
                return CreateAtomElementList("category");
            }
        }

        public AtomContentElement Content
        {
            get
            {
                return (AtomContentElement)GetAtomElement("content");
            }
        }

        public XmlNodeList Contributors
        {
            get
            {
                return CreateAtomElementList("contributor");
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

        public AtomDateConstruct Published
        {
            get
            {
                return (AtomDateConstruct)GetAtomElement("published");
            }
        }

        public AtomTextConstruct Rights
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("rights");
            }
        }

        public AtomTextConstruct Summary
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("summary");
            }
        }

        public AtomTextConstruct Title
        {
            get
            {
                return (AtomTextConstruct)GetAtomElement("title");
            }
        }

        public AtomSourceElement Source
        {
            get
            {
                return (AtomSourceElement)GetAtomElement("source");
            }
        }

        public AtomDateConstruct Updated
        {
            get
            {
                return (AtomDateConstruct)GetAtomElement("updated");
            }
        }

        public AtomDocument CreateEntryDocument()
        {
            var atomEntryDocument = new AtomDocument();
            atomEntryDocument.AppendChild(atomEntryDocument.ImportNode(this, true));

            if (!(ParentNode is AtomFeedElement)) return atomEntryDocument;
            var orignalFeed = (AtomFeedElement)ParentNode;
            var entry = (AtomEntryElement)atomEntryDocument.DocumentElement;
            if (entry.Source == null)
            {
                var source = (AtomSourceElement)atomEntryDocument.DocumentElement.AppendChild(atomEntryDocument.CreateElement("source", AtomNS));
                foreach (XmlNode node in orignalFeed.ChildNodes)
                    if (node.NodeType == XmlNodeType.Element && !(node.NamespaceURI == AtomNS && node.Name == "entry"))
                        source.AppendChild(atomEntryDocument.ImportNode(node, true));
            }

            if (entry.Authors.Count != 0) return atomEntryDocument;
            var nsManager = new XmlNamespaceManager(atomEntryDocument.NameTable);
            nsManager.AddNamespace("atom", AtomNS);

            foreach (XmlNode author in orignalFeed.SelectNodes(nsManager.LookupPrefix(AtomNS) + ":author", nsManager))
                atomEntryDocument.DocumentElement.AppendChild(atomEntryDocument.ImportNode(author, true));

            return atomEntryDocument;
        }
    }
}
