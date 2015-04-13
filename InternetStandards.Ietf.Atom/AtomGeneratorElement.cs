using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Atom
{
    public class AtomGeneratorElement : AtomElement
    {
        internal AtomGeneratorElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public Uri Uri
        {
            get
            {
                return HasAttribute("uri") ? new Uri(GetAttribute("uri")) : null;
            }
        }

        public string Version
        {
            get
            {
                return GetAttribute("version");
            }
        }

        public string Text
        {
            get
            {
                return InnerText;
            }
        }
    }
}
