using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    public class SearchResultsController : Controller
    {
        // GET: SearchResults
        public ActionResult Request(string q)
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
            ViewData["query"] = q;
            var userAceess = currentUser.userAccess;
            bool ishcm = false;
            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                ishcm = true;
            }
            if (q.StartsWith("#"))
            {
                List<UserRequest> userRequestsRaws = new DataAccess().getRequestDetailsSearchById(q.Replace("#",""));
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getAllRequestAccessList();
                List<RequestSearchRender> requestSearchRenders = new List<RequestSearchRender>();
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                if (userRequestsRaws != null && userRequestsRaws.Count > 0)
                {
                    for (int i = 0; i < userRequestsRaws.Count; i++)
                    {
                        bool userAccess = false;
                        bool isOwner = false;
                        bool isApprover = false;
                        UserRequest userRequest = userRequestsRaws[i];
                        if (ishcm)
                        {
                            userAccess = true;
                        }
                        else
                        {
                            if ((userRequest.RequestSubType != "LeaveWFHApply"
                            && userRequest.RequestSubType != "LeaveBalanceEnquiry"
                            && userRequest.RequestSubType != "HCMAddressProof"
                            && userRequest.RequestSubType != "HCMEmployeeVerification")
                            && (nueRequestAceessLogs.Where(x => (x.RequestId == userRequest.NueRequestMasterId && x.UserId == currentUser.Id && x.OwnerId != currentUser.Id)).Count() > 0))
                            {
                                userAccess = true;
                                isApprover = true;
                            }

                            if (!userAccess)
                            {
                                if (userRequest.OwnerId == currentUser.Id)
                                {
                                    isOwner = true;
                                    userAccess = true;
                                }
                            }
                        }

                        if (userAccess)
                        {
                            requestSearchRenders.Add(new RequestSearchRender(userRequest, ishcm, isOwner, isApprover));
                        }

                    }
                }
                ViewData["SearchResultObject"] = requestSearchRenders;
                ViewData["SearchResultStr"] = new Utils().generateRequestSearchUiRender(requestSearchRenders, userProfiles);
            }
            else
            {//not implimented
                ViewData["SearchResultObject"] = new List<RequestSearchRender>();
                ViewData["SearchResultStr"] = "<div class=\"col-12 results\">\r\n" +
                    "                        <div class=\"pt-4 border-bottom\">\r\n" +
                    "                            <p class=\"page-description mt-1 w-75 text-muted\"> No Data Avilable for search query</p>\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>";
            }
            return View();
        }
    }
}
