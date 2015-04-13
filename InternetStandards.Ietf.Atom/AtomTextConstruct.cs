using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace InternetStandards.Ietf.Atom
{
    public class AtomTextConstruct : AtomElement
    {
        internal AtomTextConstruct(string prefix, string localname, string ns, AtomDocument doc)
            : base(prefix, localname, ns, doc)
        {
        }

        public TextType Type
        {
            get
            {
                if (HasAttribute("type"))
                    switch (GetAttribute("type"))
                    {
                        case "text":
                            return TextType.Text;
                        case "html":
                            return TextType.Html;
                        case "xhtml":
                            return TextType.Xhtml;
                    }

                return TextType.Text;
            }
        }
        public string Text
        {
            get
            {
                if (Type == TextType.Text)
                    return InnerText;
                else if (Type == TextType.Html)
                    return InnerText;
                else if (Type == TextType.Xhtml)
                    return FirstChild.InnerXml;

                return null;
            }
        }
    }

    public enum TextType
    {
        Xhtml, Html, Text
    }
}