using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class UriReference
    {
        protected internal Match uriReferenceMatch = null;
        public UriReference(string uriReferenceString)
        {
            uriReferenceMatch = new Regex(@"(?<uriReference>(?<uri>^(?<absoluteUri>(?<scheme>[a-zA-Z][a-zA-Z\d+-,]*):(?<hierPart>(?://(?<authority>(?:(?<userInfo>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)*)@)?(?<host>(?<ipLiteral>\[(?:(?<ipV6Address>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){6}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){5}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){4}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,1}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){3}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){2}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,3}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,4}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,5}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,6}))?::)|(?<ipVFuture>v(?<hexDig>[a-fA-F\d])+\.(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)+))])|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))|(?<regName>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=]))*))(?::(?<port>\d*))?)(?<path>(?<pathAbEmpty>(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)))|(?<path>(?<pathAbsolute>/(?:(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)?)|(?<pathRootless>(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)|(?<pathEmpty>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@){0})))(?:\?(?<query>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?)(?:#(?<fragment>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?$)|(?<relativeRef>^(?<relativePart>(?://(?<authority>(?:(?<userInfo>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)*)@)?(?<host>(?<ipLiteral>\[(?:(?<ipV6Address>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){6}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){5}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){4}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,1}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){3}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){2}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,3}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,4}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,5}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,6}))?::)|(?<ipVFuture>v(?<hexDig>[a-fA-F\d])+\.(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)+))])|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))|(?<regName>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=]))*))(?::(?<port>\d*))?)(?<path>(?<pathAbEmpty>(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)))|(?<path>(?<pathAbsolute>/(?:(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)?)|(?<pathNoScheme>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|@)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)|(?<pathEmpty>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@){0})))(?:\?(?<query>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?(?:#(?<fragment>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?$))").Match(uriReferenceString);
            if (!uriReferenceMatch.Success)
                throw new FormatException("Not a valid URI Reference");

            scheme = GetGroupValueByName(uriReferenceMatch, "scheme");
            authority = GetGroupValueByName(uriReferenceMatch, "authority");
            path = GetGroupValueByName(uriReferenceMatch, "path");
            query = GetGroupValueByName(uriReferenceMatch, "query");
            fragment = GetGroupValueByName(uriReferenceMatch, "fragment");

            uriReferenceType = uriReferenceMatch.Groups["uri"].Success ? UriReferenceType.Uri : UriReferenceType.RelativeReference;
        }

        protected internal UriReference(UriReference uriReference)
        {
            scheme = uriReference.scheme;
            authority = uriReference.authority;
            path = uriReference.path;
            query = uriReference.query;
            fragment = uriReference.fragment;

            uriReferenceType = uriReference.Type;
        }

        protected internal UriReference()
        {
        }

        #region "Properties"
        protected string scheme = null;
        public string Scheme
        {
            get
            {
                return scheme;
            }
        }

        protected string authority = null;
        public virtual string Authority
        {
            get
            {
                return authority;
            }
        }

        protected string path = string.Empty;
        public virtual string Path
        {
            get
            {
                return path;
            }
        }

        protected string query = null;
        public virtual string Query
        {
            get
            {
                return query;
            }
        }

        protected string fragment = null;
        public virtual string Fragment
        {
            get
            {
                return fragment;
            }
        }

        protected UriReferenceType uriReferenceType = UriReferenceType.Unknown;
        public UriReferenceType Type
        {
            get
            {
                return uriReferenceType;
            }
        }
        #endregion

        public override string ToString()
        {
            return RecomposeUriComponents(scheme, authority, path, query, fragment);
        }

        public static string RecomposeUriComponents(string scheme, string authority, string path, string query, string fragment)
        {
            StringBuilder result = new StringBuilder();

            if (scheme != null)
            {
                result.Append(scheme);
                result.Append(':');
            }

            if (authority != null)
            {
                result.Append("//");
                result.Append(authority);
            }

            result.Append(path);

            if (query != null)
            {
                result.Append("?");
                result.Append(query);
            }

            if (fragment != null)
            {
                result.Append("#");
                result.Append(fragment);
            }

            return result.ToString();
        }

        #region "Relative Resolution"
        protected static Uri TransformReferences(UriReference r, Uri Base)
        {
            Uri t = new Uri();

            if (r.scheme != null)
            {
                t.scheme = r.scheme;
                t.authority = r.authority;
                t.path = RemoveDotSegments(r.path);
                t.query = r.query;
            }
            else
            {
                if (r.authority != null)
                {
                    t.authority = r.authority;
                    t.path = RemoveDotSegments(r.path);
                    t.query = r.query;
                }
                else
                {
                    if (r.path == string.Empty)
                    {
                        t.path = Base.path;
                        if (r.query != null)
                            t.query = r.query;
                        else
                            t.query = Base.query;
                    }
                    else
                    {
                        if (r.path.StartsWith("/"))
                        {
                            t.path = RemoveDotSegments(r.path);
                        }
                        else
                        {
                            t.path = Merge(Base, r.path);
                            t.path = RemoveDotSegments(t.path);
                        }
                        t.query = r.query;
                    }
                    t.authority = Base.authority;
                }
                t.scheme = Base.scheme;
            }

            t.fragment = r.fragment;

            return null;
        }

        protected static string Merge(Uri baseUri, string relativePathReference)
        {
            if (baseUri.Authority != null && baseUri.path == string.Empty)
                return ("/" + relativePathReference);

            int rightMostSlash = baseUri.path.LastIndexOf('/');
            if (rightMostSlash == -1)
                return relativePathReference;

            return (baseUri.path.Substring(0, rightMostSlash + 1) + relativePathReference);
        }

        protected static string RemoveDotSegments(string path)
        {
            string inputBuffer = path;
            StringBuilder outputBuffer = new StringBuilder(path.Length);

            while (inputBuffer.Length > 0)
            {
                int length = 0;
                if (inputBuffer.StartsWith("../"))
                    inputBuffer = inputBuffer.Remove(0, 3);
                else if (inputBuffer.StartsWith("./"))
                    inputBuffer = inputBuffer.Remove(0, 2);
                else if (StartsWithSegment(inputBuffer, "/.", out length))
                    inputBuffer = '/' + inputBuffer.Remove(0, length);
                else if (StartsWithSegment(inputBuffer, "/..", out length))
                {
                    inputBuffer = '/' + inputBuffer.Remove(0, length);
                    int startIndex = outputBuffer.ToString().LastIndexOf('/');
                    if (startIndex == -1) startIndex = 0;
                    outputBuffer.Remove(startIndex, outputBuffer.Length - startIndex);
                }
                else if (inputBuffer == "." || inputBuffer == "..")
                    inputBuffer = string.Empty;
                else
                {
                    length = inputBuffer.Length;

                    int stopIndex;
                    if (inputBuffer.Length > 1 && (stopIndex = inputBuffer.IndexOf('/', 1)) != -1)
                        length = stopIndex;

                    outputBuffer.Append(inputBuffer.Substring(0, length));
                    inputBuffer = inputBuffer.Remove(0, length);
                }
            }

            return outputBuffer.ToString();
        }
        
        private static bool StartsWithSegment(string s, string segment, out int length)
        {
            length = 0;
            if (s == segment)
            {
                length = segment.Length;
                return true;
            }
            else if (s.StartsWith(segment) && s[segment.Length] == '/')
            {
                length = segment.Length + 1;
                return true;
            }

            return false;
        }
        #endregion

        protected internal static string GetGroupValueByName(Match match, string groupName)
        {
            if (match.Groups[groupName].Success)
                return match.Groups[groupName].Value;
            return null;
        }
    }

    public enum UriReferenceType
    {
        Unknown,
        None,
        Uri,
        RelativeReference
    }
}
