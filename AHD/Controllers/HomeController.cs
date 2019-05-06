using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AHD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string userName = string.Empty;
            string userSessionName = string.Empty;            
            if (System.Web.HttpContext.Current != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                userName = System.Web.HttpContext.Current.User.Identity.Name;
                string userId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                Session["isLoggedIn"] = true;
                Session["userEmail"] = userName;
                Session["userSessionId"] = userId;
            }
            else
            {
                Session["isLoggedIn"] = false;
                Session["userEmail"] = null;
                Session["userSessionId"] = null;
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}