using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace InternetStandards.JsonML
{
    public class JsonMLReader :XmlReader
    {
        private JsonTextReader _reader;
        public JsonMLReader()
        {
            _reader.Read();
            

        }

        private int _attributeCount;
        public override int AttributeCount
        {
            get { return _attributeCount; }
        }

        public override string BaseURI
        {
            get { throw new NotImplementedException(); }
        }

        public override int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EOF
        {
            get { throw new NotImplementedException(); }
        }

        public override string GetAttribute(int i)
        {
            throw new NotImplementedException();
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            throw new NotImplementedException();
        }

        public override string GetAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmptyElement
        {
            get { throw new NotImplementedException(); }
        }

        private string _localName;
        public override string LocalName
        {
            get { return _localName; }
        }

        public override string LookupNamespace(string prefix)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToElement()
        {
            throw new NotImplementedException();
        }

        public override bool MoveToFirstAttribute()
        {
            throw new NotImplementedException();
        }

        public override bool MoveToNextAttribute()
        {
            throw new NotImplementedException();
        }

        public override XmlNameTable NameTable
        {
            get { throw new NotImplementedException(); }
        }

        public override string NamespaceURI
        {
            get { throw new NotImplementedException(); }
        }

        XmlNodeType _nodeType = XmlNodeType.None;
        public override XmlNodeType NodeType
        {
            get { return _nodeType; }
        }

        public override string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Read()
        {
            _reader.Read();
            switch (_reader.TokenType)
            {
                case JsonToken.StartArray:
                    _nodeType = XmlNodeType.Element;
                    _reader.Read();

                    break;
            }

            /* XmlNameTable d = new XmlNameTable();
            d.Add( */

            return true;
        }

        public override bool ReadAttributeValue()
        {
            throw new NotImplementedException();
        }

        public override ReadState ReadState
        {
            get { throw new NotImplementedException(); }
        }

        public override void ResolveEntity()
        {
            throw new NotImplementedException();
        }

        public override string Value
        {
            get { throw new NotImplementedException(); }
        }
    }
}
