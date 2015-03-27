using System;
using System.IO;
using System.Text;
using System.Xml;
using DDay.iCal;

namespace InternetStandards.Ietf.xCal
{
    public class xCalWriter:XmlTextWriter
    {
        public const string iCalendar20Namespace = "urn:ietf:params:xml:ns:icalendar-2.0";

        public xCalWriter(Stream w, Encoding encoding)
            : base(w, encoding)
        {
        }

        public xCalWriter(string filename, Encoding encoding)
            : base(filename, encoding)
        {
        }

        public xCalWriter(TextWriter w)
            : base(w)
        {
        }

        public override void WriteValue(DateTime value)
        {
            WriteElementString("date-time", value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        public override void WriteValue(string value)
        {
            WriteElementString("text", value);
        }

        public override void WriteValue(DateTimeOffset value)
        {
            if(value.Offset.Ticks == 0)
                WriteValue(value.DateTime);

            throw new Exception();
        }

        public override void WriteValue(int value)
        {
            WriteIntegerValue(value);
        }

        public void WriteTextValue(string value)
        {
            WriteElementString("text", value);
        }


        public void WriteDateValue(iCalDateTime value)
        {
            WriteElementString("date", value.Value.ToString("yyyy-MM-dd"));
        }

        public void WriteDateTimeValue(iCalDateTime value)
        {
            WriteElementString("date-time", value.Value.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }

        public void WriteTimeValue(iCalDateTime value)
        {
            WriteElementString("time", value.Value.ToString("HH:mm:ssK"));
        }

        public void WriteTimeValue(UTCOffset value)
        {
            WriteElementString("utc-offset", value.ToString().Insert(3, ":"));
        }

        public void WriteDuration(TimeSpan value)
        {
            var sb = new StringBuilder();

            if (value.Ticks < 0)
            {
                sb.Append('-');
                value = value.Negate();
            }

            sb.Append('P');
            if (value.Days > 0)
            {
                sb.Append(value.Days);
                sb.Append('D');
            }

            if (value.Hours > 0 || value.Minutes > 0 || value.Seconds > 0)
            {
                sb.Append('T');
            }

            if (value.Hours > 0)
            {
                sb.Append(value.Hours);
                sb.Append('H');
            }

            if (value.Minutes > 0)
            {
                sb.Append(value.Minutes);
                sb.Append('M');
            }

            if (value.Seconds > 0)
            {
                sb.Append(value.Seconds);
                sb.Append('S');
            }


            WriteElementString("duration", sb.ToString());
        }

        public void WriteIntegerValue(int value)
        {
            WriteElementString("integer", value.ToString());
        }
    }
}
