using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace InternetStandards.W3c.Html
{
    public static class HtmlUtility
    {
        public static string ContentDispositionHeaderAttachmentEncode(string filename, DateTime? creationDate, DateTime? modificationDate, DateTime? readDate, long? size)
        {
            DateTimeOffset? creationDateTimeOffset = null;
            if (creationDate.HasValue) creationDateTimeOffset = new DateTimeOffset(creationDate.Value);

            DateTimeOffset? modificationDateTimeOffset = null;
            if (modificationDate.HasValue) modificationDateTimeOffset = new DateTimeOffset(modificationDate.Value);

            DateTimeOffset? readDateTimeOffset = null;
            if (readDate.HasValue) readDateTimeOffset = new DateTimeOffset(readDate.Value);

            return ContentDispositionHeaderAttachmentEncode(filename, creationDateTimeOffset, modificationDateTimeOffset, readDateTimeOffset, size);
        }

        public static string ContentDispositionHeaderAttachmentEncode(string filename, DateTimeOffset? creationDate, DateTimeOffset? modificationDate, DateTimeOffset? readDate, long? size)
        {
            return "attachment"
               + EncodeDispositionParamater("filename", filename)
               + EncodeQuotedDateTime("creation-date", creationDate)
               + EncodeQuotedDateTime("modification-date", modificationDate)
               + EncodeQuotedDateTime("read-date", readDate)
               + (size.HasValue ? EncodeDispositionParamater("size", size.Value.ToString()) : string.Empty);
        }

        private static string EncodeDispositionParamater(string name, string value)
        {
            if (value == null)
                return string.Empty;
            return "; " + name + "=\"" + value + "\"";
        }

        private static string EncodeQuotedDateTime(string name, DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null)
                return string.Empty;
            return "; " + name + "=\"" + dateTimeOffset.Value.ToString("r") + "\"";
        }
    }
}
