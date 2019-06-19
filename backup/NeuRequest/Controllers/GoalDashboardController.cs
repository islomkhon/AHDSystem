using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    public class GoalDashboardController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getUserHcmActiveRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }
    }
}
