using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace InternetStandards.Ietf.Atom
{
    public class AtomPersonConstruct : AtomElement
    {
        internal AtomPersonConstruct(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public string PersonName
        {
            get
            {
                return GetInnerText("name");
            }
            set
            {
                var atomNameElement = GetAtomElement("name") ??
                                      (AtomElement)AppendChild(new AtomElement(null, "name", AtomNS, OwnerDocument));
                atomNameElement.InnerText = value;
            }
        }

        public System.Uri PersonUri
        {
            get
            {
                return GetUri("uri");
            }
            set
            {
                var atomNameElement = GetAtomElement("uri") ??
                                      (AtomElement)AppendChild(new AtomElement(null, "uri", AtomNS, OwnerDocument));
                atomNameElement.InnerText = value.ToString();
            }
        }

        public MailAddress PersonEmail
        {
            get
            {
                return new MailAddress(GetInnerText("email"), PersonName);
            }
            set
            {
                var atomNameElement = GetAtomElement("email") ??
                                      (AtomElement)AppendChild(new AtomElement(null, "email", AtomNS, OwnerDocument));
                atomNameElement.InnerText = value.ToString();
            }
        }
    }
}
