using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace InternetStandards.Ietf.Http
{
    public class MediaRange : IComparable<MediaRange>, IComparable
    {
        protected string internalType = null;
        protected string internalSubtype = null;
        protected Parameter[] internalParameters = null;
        protected MediaRangeMatchType internalSpecificy;

        public MediaRange(string mediaRange)
        {
            const string mediaRangePattern = "(\\*/\\*|(?<type>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)/\\*|(?<type>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)/(?<subtype>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+))(?<parameter>(?:(?:\\x0D\\x0A)?[\\x20\\x09]+)*;(?:(?:\\x0D\\x0A)?[\\x20\\x09]+)*(?<attribute>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+)=(?<value>[\\x21\\x23-\\x27\\x2A\\x2B\\x2D\\x2E\\x30-\\x39\\x41-\\x5A\\x5E-\\x7E]+|\"(?:(?:[\\x20\\x21\\x23-\\xFF]|(?:(?:\\x0D\\x0A)?[\\x20\\x09]+))|\\\\[\\x00-\\x7F])*\"))*";
            Regex mediaRangeRegEx = new Regex(mediaRangePattern);
            Match match = mediaRangeRegEx.Match(mediaRange);
            if (match.Success)
            {
                if (match.Groups["subtype"].Success)
                {
                    internalType = match.Groups["type"].Value;
                    internalSubtype = match.Groups["subtype"].Value;
                    internalSpecificy = MediaRangeMatchType.MediaType;
                }
                else if (match.Groups["type"].Success)
                {
                    internalType = match.Groups["type"].Value;
                    internalSpecificy = MediaRangeMatchType.Type;
                }
                else
                {
                    internalSpecificy = MediaRangeMatchType.All;
                }
                Group parameterGroup = match.Groups["parameter"];
                if (match.Groups["parameter"].Success)
                {
                    internalSpecificy += 1;
                    CaptureCollection attributes = match.Groups["attribute"].Captures;
                    CaptureCollection values = match.Groups["value"].Captures;
                    ArrayList parameters = new ArrayList();
                    for (int i = 0; i < match.Groups["parameter"].Length; i++)
                        parameters.Add(new Parameter(attributes[i].Value, values[i].Value));
                }
            }
            else
            {
                new MediaRangeFormatException(mediaRange);
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

        public MediaRangeMatchType Specificy
        {
            get
            {
                return internalSpecificy;
            }
        }

        public int CompareTo(MediaRange other)
        {
            int compare = this.Specificy - other.Specificy;
            /* if (compare == 0)
                compare = this.Parameters.Length - other.Parameters.Length; */

            if (compare > 0)
                return 1;
            if (compare < 0)
                return -1;

            return 0;
        }

        public MediaRangeMatchType Match(MediaType mediaType)
        {
            bool matches = Specificy < MediaRangeMatchType.Type
                || (this.Type.ToLowerInvariant() == mediaType.Type.ToLowerInvariant() 
                  && (Specificy < MediaRangeMatchType.MediaType
                  || this.Subtype.ToLowerInvariant() == mediaType.Subtype.ToLowerInvariant()));
                
            if (matches)
            {
                if (!(Parameters == null))
                {
                    foreach (Parameter mediaRangeParamenter in Parameters)
                    {
                        bool found = false;
                        foreach (Parameter mediaTypeParamenter in mediaType.Parameters)
                        {
                            found = mediaTypeParamenter.Attribute == mediaRangeParamenter.Attribute && mediaTypeParamenter.Value == mediaRangeParamenter.Value;
                            if (found)
                                break;
                        }

                        if (!found)
                            return MediaRangeMatchType.None;
                    }
                    return Specificy + 1;
                }
                return Specificy;
            }

            return MediaRangeMatchType.None;
        }

        public bool IsMatch(MediaType mediaType)
        {
            return Match(mediaType) > MediaRangeMatchType.None;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (!(obj is MediaRange))
                throw new Exception();

            return CompareTo((MediaRange)obj);
        }

        #endregion
    }

    public enum MediaRangeMatchType
    {
        Unknown = -1,
        None = 0,
        All = 1,
        Parameters = 2,
        Type = 3,
        TypeAndParameters = 4,
        MediaType = 5,
        MediaTypeWithParameters = 6
    }
}
