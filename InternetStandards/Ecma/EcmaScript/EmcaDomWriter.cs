using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using InternetStandards.Ecma.EcmaScript;

namespace InternetStandards.Ecma.EcmaScript
{
    public class EmcaDomWriter:XmlWriter
    {
        TextWriter writer;
        public EmcaDomWriter(TextWriter writer)
        {
            writer.WriteLine("var activeNode = document.createDocumentFragment();");
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override string LookupPrefix(string ns)
        {
            throw new NotImplementedException();
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteCData(string text)
        {
            writer.WriteLine("activeNode.appendChild(document.createCDATASection(" + EmcaEncode(text) + "));");
        }

        public override void WriteCharEntity(char ch)
        {
            throw new NotImplementedException();
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteComment(string text)
        {
            writer.WriteLine("activeNode.appendChild(document.createComment(" + EmcaEncode(text) + "));");
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            throw new NotImplementedException();
        }

        public override void WriteEndAttribute()
        {
            writer.WriteLine("activeNode = activeNode.parentNode;");
        }

        public override void WriteEndDocument()
        {
            throw new NotImplementedException();
        }

        public override void WriteEndElement()
        {
            writer.WriteLine("activeNode = activeNode.parentNode;");
        }

        public override void WriteEntityRef(string name)
        {
            writer.WriteLine("activeNode.appendChild(document.createEntityReference(" + EmcaEncode(name) + "));");
        }

        public override void WriteFullEndElement()
        {
            throw new NotImplementedException();
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            writer.WriteLine("activeNode.appendChild(document.createProcessingInstruction(" + EmcaEncode(name) + ", " + EmcaEncode(text) + "));");
        }

        public override void WriteRaw(string data)
        {
            throw new NotImplementedException();
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            string qualifiedName = localName;
            if (prefix != null) qualifiedName = prefix + ':' + qualifiedName;

            writer.WriteLine("activeNode = activeNode.appendChild(document.createAttributeNS(" + EmcaEncode(ns) + ", " + EmcaEncode(qualifiedName) + "));");
        }

        public override void WriteStartDocument(bool standalone)
        {
            throw new NotImplementedException();
        }

        public override void WriteStartDocument()
        {
            throw new NotImplementedException();
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            string qualifiedName = localName;
            if (prefix != null) qualifiedName = prefix + ':' + qualifiedName;

            writer.WriteLine("activeNode = activeNode.appendChild(document.createElementNS(" + EmcaEncode(ns) + ", " + EmcaEncode(qualifiedName) + "));");
        }

        public override WriteState WriteState
        {
            get { throw new NotImplementedException(); }
        }

        public override void WriteString(string text)
        {
            writer.WriteLine("activeNode.appendChild(document.createTextNode(" + EmcaEncode(text) + "));");
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            throw new NotImplementedException();
        }

        public override void WriteWhitespace(string ws)
        {
            throw new NotImplementedException();
        }

        private string EmcaEncode(string s)
        {
            return EmcaScriptUtility.ToStringLiteral(s, EmcaScriptUtility.EcmaScriptStringLiteralCharacters.DoubleString);
        }
    }
}
