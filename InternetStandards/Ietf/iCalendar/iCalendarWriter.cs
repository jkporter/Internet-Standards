using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
            WriteState = WriteState.Start;
        }

        public iCalendarWriter(Stream input)
            : this(new StreamWriter(input, Encoding.UTF8))
        {
        }

        public WriteState WriteState
        {
            get;
            protected set;
        }

        public void WriteiCalendarObjectBegin()
        {
            _writer.WriteLine("BEGIN:VCALENDAR");
        }

        public void WriteiCalendarObjectEnd()
        {
            _writer.WriteLine("END:VCALENDAR");
        }

        public void WriteContentLine(string name, string value)
        {
            WriteContentLineStart(name);
            WriteValue(value);
            WriteContentLineEnd();
        }

        public void WriteContentLineStart(string name)
        {
            WriteToContentLine(name);
        }

        
        public void WriteStartParamenter(string name)
        {
            WriteToContentLine(";" + name + "=");
            WriteState = WriteState.Parameter;
        }

        public void WriteParamenterValue(string value, bool? quotedString = null)
        {
            value = ParamenterValueEncode(value);
            if (quotedString.GetValueOrDefault((new[] { ':', ';', ',' }).Any(value.Contains)))
                WriteParamenterQuotedStringValue(value);
            else
                WriteParamenterTextValue(value);
        }

        public void WriteParamenter(string name, string value, bool? quotedString = null)
        {
            WriteStartParamenter(name);
            WriteParamenterValue(value, quotedString);
        }

        private void WriteParamenterTextValue(string value)
        {
            WriteParamenterValueInternal(ParamenterValueEncode(value));
        }

        private void WriteParamenterQuotedStringValue(string value)
        {
            WriteParamenterValueInternal('"' + ParamenterValueEncode(value) + '"');
        }

        private void WriteParamenterValueInternal(string value)
        {
            WriteToContentLine(value);
        }

        public void WriteTextValue(string value)
        {
            var regEx = new Regex(@"(\r\n)|\r|\n|[^\s\x21\x23-\x2B\x2D-\x39\x3C-\x5B\x5D-\x7E\u0080-\uFFFF:""]");
            WriteValue(regEx.Replace(value, m =>
            {
                if (m.Value == "\r\n" || m.Value == "\r" || m.Value == "\n" || m.Value == "\u0085" || m.Value == "\u2028")
                    return @"\n";

                if (m.Value == @"\" || m.Value == @";" || m.Value == @",")
                    return @"\" + m.Value;

                return m.Value;
            }));
        }

        public void WriteValue(string value)
        {
            WriteToContentLine(":");
            WriteToContentLine(value);
            WriteState = WriteState.Value;
        }

        public void WriteContentLineEnd()
        {
            _writer.WriteLine();
            LineOctects = 0;
        }

        public void WriteBeginEventComponent()
        {
            _writer.WriteLine("BEGIN:VEVENT");
        }

        public void WriteEndEventComponent()
        {
            _writer.WriteLine("END:VEVENT");
        }

        public void WriteBeginIanaComponent(string ianaToken)
        {
            ComponentIanaTokenOrXName = ianaToken;
            _writer.WriteLine("BEGIN:" + ComponentIanaTokenOrXName);
        }

        public void WriteEndIanaComponent()
        {
            _writer.WriteLine("END:" + ComponentIanaTokenOrXName);
        }

        public void WriteBeginXNameComponent(string xname)
        {
            ComponentIanaTokenOrXName = xname;
            _writer.WriteLine("BEGIN:" + ComponentIanaTokenOrXName);
        }

        public void WriteEndXNameComponent()
        {
            _writer.WriteLine("END:" + ComponentIanaTokenOrXName);
        }

        private string ComponentIanaTokenOrXName { get; set; }

        protected int LineOctects { get; private set; }

        protected void WriteToContentLine(string value)
        {
            var valueByteCount = _writer.Encoding.GetByteCount(value);
            if (LineOctects + valueByteCount > MaxLineOctetCount)
            {
                var line = new StringBuilder(MaxLineOctetCount);
                var textElementEnumerator = StringInfo.GetTextElementEnumerator(value);
                while (textElementEnumerator.MoveNext())
                {
                    var textElementOctets = _writer.Encoding.GetByteCount(textElementEnumerator.GetTextElement());
                    if (LineOctects + textElementOctets > MaxLineOctetCount)
                    {
                        _writer.Write(line.ToString());
                        line.Clear();
                        WriteContentLineFold();
                    }

                    line.Append(textElementEnumerator.GetTextElement());
                    LineOctects += _writer.Encoding.GetByteCount(textElementEnumerator.GetTextElement());

                }
                _writer.Write(line.ToString());
            }
            else
            {
                _writer.Write(value);
                LineOctects += valueByteCount;
            }
        }

        protected void WriteContentLineFold()
        {
            _writer.WriteLine();
            _writer.Write(WhiteSpaceCharacter);
            LineOctects = _writer.Encoding.GetByteCount(new[] { WhiteSpaceCharacter });
        }

        private static string ParamenterValueEncode(string value)
        {
            return Regex.Replace(value, @"(\r\n)|[\n^""]", m =>
            {
                switch (m.Value)
                {
                    case "^":
                        return "^^";
                    case "\"":
                        return "^\"";
                }

                return "^n";
            });
        }
    }

    public enum WriteState
    {
        Start,
        ContentLine,
        Parameter,
        Value
    }
}
