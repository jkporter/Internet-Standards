using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DDay.iCal;

namespace InternetStandards.Ietf.xCal
{
    public class xCalConverter
    {
        public void Convert(DDay.iCal.iCalendar iCalendar, Stream s)
        {
            xCalWriter writer = new xCalWriter(s, Encoding.UTF8);
            writer.WriteStartElement("icalendar", xCalWriter.iCalendar20Namespace);

            WriteComponent(iCalendar, writer);

            writer.WriteEndDocument();

            writer.Close();

            writer.Dispose();
        }

        private void WriteComponent(ICalendarComponent component, XmlWriter writer)
        {
            writer.WriteStartElement(component.Name.ToLowerInvariant(), xCalWriter.iCalendar20Namespace);

            if (component.Properties.Any())
            {
                writer.WriteStartElement("properties", xCalWriter.iCalendar20Namespace);
                foreach (var property in component.Properties)
                {
                    writer.WriteStartElement(property.Name.ToLowerInvariant(), xCalWriter.iCalendar20Namespace);
                    if (property.Parameters.Any())
                    {
                        writer.WriteStartElement("parameters", xCalWriter.iCalendar20Namespace);
                        foreach (var paramenter in property.Parameters.Where(p => p.Name != "VALUE"))
                        {
                            writer.WriteStartElement(paramenter.Name.ToLowerInvariant(), xCalWriter.iCalendar20Namespace);
                            WriteValue(paramenter.Value, writer);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }

                    WriteValue(property.Value, writer);



                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            var childComponents = component.Children.OfType<ICalendarComponent>();
            if (childComponents.Any())
            {
                writer.WriteStartElement("components", xCalWriter.iCalendar20Namespace);
                foreach (var childComponent in component.Children.OfType<ICalendarComponent>())
                {
                    WriteComponent(childComponent, writer);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        private void WriteValue(object value, XmlWriter writer)
        {
            if (value == null)
                value = string.Empty;
            if (value is string)
            {
                var @stringValue = (string)value;
                writer.WriteStartElement("text", xCalWriter.iCalendar20Namespace);
                writer.WriteAttributeString("xml", "space", null, "preserve");
                writer.WriteValue(@stringValue);
                writer.WriteEndElement();
                return;
            }

            if (value is iCalDateTime)
            {
                var dateTimeValue = (iCalDateTime)value;
                if (dateTimeValue.HasDate && dateTimeValue.HasTime)
                    writer.WriteElementString("date-time", SerializeDateTime(dateTimeValue));
                else if (dateTimeValue.HasDate)
                    writer.WriteElementString("date", SerializeDateTime(dateTimeValue));
                else if (dateTimeValue.HasTime)
                    writer.WriteElementString("time", SerializeDateTime(dateTimeValue));
                return;
            }

            if (value is UTCOffset)
            {
                var utcOffsetValue = (UTCOffset)value;
                writer.WriteElementString("utc-offset", utcOffsetValue.ToString().Insert(3, ":"));
                return;
            }

            if (value is TimeSpan)
            {
                var timeSpanValue = (TimeSpan)value;
                StringBuilder sb = new StringBuilder();

                if (timeSpanValue.Ticks < 0)
                {
                    sb.Append('-');
                    timeSpanValue = timeSpanValue.Negate();
                }

                sb.Append('P');
                if (timeSpanValue.Days > 0)
                {
                    sb.Append(timeSpanValue.Days);
                    sb.Append('D');
                }

                if (timeSpanValue.Hours > 0 || timeSpanValue.Minutes > 0 || timeSpanValue.Seconds > 0)
                {
                    sb.Append('T');
                }

                if (timeSpanValue.Hours > 0)
                {
                    sb.Append(timeSpanValue.Hours);
                    sb.Append('H');
                }

                if (timeSpanValue.Minutes > 0)
                {
                    sb.Append(timeSpanValue.Minutes);
                    sb.Append('M');
                }

                if (timeSpanValue.Seconds > 0)
                {
                    sb.Append(timeSpanValue.Seconds);
                    sb.Append('S');
                }


                writer.WriteElementString("duration", sb.ToString());
                return;
            }

            if (value is PeriodList)
            {
                var periodListValue = (PeriodList)value;
                var period = periodListValue[0];

                writer.WriteStartElement("period");
                if (period.StartTime != null)
                    writer.WriteElementString("start", SerializeDateTime(period.StartTime));

                if (period.EndTime != null)
                    writer.WriteElementString("end", SerializeDateTime(period.EndTime));

                return;
            }

            if (value is RecurrencePattern)
            {
                var recurrencePatternValue = (RecurrencePattern)value;
                foreach (var parts in recurrencePatternValue.ToString().Split(';').Select(rule => rule.Split(new[] { '=' }, 2)))
                    writer.WriteElementString(parts[0].ToLowerInvariant(), parts[1]);

                return;
            }

            if (value is GeographicLocation)
            {
                var geographicLocationValue = (GeographicLocation)value;
                writer.WriteElementString("latitude", geographicLocationValue.Latitude.ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString("longitude", geographicLocationValue.Longitude.ToString(CultureInfo.InvariantCulture));
                return;
            }

            if (value is Attachment)
            {
                var attachmentValue = (Attachment)value;
                WriteValue(attachmentValue.Uri == null ? attachmentValue.Data : (object)attachmentValue.Uri, writer);
                return;
            }

            if (value is byte[])
            {
                var byteArrayValue = (byte[])value;
                writer.WriteElementString("binary", System.Convert.ToBase64String(byteArrayValue));
                return;
            }

            if (value is Uri)
            {
                var byteArrayValue = (Uri)value;
                writer.WriteElementString("uri", value.ToString());
                return;
            }

            if (value is int)
            {
                var intValue = (int)value;
                writer.WriteElementString("integer", intValue.ToString());
                return;
            }

            if (value is float)
            {
                var floatValue = (float)value;
                writer.WriteElementString("float", floatValue.ToString());
                return;
            }

            writer.WriteElementString("unknown", value.GetType().ToString());
        }

        private string SerializeDateTime(IDateTime value)
        {
            var stringBuilder = new StringBuilder(20);
            if (value.HasDate)
                stringBuilder.Append(value.Value.ToString("yyyy-MM-dd"));

            if (value.HasDate && value.HasTime)
                stringBuilder.Append('T');

            if (value.HasTime)
                stringBuilder.Append(value.Value.ToString("HH:mm:ss"));

            if (value.IsUniversalTime)
                stringBuilder.Append('Z');

            return stringBuilder.ToString();
        }
    }
}
