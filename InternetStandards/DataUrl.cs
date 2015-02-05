using System;
using System.Web;
using System.Runtime.Serialization;
using InternetStandards.Ietf.Mail.Mime;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
namespace InternetStandards
{
    public class DataUrl : Uri
    {
        private MediaType mediaType;
        private byte[] data;

        public DataUrl(string dataUriString)
            : base(dataUriString)
        {
            Parse();
        }

        protected DataUrl(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            Parse();
        }

        public MediaType MediaType
        {
            get
            {
                return mediaType;
            }
        }

        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        private new void Parse()
        {
            if (Scheme.ToLowerInvariant() != "data")
                throw new FormatException("URI scheme must be 'data'.");

            int dataDelIndex = AbsoluteUri.IndexOf(',');
            if (dataDelIndex == -1)
                throw new FormatException("Not a valid data URI. Missing ','.");

            string mediaType = AbsoluteUri.Substring(5, dataDelIndex - 5);
            if (mediaType.EndsWith(";base64"))
            {
                mediaType = mediaType.Remove(mediaType.Length - 7);
                data = Convert.FromBase64String(AbsoluteUri.Remove(0, dataDelIndex + 1));
            }
            else
            {
                data = HttpUtility.UrlDecodeToBytes(AbsoluteUri.Remove(0, dataDelIndex + 1), Encoding.ASCII);
            }

            if (mediaType == string.Empty)
                mediaType = "text/plain;charset=US-ASCII";
            else if (mediaType.StartsWith(";") && HttpUtility.UrlDecode(mediaType).ToLowerInvariant().Contains(";charset="))
                mediaType = "text/plain" + mediaType;

            string type = null, subtype = null;
            NameValueCollection parameters = null;

            Regex mediaTypePattern = new Regex("(?<type>[^/;]+)/(?<subtype>[^/;]+)(;(?<parameter>(?<attribute>[^=;]+)=(?<value>[^=;]+)))*");
            Match m = mediaTypePattern.Match(mediaType);
            if (!m.Success)
                throw new FormatException("MediaType is not valid.");

            type = HttpUtility.UrlDecode(m.Groups["type"].Value);
            subtype = HttpUtility.UrlDecode(m.Groups["subtype"].Value);
            if (m.Groups["parameter"].Success)
            {
                parameters = new NameValueCollection();
                for (int i = 0; i < m.Groups["attribute"].Captures.Count; i++)
                {
                    string value = HttpUtility.UrlDecode(m.Groups["value"].Captures[i].Value);
                    if (value.StartsWith("\""))
                        value = value.Substring(1, value.Length - 2);
                    parameters.Add(HttpUtility.UrlDecode(m.Groups["attribute"].Captures[i].Value), HttpUtility.UrlDecode(m.Groups["value"].Captures[i].Value));
                }
            }

            this.mediaType = new MediaType(type, subtype, parameters);
        }
    }
}

