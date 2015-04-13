using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Atom
{
    public class AtomCategoryElement : AtomElement
    {
        internal AtomCategoryElement(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public string Term
        {
            get
            {
                return GetAttribute("term");
            }
        }

        public string Scheme
        {
            get
            {
                return GetAttribute("scheme");
            }
        }

        public string Label
        {
            get
            {
                return GetAttribute("label");
            }
        }
    }
}
