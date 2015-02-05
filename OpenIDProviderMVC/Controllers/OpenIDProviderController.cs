using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  InternetStandards.OpenID;

namespace OpenIDProviderMVC.Controllers
{
    public class OpenIDProviderController : Controller
    {
        //
        // GET: /OpenIDProvider/

        [HttpPost]
        public ActionResult Associate()
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            return View();
        }



    }
}
