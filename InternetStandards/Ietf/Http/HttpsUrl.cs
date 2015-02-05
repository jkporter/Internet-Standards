using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class HttpsUrl : HttpUrlBase
    {
        public HttpsUrl(string httpsUrl)
            : base(httpsUrl, "https", 443)
        {

        }

        public HttpsUrl(UriReference uriReference, HttpsUrl baseHttpsUrl)
            : base(uriReference, baseHttpsUrl, "https", 443)
        {
        }

        public override Uri Normalize()
        {
            return new HttpsUrl(base.Normalize().ToString());
        }
    }
}
