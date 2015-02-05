using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class HttpUrlBase : Uri
    {
        protected HttpUrlBase(string baseHttpUrl, string scheme, int defaultPort)
            : base(baseHttpUrl)
        {
            Init(scheme, defaultPort);
        }

        protected HttpUrlBase(UriReference uriReference, HttpUrlBase baseHttpUrl, string scheme, int defaultPort)
            : base(uriReference, baseHttpUrl)
        {
            Init(scheme, defaultPort);
        }

        private void Init(string scheme, int defaultPort)
        {
            if(this.scheme.ToLowerInvariant() != scheme )
                throw new FormatException();

            /* if (string.IsNullOrEmpty(port))
                portValue = defaultPort; */
        }

        public override Uri Normalize()
        {
            string normalizedScheme, normalizedUserInformation, normalizedHost, normalizedPort, normalizedPath, normalizedQuery, normalizedFragment;
            InternalNormalize(out normalizedScheme, out normalizedUserInformation, out normalizedHost, out normalizedPort, out normalizedPath, out normalizedQuery, out normalizedFragment, false);

            var normalizedAuthority = new StringBuilder(normalizedHost);
            if (!string.IsNullOrEmpty(normalizedPort) && int.Parse(normalizedPort) != PortValue)
            {
                normalizedAuthority.Append(":");
                normalizedAuthority.Append(port);
            }

            return new HttpUrlBase(RecomposeUriComponents(normalizedScheme,
                                                normalizedAuthority.ToString(),
                                                normalizedPath == string.Empty ? "/" : normalizedPath,
                                                normalizedQuery,
                                                normalizedFragment), scheme, PortValue.Value);
        }
    }
}
