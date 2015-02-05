using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Mvp.Xml.Common;

namespace InternetStandards.Xml
{
    public class XhtmlWriter : XmlWrappingWriter
    {
        private Stack<QName> elementStack;
        private XhtmlWriter writer;

        public XhtmlWriter()
            : base(null)
        {
        }

        public override void WriteFullEndElement()
        {
            QName elementName = elementStack.Pop();
            if (elementName.NsUri == "http://www.w3.org/1999/xhtml")
                switch (elementName.Local)
                {
                    case "area":
                    case "base":
                    case "br":
                    case "col":
                    case "hr":
                    case "img":
                    case "input":
                    case "link":
                    case "meta":
                    case "basefont":
                    case "frame":
                    case "isindex":
                    case "param":
                        throw new Exception();
                }

            base.WriteFullEndElement();

        }
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            elementStack.Push(new QName(localName, ns, prefix));
            base.WriteStartElement(prefix, localName, ns);
        }
        public override void WriteEndElement()
        {
            QName elementName = elementStack.Pop();
            if (elementName.NsUri == "http://www.w3.org/1999/xhtml")
                switch (elementName.Local)
                {
                    case "area":
                    case "base":
                    case "br":
                    case "col":
                    case "hr":
                    case "img":
                    case "input":
                    case "link":
                    case "meta":
                    case "basefont":
                    case "frame":
                    case "isindex":
                    case "param":
                        base.WriteRaw(" />");
                        return;
                }

            WriteFullEndElement();
        }
    }
}
