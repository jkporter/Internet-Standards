using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using InternetStandards.Unicode;

namespace InternetStandards.Ietf.iCalendar
{
    public class iCalendarWriter
    {
        private const char WhiteSpaceCharacter = ' ';
        private const int MaxLineOctetCount = 75;
        private readonly TextWriter _writer;

        public iCalendarWriter(TextWriter input)
        {
            _writer = input;
            _writer.NewLine = "\r\n";
        }
        
        public iCalendarWriter(Stream input):this(new StreamWriter(input, Encoding.UTF8))
        {
        }

        public void WriteContentLine(string name, PropertyParameter[] @params, string value)
        {
            var propertyNameParameterListAndValue = (name +
                                                     (@params == null
                                                          ? string.Empty
                                                          : string.Join(";", @params.Select(p => p.ToString()).ToArray())) +
                                                     ":" + value);

            var lineOctetCount = 0;
            foreach(var textElement in (new TextElementEnumerable(propertyNameParameterListAndValue)))
            {
                var octetCount = Encoding.UTF8.GetByteCount(textElement);
                if (lineOctetCount + octetCount > MaxLineOctetCount)
                {
                    _writer.Write("\r\n" + WhiteSpaceCharacter);
                    lineOctetCount = 1;
                }

                _writer.Write(textElement);
                lineOctetCount += octetCount;
            }

            _writer.Write("\r\n");
        }

        public void WriteContentLine(string name, string value)
        {
            WriteContentLine(name, null, value);
        }

        public void WritePropertyStart(string name)
        {

        }

   }
}
