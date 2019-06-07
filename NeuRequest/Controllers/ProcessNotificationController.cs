using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    public class ProcessNotificationController : Controller
    {

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
            ViewData["UserProfileSession"] = currentUser;

            MessagesModel messagesModel = new MessagesModel();
            messagesModel.UserId = currentUser.Id;
            List<MessagesModel> messages = new DataAccess().getAllNotification(messagesModel);
            ViewData["Messages"] = messages;

            return View();
        }

        // GET: ProcessNotification
        public ActionResult Process(int notificationId)
        {
            try
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
                ViewData["UserProfileSession"] = currentUser;

                MessagesModel messagesModel = new MessagesModel();
                messagesModel.MessageID = notificationId;

                MessagesModel messagesModelUpdated = new DataAccess().updateNotification(messagesModel);
                if(messagesModelUpdated != null)
                {
                    if (messagesModelUpdated.Target.StartsWith("/HcmDashboard"))
                    {
                        return Redirect(messagesModelUpdated.Target);
                    }
                    else
                    {
                        ViewData["MessagesModel"] = messagesModelUpdated;
                    }
                }
                else
                {
                    return RedirectToAction("OpError", "ErrorHandilar", new { message = "Invalid Opration" });
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access" });
            }
        }
    }
}
