using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class Uri : UriReference
    {
        public Uri(string uriString)
            : base(uriString)
        {
            if (Type != UriReferenceType.Uri)
                throw new FormatException("Not a valid URI");

            absoluteUri = GetGroupValueByName(uriReferenceMatch, "absoluteUri");

            hierarchicalPart = GetGroupValueByName(uriReferenceMatch, "hierPart");

            authority = GetGroupValueByName(uriReferenceMatch, "authority");
            userInformation = GetGroupValueByName(uriReferenceMatch, "userInfo");
            host = GetGroupValueByName(uriReferenceMatch, "host");
            hostType = GetHostTypeByRegExMatch(uriReferenceMatch);
            port = GetGroupValueByName(uriReferenceMatch, "port");
            
            if (path != string.Empty)
                segments = (path[0] == '/' ? path.Remove(0, 1) : path).Split('/');
        }

        public Uri(UriReference uriReference, Uri baseUri)
            : base(uriReference)
        {
        }

        internal Uri()
            : base()
        {
        }

        #region "Properties"
        protected string absoluteUri = null;
        public string AbsoluteUri
        {
            get
            {
                return absoluteUri;
            }
        }

        protected string hierarchicalPart = null;
        public string HierarchicalPart
        {
            get
            {
                return hierarchicalPart;
            }
        }

        protected string userInformation = null;
        public virtual string UserInformation
        {
            get
            {
                return userInformation;
            }
        }

        protected string host = null;
        public virtual string Host
        {
            get
            {
                return host;
            }
        }

        protected UniformResourceIdentifierHostType hostType = UniformResourceIdentifierHostType.Unknown;
        public UniformResourceIdentifierHostType HostType
        {
            get
            {
                return hostType;
            }
        }

        protected string port = null;
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
                if (!string.IsNullOrEmpty(port))
                    return int.Parse(port);
                return null;
            }
        }

        protected string[] segments = null;
        public virtual string[] Segments
        {
            get
            {
                return segments;
            }
        }
        #endregion

        public virtual Uri Normalize()
        {
            string normalizedScheme, normalizedUserInformation, normalizedHost, normalizedPort, normalizedPath, normalizedQuery, normalizedFragment;
            InternalNormalize(out normalizedScheme, out normalizedUserInformation, out normalizedHost, out normalizedPort, out normalizedPath, out normalizedQuery, out normalizedFragment, false);

            var normalizedAuthority = new StringBuilder(normalizedHost);
            if (normalizedUserInformation != null)
            {
                normalizedAuthority.Insert(0, "@");
                normalizedAuthority.Insert(0, normalizedUserInformation);
            }
            if (normalizedPort != null)
            {
                normalizedAuthority.Append(":");
                normalizedAuthority.Append(port);
            }

            return new Uri(RecomposeUriComponents(normalizedScheme, normalizedAuthority.ToString(), normalizedPath, normalizedQuery, normalizedFragment));
        }

        protected void InternalNormalize(out string scheme, out string userInformation, out string host, out string port, out string path, out string query, out string fragment)
        {
            InternalNormalize(out scheme, out userInformation, out host, out port, out path, out query, out fragment, false);
        }

        protected void InternalNormalize(out string scheme, out string userInformation, out string host, out string port, out string path, out string query, out string fragment, bool removeEmptyPort)
        {
            scheme = this.scheme.ToLowerInvariant();
            userInformation = this.userInformation  == null ? null : PercentEncodingCaseAndPercentEncodingNormalize(this.userInformation);
            host = this.host == null ? null : PercentEncodingCaseAndPercentEncodingNormalize(this.host.ToLowerInvariant());
            port = this.port == null ? null : removeEmptyPort && this.port == string.Empty ? null : this.port;
            path = this.path == null ? null : PercentEncodingCaseAndPercentEncodingNormalize(RemoveDotSegments(this.path));
            query = this.query == null ? null : PercentEncodingCaseAndPercentEncodingNormalize(this.query);
            fragment = this.fragment == null ? null : PercentEncodingCaseAndPercentEncodingNormalize(this.fragment);
        }

        public override int GetHashCode()
        {
            return Normalize().ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((Uri)obj);
        }

        public bool Equals(Uri uri)
        {
            return Normalize().ToString() == uri.Normalize().ToString();
        }

        protected static string PercentEncodingCaseAndPercentEncodingNormalize(string s)
        {
            var percentEncodingPattern = new Regex("%([0-9A-Fa-f]{2})");
            return percentEncodingPattern.Replace(s, (match) =>
            {
                var c = (char)Convert.ToByte(match.Captures[0].Value, 16);
                return ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '-' ||
                        c == '.' || c == '_' || c == '~')
                    ? new string(c, 1)
                    : match.Value.ToUpperInvariant();
            });
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
            var groupNames = new[] { "pathAbEmpty", "pathAbsolute", "pathNoScheme", "pathRootless" };
            foreach (var groupName in groupNames.Where(groupName => match.Groups[groupName].Success))
                return match.Groups[groupName].Value;

            return string.Empty;
        }
    }

    public enum UniformResourceIdentifierHostType
    {
        Unknown,
        None,
        IPV6Address,
        IPVFuture,
        IPV4Address,
        RegisteredName
    }
}
