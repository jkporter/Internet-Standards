using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Atom
{
    public class AtomLinkElement : AtomElement
    {
        internal AtomLinkElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public System.Uri HyperlinkReference
        {
            get
            {
                return GetUriFromAttribute(this, "href");
            }
        }

        public string Relationship
        {
            get
            {
                return GetAttribute("rel");
            }
        }

        public string Type
        {
            get
            {
                return GetAttribute("type");
            }
        }

        public string HyperlinkReferenceLanguage
        {
            get
            {
                return GetAttribute("hreflang");
            }
        }

        public string Title
        {
            get
            {
                return GetAttribute("title");
            }
        }

        public Single Length
        {
            get
            {
                if(HasAttribute("length"))
                    return Single.Parse(GetAttribute("length"));
                return -1;
            }
        }
    }
}
