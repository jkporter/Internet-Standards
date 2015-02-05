using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace InternetStandards.Ietf.Atom
{
    public class AtomDateConstruct : AtomElement
    {
        internal AtomDateConstruct(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse(InnerText);
            }
        }
    }
}