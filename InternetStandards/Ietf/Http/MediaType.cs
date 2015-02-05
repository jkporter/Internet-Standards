using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace InternetStandards.Ietf.Http
{
    public class MediaType
    {
        private static Regex mediaTypeRegEx = null;
        
        protected string internalType = null;
        protected string internalSubtype = null;
        protected Parameter[] internalParameters = null;

        static MediaType()
        {
            const string mediaTypePattern = "(?<type>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)/(?<subtype>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)(?<parameter>(?:(?:\\x0D\\x0A)?[\\x20\\x09]+)*;(?:(?:\\x0D\\x0A)?[\\x20\\x09]+)*(?<attribute>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)=(?<value>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+|\"(?:(?:[\\x20\\x21\\x23-\\xFF]|(?:(?:\\x0D\\x0A)?[\\x20\\x09]+))|\\\\[\\x00-\\x7F])*\"))*";
            mediaTypeRegEx = new Regex(mediaTypePattern, RegexOptions.Compiled);
        }

        public MediaType(string mediaTypeString)
        {   
            Match match = mediaTypeRegEx.Match(mediaTypeString);
            if (match.Success)
            {
                internalType = match.Groups["type"].Value;
                internalSubtype = match.Groups["subtype"].Value;
                Group parameterGroup = match.Groups["parameter"];
                if (match.Groups["parameter"].Success)
                {
                    CaptureCollection attributes = match.Groups["attribute"].Captures;
                    CaptureCollection values = match.Groups["value"].Captures;
                    ArrayList parameters = new ArrayList();
                    internalParameters = new Parameter[match.Groups["parameter"].Captures.Count];
                    for (int i = 0; i < internalParameters.Length; i++)
                        internalParameters[i] = new Parameter(attributes[i].Value, values[i].Value);
                }
            }
            else
            {
                throw new MediaTypeFormatException();
            }
        }

        public string Type
        {
            get
            {
                return internalType;
            }
        }

        public string Subtype
        {
            get
            {
                return internalSubtype;
            }
        }

        public Parameter[] Parameters
        {
            get
            {
                return internalParameters;
            }
        }
    }
}
