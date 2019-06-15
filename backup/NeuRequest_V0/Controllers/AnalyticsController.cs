using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace NeuRequest.Controllers
{
    [Authorize]
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult Index()
        {
            if (Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            string AnalyticsPath = ConfigurationManager.AppSettings["Analytics"].ToString();
            if (Session["UserAnalyticsSession"] == null)
            {
                Session["UserAnalyticsSession"] = true;
                return Redirect(AnalyticsPath+"?module=Login&action=logme&login=root&password=a8b6bb3b34339f5a6d439a0ef7fc7878");
            }
            else
            {
                return Redirect(AnalyticsPath);
            }
        }
    }
}