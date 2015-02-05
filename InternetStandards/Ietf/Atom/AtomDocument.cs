using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace InternetStandards.Ietf.Atom
{
    public class AtomDocument : XmlDocument
    {
        const string atomNs = "http://www.w3.org/2005/Atom";
        private string language = string.Empty;

        public AtomDocumentTypes AtomDocumentType
        {
            get
            {
                if (DocumentElement == null) return AtomDocumentTypes.Unknown;
                if (DocumentElement.NamespaceURI == atomNs && DocumentElement.LocalName == "feed")
                    return AtomDocumentTypes.FeedDocument;

                if (DocumentElement.NamespaceURI == atomNs && DocumentElement.LocalName == "entry")
                    return AtomDocumentTypes.EntryDocument;

                return AtomDocumentTypes.Unknown;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
        {
            if (namespaceUri != atomNs) return base.CreateElement(prefix, localName, namespaceUri);

            Type elementType;
            switch (localName)
            {
                case "rights":
                case "title":
                case "subtitle":
                case "summary":
                    elementType = typeof(AtomTextConstruct);
                    break;

                case "author":
                case "contributor":
                    elementType = typeof(AtomPersonConstruct);
                    break;

                case "updated":
                case "published":
                    elementType = typeof(AtomDateConstruct);
                    break;

                case "feed":
                    elementType = typeof(AtomFeedElement);
                    break;
                case "entry":
                    elementType = typeof(AtomEntryElement);
                    break;

                case "source":
                    elementType = typeof(AtomSourceElement);
                    break;

                case "category":
                    elementType = typeof(AtomCategoryElement);
                    break;
                case "generator":
                    elementType = typeof(AtomGeneratorElement);
                    break;
                case "link":
                    elementType = typeof(AtomLinkElement);
                    break;

                case "content":
                    elementType = typeof(AtomContentElement);
                    break;

                default:
                    elementType = typeof(AtomElement);
                    break;
            }
            return (XmlElement)CreateInstance(elementType, new object[] { prefix, localName, namespaceUri, this });
        }

        private Object CreateInstance(Type type, object[] args)
        {
            return type.Assembly.CreateInstance(
                type.FullName, false, BindingFlags.Instance | BindingFlags.NonPublic, null, args, null, new object[0]);
        }
    }

    public enum AtomDocumentTypes
    {
        Unknown,
        FeedDocument,
        EntryDocument
    }
}
