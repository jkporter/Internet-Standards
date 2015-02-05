using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace InternetStandards.Ietf.WebDav
{
    class WebDavHandlerFactory : IHttpHandlerFactory
    {
        #region IHttpHandlerFactory Members

        IHttpHandler IHttpHandlerFactory.GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IHttpHandlerFactory.ReleaseHandler(IHttpHandler handler)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
