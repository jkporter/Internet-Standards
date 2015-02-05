using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace InternetStandards.Ietf.WebDav
{
    class WebDavGet : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
