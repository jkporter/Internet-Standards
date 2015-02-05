using System;
using System.Collections.Generic;
using System.Text;
using InternetStandards.Ietf.Mail.Mime;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Web;

namespace InternetStandards.Ietf
{
    public class DataUrl : Uri
    {
        public DataUrl()
            : base(null)
        {
        }

        protected string mediaType = null;
        public string MediaType
        {
            get
            {
                return mediaType;
            }
        }

        protected string type = null;
        public string Type
        {
            get
            {
                return null;
            }
        }

        protected string subtype = null;
        public string Subtype
        {
            get
            {
                return null;
            }
        }

        protected NameValueCollection parameters = null;
        public NameValueCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        protected MediaType mediaTypeValue = null;
        public MediaType MediaTypeValue
        {
            get
            {
                return mediaTypeValue;
            }
        }

        protected byte[] data = null;
        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        private void Parse()
        {
            if (Scheme.ToLowerInvariant() != "data")
                throw new FormatException("URI scheme must be 'data'.");

            int dataDelIndex = AbsoluteUri.IndexOf(',');
            if (dataDelIndex == -1)
                throw new FormatException("Not a valid data URI. Missing ','.");

            string mediaTypeValueString = AbsoluteUri.Substring(5, dataDelIndex - 5);
            if (mediaTypeValueString.EndsWith(";base64"))
            {
                mediaTypeValueString = mediaTypeValueString.Remove(mediaType.Length - 7);
                data = Convert.FromBase64String(AbsoluteUri.Remove(0, dataDelIndex + 1));
            }
            else
            {
                Regex dataRegEx = new Regex("((%[A-Fa-f0-9]{2})|.)*");
                string[] octets = dataRegEx.Split(AbsoluteUri.Remove(0, dataDelIndex + 1));
                using (MemoryStream ms = new MemoryStream(octets.Length))
                {
                    foreach (string octet in octets)
                    {
                        if (octet.StartsWith("%"))
                            ms.WriteByte(byte.Parse(octet.Substring(1), NumberStyles.AllowHexSpecifier));
                        else
                            ms.WriteByte((byte)octet[0]);
                    }
                    data = ms.ToArray();
                }
            }

            this.mediaType = mediaTypeValueString;

            if (mediaTypeValueString == string.Empty)
                mediaTypeValueString = "text/plain;charset=US-ASCII";
            else if (mediaType.StartsWith(";") && HttpUtility.UrlDecode(mediaType).ToLowerInvariant().Contains(";charset="))
                mediaTypeValueString = "text/plain" + mediaType;

            string type = null, subtype = null;
            NameValueCollection parameters = null;

            int mediaTypeSubTypeDividerIndex = mediaTypeValueString.IndexOf('/');
            int parameterStartIndex = mediaTypeValueString.IndexOf(';');

            string[] mediaTypesSplit;
            string parametersString = null;
            if (parameterStartIndex == -1)
            {
                mediaTypesSplit = mediaTypeValueString.Split(new char[] { '/' });
            }
            else
            {
                mediaTypesSplit = mediaTypeValueString.Remove(parameterStartIndex).Split(new char[] { '/' });
                parametersString = mediaTypeValueString.Substring(parameterStartIndex);
            }

            type = HttpUtility.UrlDecode(mediaTypesSplit[0]);
            subtype = HttpUtility.UrlDecode(mediaTypesSplit[1]);

            Regex token = new Regex(@"^[^\x00-\x20\x7F()<>@,;:\\""/[\]\?=]+$");

            if (mediaTypesSplit.Length != 2 || !token.IsMatch(type) || !token.IsMatch(subtype))
                throw new FormatException("MediaType is not valid.");

            if (!string.IsNullOrEmpty(parametersString))
            {
                parameters = new NameValueCollection();
                using (StringReader reader = new StringReader(parametersString))
                {
                    int value = reader.Read();
                    char c = (char)value;
                    do
                    {
                        if (c != ';')
                            throw new FormatException("MediaType is not valid.");
                        
                        StringBuilder attributeBuilder = new StringBuilder();
                        while ((value = reader.Read()) != -1 && (c = (char)value) != '=')
                            attributeBuilder.Append(c);

                        string attribute = HttpUtility.UrlDecode(attributeBuilder.ToString());
                        if (c != '=' || !token.IsMatch(attribute))
                            throw new FormatException("MediaType is not valid.");

                        StringBuilder valueBuilder = new StringBuilder();
                        while ((value = reader.Read()) != -1 && (c = (char)value) != ';')
                            valueBuilder.Append(c);

                        string encodedParameterValue = valueBuilder.ToString();

                        string parameterValue = null;
                        if (Regex.IsMatch(encodedParameterValue, @"^(?:(?:%[A-Fa-f0-9]{2})|[^\x00-\x20\x7F()<>@,;:\\""/[\]\?=])*$"))
                        {
                            parameterValue = HttpUtility.UrlDecode(encodedParameterValue);   
                        }
                        else if (encodedParameterValue.StartsWith("%22") && reader.Peek() != -1)
                        {
                            string encodedParametersEnd = reader.ReadToEnd();
                            string parametersEnd = HttpUtility.UrlDecode(reader.ReadToEnd());

                            Match m = Regex.Match(parametersEnd, @"((?:^"")|(?:[^\\]""))[;$]");
                            if(!m.Success)
                                throw new FormatException("MediaType is not valid.");

                            parameterValue = Regex.Replace(HttpUtility.UrlDecode(encodedParameterValue.Remove(0, 3)) + parametersEnd.Remove(m.Index + m.Length - 1), @"\(.)", "$2");
                        }
                        else
                        {
                            throw new FormatException("MediaType is not valid.");
                        }

                        if(!Regex.IsMatch(parameterValue, @"[\x00-\x7F]*"))
                            throw new FormatException("MediaType is not valid.");

                        parameters.Add(attribute, parameterValue);
                    }
                    while (value != -1);
                }
            }

            mediaTypeValue = new MediaType(type, subtype, parameters);
        }
    }
}
