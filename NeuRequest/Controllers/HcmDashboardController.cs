using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    [Authorize]
    public class HcmDashboardController : Controller
    {
        // GET: HcmDashboard
        public ActionResult Index()
        {
            if(Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if(currentUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getUserHcmActiveRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }


        public ActionResult LeaveCancelation()
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles =  new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["LeaveCancelationUiRender"] = new LeaveCancelationUiRender();
            return View();
        }
        
        [HttpPost]
        public ActionResult LeaveCancelation(LeaveCancelationUiRender leaveCancelationUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveCancelationUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
                    NeuLeaveCancelation neuLeaveCancelation = new NeuLeaveCancelation();
                    neuLeaveCancelation.UserId = currentUser.Id;
                    neuLeaveCancelation.RequestId = newRequestId;
                    neuLeaveCancelation.Message = leaveCancelationUiRender.message;
                    neuLeaveCancelation.StartDate = leaveCancelationUiRender.leaveStartDate;
                    neuLeaveCancelation.EndDate = leaveCancelationUiRender.leaveEndDate;
                    neuLeaveCancelation.AddedOn = dateCreated;
                    neuLeaveCancelation.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addNewLeaveCancelation(neuLeaveCancelation);
                    if(newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 1;
                        nueRequestMaster.RequestStatus = 1;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = 1;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if(newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leaveCancelationUiRender.leaveCancelationApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                }
                else
                {
                    ViewData["UserMasterList"] = userProfiles;
                    ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                    ViewData["LeaveCancelationUiRender"] = leaveCancelationUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["LeaveCancelationUiRender"] = leaveCancelationUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

            // GET: HcmDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HcmDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HcmDashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HcmDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HcmDashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HcmDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HcmDashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
