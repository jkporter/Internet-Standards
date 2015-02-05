using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace InternetStandards.Ietf.Mail.Mime
{
    public class MediaType
    {
        protected string type;
        protected string subtype;
        protected NameValueCollection parameters = new NameValueCollection();

        public const string MediaTypeApplication = "application";
        public const string MediaTypeAudio = "audio";
        public const string MediaTypeImage = "image";
        public const string MediaTypeMultipart = "multipart";
        public const string MediaTypeText = "text";
        public const string MediaTypeVideo = "video";

        public MediaType(string type, string subtype, NameValueCollection parameters)
        {
            this.type = type;
            this.subtype = subtype;
            this.parameters = parameters;
        }

        public MediaType(string type, string subtype)
            : this(type, subtype, null)
        {
        }

        public MediaType(string type, string subtype, string[] paramNames, string[] paramValues)
            : this(type, subtype)
        {
            for (int i = 0; i < paramNames.Length; i++)
                parameters.Add(paramNames[i], GetValue(paramValues, i));
        }
        
        public string Type
        {
            get
            {
                return type;
            }
        }

        public string Subtype
        {
            get
            {
                return subtype;
            }
        }

        public NameValueCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        private static string GetValue(string[] paramValues, int index)
        {
            if(paramValues == null || paramValues.GetUpperBound(0) > index)
                return null;

            return paramValues[index];
        }
    }
}
