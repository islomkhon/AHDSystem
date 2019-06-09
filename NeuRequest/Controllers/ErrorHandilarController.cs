using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    public class ErrorHandilarController : Controller
    {
        // GET: ErrorHandilar
        public ActionResult OpError(String message, String errorCode)
        {
            TempData["Message"] = message;
            TempData["ErrorCode"] = errorCode;
            return View("~/Views/ErrorHandilar/OpError.cshtml");
        }

        public ActionResult AccessError(String message, String errorCode)
        {
            TempData["Message"] = message;
            TempData["ErrorCode"] = errorCode;
            return View("~/Views/ErrorHandilar/OpError.cshtml");
        }
        
    }
}
