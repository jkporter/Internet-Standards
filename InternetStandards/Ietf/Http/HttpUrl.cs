using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class HttpUrl : HttpUrlBase
    {
        public HttpUrl(string httpUrl)
            : base(httpUrl, "http", 80)
        {
            
        }

        public HttpUrl(UriReference uriReference, HttpUrl baseHttpUrl)
            : base(uriReference, baseHttpUrl, "http", 80)
        {
        }

        public override Uri Normalize()
        {
            return new HttpUrl(base.Normalize().ToString());
        }
    }
}
