using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace OpenAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC on Mono!";
            return View();
        }

        public ActionResult Authenticate(Axioms.AuthenticationHelper.Networks network)
        {
            string authString = Url.Action("AuthenticationResult", "Home", new { authType = network }, "http");
            var client = Axioms.AuthenticationHelper.GetClient(network);
            client.RequestAuthentication(HttpContext, new Uri(authString));
            return null;
        }

        [HttpGet]
        public ActionResult AuthenticationResult(Axioms.AuthenticationHelper.Networks authType)
        {
            DotNetOpenAuth.AspNet.AuthenticationResult res;

            string authString = Url.Action("AuthenticationResult", "Home", new { authType = authType }, "http");
            VerifyAuthentication(authType, out res, new Uri(authString));

            if (!res.IsSuccessful)
            {
                throw new ApplicationException("Authentication Provider Error: it seems that an error occured while validating your auth provider's response.");
            }
            else
            {
                return Redirect("/home");
            }
        }

        private void VerifyAuthentication(Axioms.AuthenticationHelper.Networks authType, out DotNetOpenAuth.AspNet.AuthenticationResult res, Uri authUrl)
        {
            var client = Axioms.AuthenticationHelper.GetClient(authType);
            if (authType == OpenAuth.Axioms.AuthenticationHelper.Networks.Facebook)
                res = (client as DotNetOpenAuth.AspNet.Clients.FacebookClient).VerifyAuthentication(HttpContext, authUrl);
            else
                res = client.VerifyAuthentication(HttpContext);
        }
    }
}

