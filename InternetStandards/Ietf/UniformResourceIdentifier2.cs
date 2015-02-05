using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class UniformResourceIdentifier2
    {
        protected string absoluteUri = null;

        protected string scheme = null;

        protected string hierarchicalPart = null;

        protected string authority = null;

        protected string userInformation = null;
        protected string host = null;
        protected UniformResourceIdentifierHostType hostType = UniformResourceIdentifierHostType.Unknown;
        protected string port = null;

        protected string path = string.Empty;
        protected string query = null;
        protected string fragment = null;

        #region "Constructors"

        public UniformResourceIdentifier2(string uriString)
        {
            Parse(uriString);
        }

        public UniformResourceIdentifier2(string uriReference, UniformResourceIdentifier2 baseUri)
        {
            Match match =
                new Regex(
                    @"(?<relativeRef>^(?<relativePart>(?://(?<authority>(?:(?<userInfo>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)*)@)?(?<host>(?<ipLiteral>\[(?:(?<ipV6Address>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){6}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){5}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){4}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,1}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){3}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){2}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,3}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,4}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,5}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,6}))?::)|(?<ipVFuture>v(?<hexDig>[a-fA-F\d])+\.(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)+))])|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))|(?<regName>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=]))*))(?::(?<port>\d*))?)(?<path>(?<pathAbEmpty>(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)))|(?<path>(?<pathAbsolute>/(?:(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)?)|(?<pathNoScheme>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|@)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)|(?<pathEmpty>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@){0})))(?:\?(?<query>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?(?:#(?<fragment>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?$)")
                    .Match(uriReference);
            if (match.Success)
            {
                if (match.Groups["authority"].Success)
                {
                    authority = GetGroupValueByName(match, "authority");
                    userInformation = GetGroupValueByName(match, "userInfo");
                    host = GetGroupValueByName(match, "host");
                    hostType = GetHostTypeByRegExMatch(match);
                    port = GetGroupValueByName(match, "port");
                    path = RemoveDotSegments(GetPathByRegExMatch(match));
                    query = GetGroupValueByName(match, "query");
                }
                else
                {
                    if (match.Groups["pathEmpty"].Success)
                    {
                        path = baseUri.path;
                        if (match.Groups["query"].Success)
                            query = GetGroupValueByName(match, "query");
                        else
                            query = baseUri.query;
                    }
                    else
                    {
                        string pathByRegExMatch = GetPathByRegExMatch(match);
                        if (pathByRegExMatch.StartsWith("/"))
                            path = RemoveDotSegments(pathByRegExMatch);
                        else
                            path = RemoveDotSegments(Merge(baseUri, pathByRegExMatch));
                        query = GetGroupValueByName(match, "query");
                    }
                    authority = baseUri.authority;
                    userInformation = baseUri.userInformation;
                    host = baseUri.host;
                    hostType = baseUri.hostType;
                    port = baseUri.port;
                }
                scheme = baseUri.scheme;

                StringBuilder builder = new StringBuilder();

                if (authority != null)
                {
                    builder.Append("//");
                    builder.Append(authority);
                }
                builder.Append(path);
                hierarchicalPart = builder.ToString();

                builder.Insert(0, scheme + ':');
                if (query != null)
                {
                    builder.Append('?');
                    builder.Append(query);
                }
                absoluteUri = builder.ToString();

                fragment = GetGroupValueByName(match, "fragment");
            }
            else
            {
                Parse(uriReference);
            }
        }

        #endregion

        #region "Properties"

        public string AbsoluteUri
        {
            get
            {
                return absoluteUri;
            }
        }

        public string Scheme
        {
            get
            {
                return scheme;
            }
        }

        public string HierarchicalPart
        {
            get
            {
                return hierarchicalPart;
            }
        }

        public virtual string Authority
        {
            get
            {
                return authority;
            }
        }

        public virtual string UserInformation
        {
            get
            {
                return userInformation;
            }
        }

        public virtual string Host
        {
            get
            {
                return host;
            }
        }

        public UniformResourceIdentifierHostType HostType
        {
            get
            {
                return hostType;
            }
        }

        public virtual string Port
        {
            get
            {
                return port;
            }
        }

        public virtual int? PortValue
        {
            get
            {
                if (string.IsNullOrEmpty(port))
                    return null;

                return int.Parse(port);
            }
        }

        public virtual string Path
        {
            get
            {
                return path;
            }
        }

        public virtual string[] Segments
        {
            get
            {
                if (path == string.Empty)
                    return null;

                if (path[0] == '/')
                    return path.Remove(0, 1).Split('/');

                return path.Split('/');
            }
        }

        public virtual string Query
        {
            get
            {
                return query;
            }
        }

        public virtual string Fragment
        {
            get
            {
                return fragment;
            }
        }
        #endregion

        public override int GetHashCode()
        {
            return this.Normalize().ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((UniformResourceIdentifier2)obj);
        }

        public bool Equals(UniformResourceIdentifier2 uri)
        {
            return Normalize().ToString() == uri.Normalize().ToString();
        }

        public virtual UniformResourceIdentifier2 Normalize()
        {
            string authorityNormalized = authority;
            if (authorityNormalized != null)
            {
                StringBuilder normalizedAuthorityBuilder = new StringBuilder();
                if (userInformation != null)
                {
                    normalizedAuthorityBuilder.Append(userInformation);
                    normalizedAuthorityBuilder.Append('@');
                }
                normalizedAuthorityBuilder.Append(host.ToLowerInvariant());
                if (!string.IsNullOrEmpty(port))
                {
                    normalizedAuthorityBuilder.Append(':');
                    normalizedAuthorityBuilder.Append(port);
                }
                authorityNormalized = normalizedAuthorityBuilder.ToString();
            }

            return new UniformResourceIdentifier2(PercentEncodingCaseAndPercentEncodingNormalize(RecomposeUriComponents(scheme.ToLowerInvariant(), authorityNormalized, RemoveDotSegments(path), query, fragment)));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(scheme);
            result.Append(':');
            result.Append(hierarchicalPart);
            if (query != null)
            {
                result.Append('?');
                result.Append(query);
            }
            if (fragment != null)
            {
                result.Append('#');
                result.Append(fragment);
            }

            return result.ToString();
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

        protected static UniformResourceIdentifier2 TransformReferences(UriReference r)
        {
            /* UniformResourceIdentifier t = new UniformResourceIdentifier();

            if (r.scheme != null)
            {
                t.scheme = r.scheme;
                t.authority = r.authority;
                t.path = remove_dot_segments(r.path);
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
                        if (r.path.startsWith("/"))
                        {
                            t.path = RemoveDotSegments(r.path);
                        }
                        else
                        {
                            t.path = Merge(Base.path, r.path);
                            t.path = RemoveDotSegments(t.path);
                        }
                        t.query = r.query;
                    }
                    t.authority = Base.authority;
                }
                t.scheme = Base.scheme;
            }

            t.fragment = r.fragment; */

            return null;
        }

        protected static string Merge(UniformResourceIdentifier2 baseUri, string relativePathReference)
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
        #endregion

        #region "Private Utility Functions"

        private void Parse(string uriString)
        {
            Match match = new Regex(@"(?<uri>^(?<absoluteUri>(?<scheme>[a-zA-Z][a-zA-Z\d+-,]*):(?<hierPart>(?://(?<authority>(?:(?<userInfo>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)*)@)?(?<host>(?<ipLiteral>\[(?:(?<ipV6Address>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){6}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){5}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])))|(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){4}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,1}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){3}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){2}(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,3}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,4}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<ls32>(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))))?|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,5}(?<h16>(?<hexDig>[a-fA-F\d]){1,4}))?::(?<h16>(?<hexDig>[a-fA-F\d]){1,4})|(?:(?:(?<h16>(?<hexDig>[a-fA-F\d]){1,4}):){0,2}(?<h16>(?<hexDig>[a-fA-F\d]){1,6}))?::)|(?<ipVFuture>v(?<hexDig>[a-fA-F\d])+\.(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:)+))])|(?<ipV4Address>(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35])\.(?<decOctet>\d|[\x31-\x39]\d|1\d{2}|2[\x30-\x34]|25[\x30-\x35]))|(?<regName>(?:(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=]))*))(?::(?<port>\d*))?)(?<path>(?<pathAbEmpty>(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)))|(?<path>(?<pathAbsolute>/(?:(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)?)|(?<pathRootless>(?<segmentNz>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)+)+(?:/(?<segment>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)*))*)|(?<pathEmpty>(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@){0})))(?:\?(?<query>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?)(?:#(?<fragment>(?:(?<pchar>(?<unreserved>[-a-zA-Z\d._~])|(?<pctEncoded>%[a-fA-F0-9]{2})|(?<subDelims>[!$&'()*+,;=])|:|@)|/|\?)*))?$)").Match(uriString);
            if (!match.Success)
                throw new FormatException("Not a valid URI");

            absoluteUri = GetGroupValueByName(match, "absoluteUri");
            scheme = GetGroupValueByName(match, "scheme");
            hierarchicalPart = GetGroupValueByName(match, "hierPart");
            authority = GetGroupValueByName(match, "authority");
            userInformation = GetGroupValueByName(match, "userInfo");
            host = GetGroupValueByName(match, "host");
            hostType = GetHostTypeByRegExMatch(match);
            port = GetGroupValueByName(match, "port");
            path = GetGroupValueByName(match, "path"); ;
            query = GetGroupValueByName(match, "query");
            fragment = GetGroupValueByName(match, "fragment");
        }

        private static string GetGroupValueByName(Match match, string groupName)
        {
            if (match.Groups[groupName].Success)
                return match.Groups[groupName].Value;
            return null;
        }

        private static UniformResourceIdentifierHostType GetHostTypeByRegExMatch(Match match)
        {
            if (match.Groups["ipV6Address"].Success)
                return UniformResourceIdentifierHostType.IPV6Address;
            if (match.Groups["ipVFuture"].Success)
                return UniformResourceIdentifierHostType.IPVFuture;
            if (match.Groups["ipV4Address"].Success)
                return UniformResourceIdentifierHostType.IPV4Address;
            if (match.Groups["regName"].Success)
                return UniformResourceIdentifierHostType.RegisteredName;
            return UniformResourceIdentifierHostType.None;
        }

        private static string GetPathByRegExMatch(Match match)
        {
            if (match.Groups["pathAbEmpty"].Success)
                return match.Groups["pathAbEmpty"].Value;
            if (match.Groups["pathAbsolute"].Success)
                return match.Groups["pathAbsolute"].Value;
            if (match.Groups["pathNoScheme"].Success)
                return match.Groups["pathNoScheme"].Value;
            if (match.Groups["pathRootless"].Success)
                return match.Groups["pathRootless"].Value;
            return string.Empty;
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

        protected string PercentEncodingCaseAndPercentEncodingNormalize(string s)
        {
            Regex percentEncodingPattern = new Regex("%([0-9A-Fa-f]{2})");
            return percentEncodingPattern.Replace(s, new MatchEvaluator(PercentEncodingCaseAndPercentEncodingNormalize));
        }

        private string PercentEncodingCaseAndPercentEncodingNormalize(Match m)
        {
            char c = (char)Convert.ToByte(m.Captures[0].Value, 16);
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '-' || c == '.' || c == '_' || c == '~')
                return new string(c, 1);
            return m.Value.ToUpperInvariant();
        }
        #endregion
    }

   
}
