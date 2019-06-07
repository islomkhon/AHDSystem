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

        [HandleError(View = "InternalError")]
        public ActionResult Process(string notificationId)
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
                int notificationIdInt = 0;
                try
                {
                    notificationIdInt = int.Parse(notificationId);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid Request");
                }
                messagesModel.MessageID = notificationIdInt;

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
                    throw new Exception("Invalid Opration");
                    //TempData["Message"] = "Invalid Opration";
                    //return RedirectToAction("OpError", "ErrorHandilar", new { message = "Invalid Opration" });
                }
                return View();
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                throw new Exception(e.Message);
                //return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access" });
            }
        }
    }
}
