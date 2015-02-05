using System;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using WriteState = System.Xml.WriteState;

namespace InternetStandards.JsonML
{
    public class JsonMLWriter : XmlWriter
    {
        private readonly JsonTextWriter _writer;
        private readonly StringBuilder _sb = new StringBuilder();

        public JsonMLWriter(JsonTextWriter output)
        {
            _writer = output;
        }

        public override void WriteStartDocument()
        {
        }

        public override void WriteStartDocument(bool standalone)
        {
        }

        public override void WriteEndDocument()
        {
            _writer.WriteEnd();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            if (_writeState == WriteState.Attribute)
                _writer.WriteEndObject();
            _writer.WriteStartArray();
            _writer.WriteValue(string.IsNullOrEmpty(prefix) ? localName : prefix + ":" + localName);
            _writeState = WriteState.Element;
        }

        public override void WriteEndElement()
        {
            if (_writeState == WriteState.Attribute)
                _writer.WriteEndObject();
            if (_sb.Length > 0)
            {
                _writer.WriteValue(_sb.ToString());
                _sb.Clear();
            }
            _writer.WriteEndArray();
            _writeState = WriteState.Content;
        }

        public override void WriteFullEndElement()
        {
            _writer.WriteEndArray();
            _writeState = WriteState.Content;
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            if (_writeState == WriteState.Element)
                _writer.WriteStartObject();

            _writer.WritePropertyName(string.IsNullOrEmpty(prefix) ? localName : prefix + ":" + localName);
            // _writer.WriteRawValue(new string(_writer.QuoteChar, 1));

            _writeState = WriteState.Attribute;
        }

        public override void WriteEndAttribute()
        {
            // _writer.WriteRaw(new string(_writer.QuoteChar, 1));
            _writer.WriteValue(_sb.ToString());
            _sb.Clear();
        }

        public override void WriteCData(string text)
        {
            // _writer.WriteRaw(text);
            _sb.Append(text);
        }

        public override void WriteComment(string text)
        {
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
        }

        public override void WriteEntityRef(string name)
        {
        }

        public override void WriteCharEntity(char ch)
        {
        }

        public override void WriteWhitespace(string ws)
        {
        }

        public override void WriteString(string text)
        {
            // _writer.WriteRaw(text);
            _sb.Append(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
        }

        public override void WriteRaw(string data)
        {
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
        }

        public override void Close()
        {
            _writer.Close();
        }

        public override void Flush()
        {
            _writer.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            throw new NotImplementedException();
        }

        WriteState _writeState = WriteState.Start;
        public override WriteState WriteState
        {
            get { return _writeState; }
        }
    }
}
