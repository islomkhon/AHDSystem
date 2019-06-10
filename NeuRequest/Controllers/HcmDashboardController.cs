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

        public ActionResult RequestHistory()
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

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getUserHcmInactiveRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }

        public ActionResult ApprovalInbox()
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

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getHcmActiveApproverRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }

        public ActionResult ApprovalHistory()
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

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getHcmInactiveApproverRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }

        public ActionResult HCMApprovalInbox()
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

            var userAceess = currentUser.userAccess;
            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getHcmActiveApproverFinalRequests(currentUser.Id);
                ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
                ViewData["UserProfileSession"] = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access", errorCode = "404" });
            }
        }

        public ActionResult ApprovalHCMHistory()
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

            var userAceess = currentUser.userAccess;
            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getHcmInactiveApproverFinalRequests(currentUser.Id);
                ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
                ViewData["UserProfileSession"] = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access", errorCode = "404" });
            }
        }

        public ActionResult SelfRequestDetails(String requestId, String message = null)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            ViewData["UserProfileSession"] = currentUser;
            ViewData["RequestId"] = requestId;

            if (message != null)
            {
                TempData["Message"] = message;
            }
            var userAceess = currentUser.userAccess;
            bool userAccess = false;

            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

            UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
            List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
            List<NueRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
            List<AttachmentLogModel> attachmentLogModels = new DataAccess().getAttachmentLogs(requestId);
            if (userRequest == null || userRequest.RequestId == null)
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid Request", errorCode = "404" });
            }
            
            if (adminUsers > 0)
            {
                userAccess = true;
            }

            if(nueRequestAceessLogs.Where(x => (x.UserId == currentUser.Id)).Count() > 0)
            {
                userAccess = true;
            }

            if (userAccess)
            {
                bool isOwner = false;
                bool ishcm = false;
                bool isApprover = false;

                //is owner
                if (userRequest.OwnerId == currentUser.Id)
                {
                    isOwner = true;
                }

                //is hcm group
                if(userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count() > 0)
                {
                    ishcm = true;
                }

                //find user is approver
                if ((userRequest.RequestSubType != "LeaveBalanceEnquiry"
                    && userRequest.RequestSubType != "HCMAddressProof"
                    && userRequest.RequestSubType != "HCMEmployeeVerification")
                    && (nueRequestAceessLogs.Where(x => (x.UserId == currentUser.Id && x.OwnerId != currentUser.Id)).Count() > 0))
                {
                    isApprover = true;
                }

                if(userRequest.RequestSubType == "LeaveCancelation")
                {
                    NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                    string viewRender = new Utils().generateLeaveCancelationUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeaveCancelationModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Cancelation";
                }
                else if (userRequest.RequestSubType == "LeavePastApply")
                {
                    NeuLeavePastApplyModal neuLeavePastApplyModal = new DataAccess().getNeuLeavePastApplyDetails(requestId);
                    string viewRender = new Utils().generateLeavePastApplyUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeavePastApplyModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Past Apply";
                }
                else if (userRequest.RequestSubType == "LeaveWFHApply")
                {
                    NeuLeaveWFHApplyModal neuLeaveWFHApplyModal = new DataAccess().getNeuLeaveWFHApplyDetails(requestId);
                    string viewRender = new Utils().generateLeaveWFHApplyUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeaveWFHApplyModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Work From Home";
                }
                else if (userRequest.RequestSubType == "LeaveBalanceEnquiry")
                {
                    LeaveBalanceEnquiryModal leaveBalanceEnquiryModal = new DataAccess().getNeuLeaveBalanceEnquiryDetails(requestId);
                    string viewRender = new Utils().generateLeaveBalanceEnquiryUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, leaveBalanceEnquiryModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Balance Enquiry";
                }
                else if (userRequest.RequestSubType == "HCMAddressProof")
                {
                    AddressProofModal addressProofModal = new DataAccess().getNeuAddressProofModalDetails(requestId);
                    string viewRender = new Utils().generateAddressProofUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, addressProofModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Address Proof"; 
                }
                else if (userRequest.RequestSubType == "HCMEmployeeVerification")
                {
                    EmployeeVerificationReqModal employeeVerificationReqModal = new DataAccess().getNeuEmployeeVerificationReqModalDetails(requestId);
                    string viewRender = new Utils().generateEmployeeVerificationReqUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, employeeVerificationReqModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Employee Verification";
                }
                else if (userRequest.RequestSubType == "SalaryCertificate")
                {
                    SalaryCertificateModal salaryCertificateModal = new DataAccess().getNeuSalaryCertificateModalDetails(requestId);
                    string viewRender = new Utils().generateSalaryCertificateUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, salaryCertificateModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Salary Certificate";
                }
                else if (userRequest.RequestSubType == "HCMGeneral")
                {
                    GeneralRequestModal generalRequestModal = new DataAccess().getNeuGeneralRequestModalDetails(requestId);
                    string viewRender = new Utils().generateGeneralRequestUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, generalRequestModal, nueRequestAceessLogs, userProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Common Request";
                }
                else
                {
                    return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Coming soon", errorCode = "404" });
                }

            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access", errorCode = "404" });
            }

            return View();
        }

        public FileResult DownloadAttachment(String requestId, String vFile)
        {
            try
            {

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                List<NueRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
                List<AttachmentLogModel> attachmentLogModels = new DataAccess().getAttachmentLogs(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (nueRequestAceessLogs.Where(x => (x.UserId == currentUser.Id && x.OwnerId != currentUser.Id)).Count() > 0)
                {
                    userAccess = true;
                }

                if (userRequest.OwnerId == currentUser.Id)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    var internalFile = attachmentLogModels.Where(x => x.Request == userRequest.RequestId && x.VFileName == vFile);
                    if (internalFile != null && internalFile.Count() > 0)
                    {
                        AttachmentLogModel attachmentLogModel = internalFile.First();
                        string UploadPath = ConfigurationManager.AppSettings["UserFilePath"].ToString();
                        var tempUPath = (UploadPath + requestId).Replace(@"\", "/");
                        byte[] fileBytes = System.IO.File.ReadAllBytes(@UploadPath + requestId + "/" + attachmentLogModel.VFileName);
                        string fileName = attachmentLogModel.FileName + attachmentLogModel.FileExt;
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
















        /********************** General Request ***************************/
        public ActionResult GeneralRequest()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["GeneralRequestUiRender"] = new GeneralRequestUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult GeneralRequest(GeneralRequestUiRender generalRequestUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            TempData["Message"] = null;
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (generalRequestUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMGeneral");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    GeneralRequest generalRequest = new GeneralRequest();
                    generalRequest.UserId = currentUser.Id;
                    generalRequest.RequestId = newRequestId;
                    generalRequest.Message = generalRequestUiRender.message;
                    generalRequest.AddedOn = dateCreated;
                    generalRequest.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addGeneralRequest(generalRequest);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 0;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesTemp)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Common Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited common request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["GeneralRequestUiRender"] = generalRequestUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["GeneralRequestUiRender"] = generalRequestUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawGeneralRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseGeneralRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveGeneralRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NueRequestMaster nueRequestMaster = new NueRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                            nueRequestActivity1.Payload = userComment;
                            nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity1.UserId = currentUser.Id;
                            nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity1.Request = userRequest.RequestId;
                            nueRequestActivity1.AddedOn = dateCreated;
                            nueRequestActivity1.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity1);
                        }

                        nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                        NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Common Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) common request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** General Request ***************************/


        /********************** Salary Certificate ***************************/
        public ActionResult SalaryCertificate()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["SalaryCertificateUiRender"] = new SalaryCertificateUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult SalaryCertificate(SalaryCertificateUiRender salaryCertificateUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            TempData["Message"] = null;
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (salaryCertificateUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "SalaryCertificate");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    SalaryCertificate salaryCertificate = new SalaryCertificate();
                    salaryCertificate.UserId = currentUser.Id;
                    salaryCertificate.RequestId = newRequestId;
                    salaryCertificate.Message = salaryCertificateUiRender.message;
                    salaryCertificate.AddedOn = dateCreated;
                    salaryCertificate.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addSalaryCertificateRequest(salaryCertificate);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 0;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesTemp)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Salary Certificate Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited salary certificate request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["SalaryCertificateUiRender"] = salaryCertificateUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["SalaryCertificateUiRender"] = salaryCertificateUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawSalaryCertificateRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseSalaryCertificateRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveSalaryCertificateRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NueRequestMaster nueRequestMaster = new NueRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                            nueRequestActivity1.Payload = userComment;
                            nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity1.UserId = currentUser.Id;
                            nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity1.Request = userRequest.RequestId;
                            nueRequestActivity1.AddedOn = dateCreated;
                            nueRequestActivity1.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity1);
                        }

                        nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                        NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Salary Certificate Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) salary certificate request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Salary Certificate ***************************/


        /********************** Employee Verification ***************************/
        public ActionResult EmployeeVerificationReq()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["EmployeeVerificationReqUiRender"] = new EmployeeVerificationReqUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeVerificationReq(EmployeeVerificationReqUiRender employeeVerificationReqUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            TempData["Message"] = null;
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (employeeVerificationReqUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMEmployeeVerification");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    EmployeeVerificationReq employeeVerificationReq = new EmployeeVerificationReq();
                    employeeVerificationReq.UserId = currentUser.Id;
                    employeeVerificationReq.RequestId = newRequestId;
                    employeeVerificationReq.Message = employeeVerificationReqUiRender.message;
                    employeeVerificationReq.AddedOn = dateCreated;
                    employeeVerificationReq.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addEmployeeVerificationRequest(employeeVerificationReq);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 0;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesTemp)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Employee Verification Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited employee verification request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["EmployeeVerificationReqUiRender"] = employeeVerificationReqUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["EmployeeVerificationReqUiRender"] = employeeVerificationReqUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawHCMEmployeeVerificationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseHCMEmployeeVerificationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveHCMEmployeeVerificationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NueRequestMaster nueRequestMaster = new NueRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                            nueRequestActivity1.Payload = userComment;
                            nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity1.UserId = currentUser.Id;
                            nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity1.Request = userRequest.RequestId;
                            nueRequestActivity1.AddedOn = dateCreated;
                            nueRequestActivity1.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity1);
                        }

                        nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                        NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Employee Verification Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) employee verification request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Employee Verification ***************************/



        /********************** Address Proof ***************************/
        public ActionResult AddressProof()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["AddressProofUiRender"] = new AddressProofUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult AddressProof(AddressProofUiRender addressProofUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            TempData["Message"] = null;
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (addressProofUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMAddressProof");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    AddressProof addressProof = new AddressProof();
                    addressProof.UserId = currentUser.Id;
                    addressProof.RequestId = newRequestId;
                    addressProof.Message = addressProofUiRender.message;
                    addressProof.AddedOn = dateCreated;
                    addressProof.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addAddressProofRequest(addressProof);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 0;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesTemp)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Address Proof Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited address proof request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["AddressProofUiRender"] = addressProofUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["AddressProofUiRender"] = addressProofUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawHCMAddressProofReqRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseHCMAddressProofRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveHCMAddressProofRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NueRequestMaster nueRequestMaster = new NueRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                            nueRequestActivity1.Payload = userComment;
                            nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity1.UserId = currentUser.Id;
                            nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity1.Request = userRequest.RequestId;
                            nueRequestActivity1.AddedOn = dateCreated;
                            nueRequestActivity1.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity1);
                        }

                        nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                        NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Address Proof Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) address proof request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Address Proof ***************************/



        /********************** Leave Balance Enquiry ***************************/
        public ActionResult LeaveBalanceEnquiry()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["LeaveBalanceEnquiryUiRender"] = new LeaveBalanceEnquiryUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult LeaveBalanceEnquiry(LeaveBalanceEnquiryUiRender leaveBalanceEnquiryUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            TempData["Message"] = null;
            try
            {
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveBalanceEnquiryUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveBalanceEnquiry");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    LeaveBalanceEnquiry leaveBalanceEnquiry = new LeaveBalanceEnquiry();
                    leaveBalanceEnquiry.UserId = currentUser.Id;
                    leaveBalanceEnquiry.RequestId = newRequestId;
                    leaveBalanceEnquiry.Message = leaveBalanceEnquiryUiRender.message;
                    leaveBalanceEnquiry.StartDate = leaveBalanceEnquiryUiRender.leaveStartDate;
                    leaveBalanceEnquiry.EndDate = leaveBalanceEnquiryUiRender.leaveEndDate;
                    leaveBalanceEnquiry.AddedOn = dateCreated;
                    leaveBalanceEnquiry.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addLeaveBalanceEnquiry(leaveBalanceEnquiry);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 0;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();
                            List<MailItem> mailItems = new List<MailItem>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);
                            List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesTemp)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Leave Balance Enquiry";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited leave balance enquiry request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);


                                MailItem mailItem = new MailItem();
                                mailItem.Subject = messagesModel.EmptyMessage;
                                mailItem.Body = mailTemplate;
                                mailItem.To = "monin.jose@neudesic.com";
                                mailItem.Priority = true;
                                mailItems.Add(mailItem);
                            }

                            
                            new DataAccess().addNeuMessagess(messages);
                            new Utils().mailHandilar(mailItems);

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
                    ViewData["LeaveBalanceEnquiryUiRender"] = leaveBalanceEnquiryUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["LeaveBalanceEnquiryUiRender"] = leaveBalanceEnquiryUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawLeaveBalanceEnqRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseBalanceEnqLeaveRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveBalanceEnqLeaveRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NueRequestMaster nueRequestMaster = new NueRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                            nueRequestActivity1.Payload = userComment;
                            nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity1.UserId = currentUser.Id;
                            nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity1.Request = userRequest.RequestId;
                            nueRequestActivity1.AddedOn = dateCreated;
                            nueRequestActivity1.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity1);
                        }

                        nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                        NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Leave Balance Enquiry";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) leave balance enquiry request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("An error occerd");
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Leave Balance Enquiry ***************************/

            

        /********************** Leave WFH Apply ***************************/
        public ActionResult LeaveWFHApply()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["LeaveWFHApplyUiRender"] = new LeaveWFHApplyUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult LeaveWFHApply(LeaveWFHApplyUiRender leaveWFHApplyUiRender)
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveWFHApplyUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveWFHApply");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    LeaveWFHApply leaveWFHApply = new LeaveWFHApply();
                    leaveWFHApply.UserId = currentUser.Id;
                    leaveWFHApply.RequestId = newRequestId;
                    leaveWFHApply.Message = leaveWFHApplyUiRender.message;
                    leaveWFHApply.StartDate = leaveWFHApplyUiRender.leaveStartDate;
                    leaveWFHApply.EndDate = leaveWFHApplyUiRender.leaveEndDate;
                    leaveWFHApply.AddedOn = dateCreated;
                    leaveWFHApply.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addNewLeaveWFHApply(leaveWFHApply);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 1;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leaveWFHApplyUiRender.leaveWFHApplyApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Work From Home";
                            messagesModel.EmptyMessage = currentUser.FullName + " submitted work from home request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = int.Parse(leaveWFHApplyUiRender.leaveWFHApplyApprover);
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);
                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["LeaveWFHApplyUiRender"] = leaveWFHApplyUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["LeaveWFHApplyUiRender"] = leaveWFHApplyUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawLeaveWFHApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseWFHLeaveApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveWFHLeaveRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if (nueRequestAceessLogs.Where(x => x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Work From Home";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) work from home request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SubApproveWFHLeaveRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                if (nueRequestAceessLogs.Where(x => x.UserId == currentUser.Id && x.OwnerId != currentUser.Id).Count() > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    var dateCreated = DateTime.UtcNow;
                    NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if (updated != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Work From Home";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved work from home request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            List<NueRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if (nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                                foreach (var item in userProfilesTemp)
                                {
                                    messagesModel = new MessagesModel();
                                    messagesModel.Message = "Work From Home";
                                    messagesModel.EmptyMessage = userRequest.FullName + " submited work from home request";
                                    messagesModel.Processed = 0;
                                    messagesModel.UserId = item.Id;
                                    messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                                    messagesModel.MessageDate = dateCreated;
                                    messages.Add(messagesModel);
                                }

                            }

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
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
                    throw new Exception("Invalid opration");
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Leave WFH Apply ***************************/



        /********************** Past Leave ***************************/
        public ActionResult LeavePastApply()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["LeavePastApplyUiRender"] = new LeavePastApplyUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult LeavePastApply(LeavePastApplyUiRender leavePastApplyUiRender)
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leavePastApplyUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeavePastApply");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    NeuLeavePastApply neuLeavePastApply = new NeuLeavePastApply();
                    neuLeavePastApply.UserId = currentUser.Id;
                    neuLeavePastApply.RequestId = newRequestId;
                    neuLeavePastApply.Message = leavePastApplyUiRender.message;
                    neuLeavePastApply.StartDate = leavePastApplyUiRender.leaveStartDate;
                    neuLeavePastApply.EndDate = leavePastApplyUiRender.leaveEndDate;
                    neuLeavePastApply.AddedOn = dateCreated;
                    neuLeavePastApply.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addNewLeavePastApply(neuLeavePastApply);
                    if (newRequestInternId != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.RequestId = newRequestId;
                        nueRequestMaster.CreatedBy = currentUser.Id;
                        nueRequestMaster.IsApprovalProcess = 1;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leavePastApplyUiRender.leavePastApplyApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Past Apply";
                            messagesModel.EmptyMessage = currentUser.FullName + " submitted leave past request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = int.Parse(leavePastApplyUiRender.leavePastApplyApprover);
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);
                            new DataAccess().addNeuMessagess(messages);


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
                    ViewData["LeavePastApplyUiRender"] = leavePastApplyUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["LeavePastApplyUiRender"] = leavePastApplyUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawLeavePastApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CloseLeavePastApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveLeavePastApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if(nueRequestAceessLogs.Where(x=>x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Past Apply";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) leave past apply request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SubApproveLeavePastApplyRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                if (nueRequestAceessLogs.Where(x => x.UserId == currentUser.Id && x.OwnerId != currentUser.Id).Count() > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    var dateCreated = DateTime.UtcNow;
                    NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if (updated != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Past Apply";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved leave past apply request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            List<NueRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if (nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                                foreach (var item in userProfilesTemp)
                                {
                                    messagesModel = new MessagesModel();
                                    messagesModel.Message = "Leave Past Apply";
                                    messagesModel.EmptyMessage = userRequest.FullName + " submited leave past apply request";
                                    messagesModel.Processed = 0;
                                    messagesModel.UserId = item.Id;
                                    messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                                    messagesModel.MessageDate = dateCreated;
                                    messages.Add(messagesModel);
                                }

                            }

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
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
                    throw new Exception("Invalid opration");
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }
        /********************** Past Leave End***************************/


        /********************** Leave Cancelation ***************************/
        public ActionResult LeaveCancelation()
        {
            TempData["Message"] = null;
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
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            try
            {
                var dateCreated = DateTime.UtcNow;
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveCancelationUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NueRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveCancelation");
                    NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

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
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.PayloadId = newRequestInternId;
                        nueRequestMaster.RequestCatType = nueRequestCat.Id;
                        nueRequestMaster.AddedOn = dateCreated;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().addNeuRequest(nueRequestMaster);
                        if(newRequestTempInternId != -1)
                        {
                            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
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

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Cancelation";
                            messagesModel.EmptyMessage = currentUser.FullName + " submitted leave cancellation request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = int.Parse(leaveCancelationUiRender.leaveCancelationApprover);
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId="+ newRequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);
                            new DataAccess().addNeuMessagess(messages);


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
        
        [HttpPost]
        public JsonResult WithdrawLeaveCancelationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NueRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id 
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Withdraw");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request withdrawn successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult CloseLeaveCancelationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NueRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if(userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Close");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);
                            return Json(new JsonResponse("Ok", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid Operation, Request is not in approved state"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to close the request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApproveLeaveCancelationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if(nueRequestAceessLogs.Where(x=>x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("HCM Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Cancelation";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) leave cancellation request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            throw new Exception("An error occerd");
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    return null;
                }
                else
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SubApproveLeaveCancelationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if(userComment != null)
                {
                    userComment = userComment.Trim();
                }
                
                NueRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NueRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }
                
                if (nueRequestAceessLogs.Where(x => x.UserId == currentUser.Id && x.OwnerId != currentUser.Id).Count() > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    var dateCreated = DateTime.UtcNow;
                    NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if(updated != -1)
                    {
                        NueRequestMaster nueRequestMaster = new NueRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if(newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NueRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NueRequestActivity nueRequestActivity1 = new NueRequestActivity();
                                nueRequestActivity1.Payload = userComment;
                                nueRequestActivity1.PayloadType = nueRequestCmtActivityMaster.Id;
                                nueRequestActivity1.UserId = currentUser.Id;
                                nueRequestActivity1.RequestId = userRequest.NueRequestMasterId;
                                nueRequestActivity1.Request = userRequest.RequestId;
                                nueRequestActivity1.AddedOn = dateCreated;
                                nueRequestActivity1.ModifiedOn = dateCreated;
                                new DataAccess().addRequestComment(nueRequestActivity1);
                            }

                            nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                            NueRequestActivity nueRequestActivity2 = new NueRequestActivity();
                            nueRequestActivity2.Payload = appCmt;
                            nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                            nueRequestActivity2.UserId = currentUser.Id;
                            nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                            nueRequestActivity2.Request = userRequest.RequestId;
                            nueRequestActivity2.AddedOn = dateCreated;
                            nueRequestActivity2.ModifiedOn = dateCreated;
                            new DataAccess().addRequestComment(nueRequestActivity2);

                            MessagesModel messagesModel = new MessagesModel();
                            messagesModel.Message = "Leave Cancelation";
                            messagesModel.EmptyMessage = currentUser.FullName + " approved leave cancellation request";
                            messagesModel.Processed = 0;
                            messagesModel.UserId = userRequest.OwnerId;
                            messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                            messagesModel.MessageDate = dateCreated;
                            messages.Add(messagesModel);

                            List<NueRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if(nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                                foreach (var item in userProfilesTemp)
                                {
                                    messagesModel = new MessagesModel();
                                    messagesModel.Message = "Leave Cancelation";
                                    messagesModel.EmptyMessage = userRequest.FullName + " submited leave cancellation request";
                                    messagesModel.Processed = 0;
                                    messagesModel.UserId = item.Id;
                                    messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                                    messagesModel.MessageDate = dateCreated;
                                    messages.Add(messagesModel);
                                }

                            }

                            new DataAccess().addNeuMessagess(messages);

                            return Json(new JsonResponse("Ok", "Request approved successfully."), JsonRequestBehavior.AllowGet);
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
                    throw new Exception("Invalid opration");
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
        }

        /********************** Leave Cancelation End***************************/

        [HttpPost]
        public ActionResult AddUserAttachment(NueRequestAttchment nueRequestAttchment)
        {
            string retrunResponse = "";
            string requestId = nueRequestAttchment.requestId;
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || requestId == null
                    || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                NueRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("File");
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (nueRequestAceessLogs.Where(x => (x.UserId == currentUser.Id && x.OwnerId != currentUser.Id)).Count() > 0)
                {
                    userAccess = true;
                }

                if (userRequest.OwnerId == currentUser.Id)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    string FileName = Path.GetFileNameWithoutExtension(nueRequestAttchment.requestAtchmentFile.FileName);
                    string FileExtension = Path.GetExtension(nueRequestAttchment.requestAtchmentFile.FileName);
                    string VFileName = DateTime.UtcNow.ToString("yyyyMMddhhmmss") + "_" + FileExtension;

                    var dateCreated = DateTime.UtcNow;
                    AttachmentLog attachmentLog = new AttachmentLog();
                    attachmentLog.RequestId = userRequest.NueRequestMasterId;
                    attachmentLog.Request = userRequest.RequestId;
                    attachmentLog.UserId = currentUser.Id;
                    attachmentLog.OwnerId = userRequest.OwnerId;
                    attachmentLog.FileName = FileName;
                    attachmentLog.FileExt = FileExtension;
                    attachmentLog.VFileName = VFileName;
                    attachmentLog.AddedOn = dateCreated;
                    attachmentLog.ModifiedOn = dateCreated;

                    string UploadPath = ConfigurationManager.AppSettings["UserFilePath"].ToString();
                    //var tempUPath = Server.MapPath(UploadPath + requestId).Replace(@"\", "/");
                    var tempUPath = (UploadPath + requestId).Replace(@"\", "/");

                    bool exists = System.IO.Directory.Exists(tempUPath);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(tempUPath);

                    string vFilePath = UploadPath + requestId + "/" + VFileName;
                    nueRequestAttchment.requestAtchmentFile.SaveAs(vFilePath);

                    int addedId = new DataAccess().addRequestAttachmentLog(attachmentLog);
                    if (addedId != -1)
                    {
                        NueRequestActivity nueRequestActivity = new NueRequestActivity();
                        nueRequestActivity.Payload = VFileName;
                        nueRequestActivity.PayloadType = nueRequestActivityMaster.Id;
                        nueRequestActivity.UserId = currentUser.Id;
                        nueRequestActivity.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity.Request = userRequest.RequestId;
                        nueRequestActivity.AddedOn = dateCreated;
                        nueRequestActivity.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity);
                        retrunResponse = "File added successfully.";
                    }
                    else
                    {
                        throw new Exception("Invalid request");
                    }

                }
                else
                {
                    retrunResponse = "Invalid request";
                }
            }
            catch (Exception e)
            {
                retrunResponse = "An error occerd.";
            }
            finally
            {

            }
            return RedirectToAction("SelfRequestDetails", new { requestId = requestId, message = retrunResponse });
        }

        [HttpPost]
        public ActionResult AddUserComment(FormCollection formCollection)
        {
            string retrunResponse = "";
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null || userComment == null || requestId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" )
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();
                NueRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if(userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (nueRequestAceessLogs.Where(x => (x.UserId == currentUser.Id && x.OwnerId != currentUser.Id)).Count() > 0)
                {
                    userAccess = true;
                }

                if (userRequest.OwnerId == currentUser.Id)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    var dateCreated = DateTime.UtcNow;

                    NueRequestActivity nueRequestActivity = new NueRequestActivity();
                    nueRequestActivity.Payload = userComment;
                    nueRequestActivity.PayloadType = nueRequestActivityMaster.Id;
                    nueRequestActivity.UserId = currentUser.Id;
                    nueRequestActivity.RequestId = userRequest.NueRequestMasterId;
                    nueRequestActivity.Request = userRequest.RequestId;
                    nueRequestActivity.AddedOn = dateCreated;
                    nueRequestActivity.ModifiedOn = dateCreated;
                    int addedId = new DataAccess().addRequestComment(nueRequestActivity);
                    if (addedId != -1)
                    {
                        retrunResponse = "Comment added sucessfully";
                    }
                    else
                    {
                        throw new Exception("Invalid request");
                    }
                }
                else
                {
                    throw new Exception("Invalid Request");
                }
                
            }
            catch (Exception e)
            {
                retrunResponse = "An error occerd.";
            }
            finally
            {

            }
            return RedirectToAction("SelfRequestDetails", new { requestId = requestId, message = retrunResponse });
        }
    }
}
