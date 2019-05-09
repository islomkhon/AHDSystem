using AHD.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AHD.Controllers
{
    public class HomeController : Controller
    {
        public bool isAuth()
        {
            try
            {
                if (bool.Parse(Session["isLoggedIn"].ToString()) == true 
                    && Session["userEmail"] != null 
                    && Session["userSessionId"] != null 
                    && Session["userProfileSession"] != null
                    && (Session["userEmail"] as string) == (Session["userProfileSession"] as NueUserProfile).email)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Session["userProfileSession"] = null;
                return false;
            }
        }

        public ActionResult Index()
        {
            string userName = string.Empty;
            string userSessionName = string.Empty;            
            if (System.Web.HttpContext.Current != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!isAuth())
                {
                    userName = System.Web.HttpContext.Current.User.Identity.Name.ToLower();
                    string userId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    NueUserProfile nueUserProfile = new MongoCommunicator().getActiveUserData(userName);
                    if (nueUserProfile != null)
                    {
                        Session["isLoggedIn"] = true;
                        Session["userEmail"] = userName.ToLower();
                        Session["userSessionId"] = userId;
                        Session["userProfileSession"] = nueUserProfile;
                        Session["fullName"] = nueUserProfile.fullName;
                        Session["designation"] = nueUserProfile.designation;
                    }
                    else
                    {
                        Session["isLoggedIn"] = false;
                        Session["userEmail"] = null;
                        Session["userSessionId"] = null;
                        Session["userProfileSession"] = null;
                        return RedirectToAction("Login", "Account");
                    }
                }
                
            }
            else
            {
                Session["isLoggedIn"] = false;
                Session["userEmail"] = null;
                Session["userSessionId"] = null;
                Session["userProfileSession"] = null;
                return RedirectToAction("Login", "Account");
            }
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            return View();
        }

        public ActionResult Logout()
        {
            Session["isLoggedIn"] = false;
            Session["userEmail"] = null;
            Session["userSessionId"] = null;
            Session["userProfileSession"] = null;
            return RedirectToAction("Login", "Account");
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