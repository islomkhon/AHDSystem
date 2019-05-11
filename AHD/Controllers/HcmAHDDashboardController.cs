using AHD.App_Start;
using AHD.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AHD.Controllers
{
    public class HcmAHDDashboardController : Controller
    {
        MongoContext _dbContext;
        public HcmAHDDashboardController()
        {
            _dbContext = new MongoContext();
        }

        // GET: HcmAHDDashboard
        public ActionResult Index()
        {
            
            NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
            ViewData["NueUserProfile"] = nueUserProfile;
            
            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            var filterDup = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", nueUserProfile.ntplId)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.close)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.withdraw)));

            List<NueRequestModel> userRequests = document.Find<NueRequestModel>(filterDup).ToList<NueRequestModel>();

            ViewData["UserMasterList"] = userRequests;
            return View();
        }

        public ActionResult SelfRequestDetails(String requestId, String userId, String message = null)
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            ViewData["RequestId"] = requestId;
            ViewData["UserIdStr"] = (Session["userProfileSession"] as NueUserProfile).ntplId;
            if (message != null)
            {
                TempData["Message"] = message;
            }
            return View();
        }
            // GET: HcmAHDDashboard/Create
        public ActionResult LeaveCancelationCreate()
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            ViewData["LeaveCancelationUiRender"] = new LeaveCancelationUiRender();
            
            return View(returnUserList);
        }

        
        [HttpPost]
        public ActionResult LeaveCancelationCreate(LeaveCancelationUiRender leaveCancelationUiRender)
        {
            try
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                List<NueUserProfile> returnUserListTemp = returnUserList.ToList<NueUserProfile>();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                if (leaveCancelationUiRender.isValid())
                {

                    NueUserProfile approver1 = returnUserListTemp.Where(x => x.ntplId == leaveCancelationUiRender.leaveCancelationApprover).First();
                    var dateCreated = DateTime.UtcNow;
                    NeuLeaveCancelation neuLeaveCancelation = new NeuLeaveCancelation();
                    NueUserProfile owner = (Session["userProfileSession"] as NueUserProfile);
                    neuLeaveCancelation.ntplId = owner.ntplId;
                    neuLeaveCancelation.nueUserProfile = owner;
                    neuLeaveCancelation.leaveStartDate = leaveCancelationUiRender.leaveStartDate;
                    neuLeaveCancelation.leaveEndDate = leaveCancelationUiRender.leaveEndDate;
                    neuLeaveCancelation.message = leaveCancelationUiRender.message;
                    neuLeaveCancelation.isApprovalProcess = true;
                    neuLeaveCancelation.approvalProcess = new ApprovalProcess();
                    neuLeaveCancelation.requestStatus = RequestStatus.created;
                    neuLeaveCancelation.dateCreated = dateCreated;
                    neuLeaveCancelation.leaveCancelationUiRender = leaveCancelationUiRender;
                    neuLeaveCancelation.approvalProcess.requestStatusStage = neuLeaveCancelation.requestStatus;
                    neuLeaveCancelation.approvalProcess.requestApprovals = new LinkedList<RequestApproval>();

                    //craete and add requestApprovals
                    RequestApproval requestApproval = new RequestApproval();
                    requestApproval.isApproved = false;
                    requestApproval.requestStatusStage = RequestStatus.L1;
                    requestApproval.ntplId = approver1.ntplId;
                    requestApproval.nueUserProfile = approver1;
                    neuLeaveCancelation.approvalProcess.requestApprovals.AddLast(requestApproval);

                    var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
                    neuLeaveCancelation.requestId = newRequestId;

                    NueRequestModel nueRequestModel = new NueRequestModel();
                    nueRequestModel.ntplId = owner.ntplId;
                    nueRequestModel.requestId = newRequestId;
                    nueRequestModel.nueUserProfile = owner;
                    nueRequestModel.requestType = RequestType.LeaveCancelation;
                    nueRequestModel.requestPayload = neuLeaveCancelation;
                    nueRequestModel.requestStatus = RequestStatus.created;
                    nueRequestModel.dateCreated = dateCreated;
                    nueRequestModel.requestLogs = new LinkedList<RequestLog>();
                    nueRequestModel.attachmentLogs = new LinkedList<AttachmentLog>();

                    document.InsertOne(nueRequestModel);
                    System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);
                    return RedirectToAction("Index");
                }
                else
                {
                    returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                    ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                    ViewData["UserMasterList"] = returnUserList;
                    ViewData["LeaveCancelationUiRender"] = leaveCancelationUiRender;
                    TempData["Message"] = "Invalid request";
                    return View(returnUserList);;
                }
            }
            catch(Exception e)
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                ViewData["UserMasterList"] = returnUserList;
                ViewData["LeaveCancelationUiRender"] = new LeaveCancelationUiRender();
                TempData["Message"] = "Invalid request";
                return View(returnUserList);
            }
        }

        [HttpPost]
        public JsonResult WithdrawLeaveCancelationRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            string ntplId = formCollection["userId"];
            try
            {

                if (userComment == null || requestId == null || ntplId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();

                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);

                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                if (userComment.Trim() == "")
                {
                    userComment = "Request Withdrawn";
                }
                RequestLog requestLog = new RequestLog();
                requestLog.requestId = requestId;
                requestLog.ntplId = user.ntplId;
                requestLog.nueUserProfile = user;
                requestLog.userComment = userComment;
                requestLog.dateCreated = DateTime.UtcNow;

                RequestLog requestLogCommand = new RequestLog();
                requestLogCommand.requestId = requestId;
                requestLogCommand.ntplId = user.ntplId;
                requestLogCommand.nueUserProfile = user;
                requestLogCommand.userComment = "<neulog>Withdrawn</neulog>";
                requestLogCommand.dateCreated = DateTime.UtcNow;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeaveCancelation)
                {
                    if(userRequest.requestStatus != RequestStatus.close && userRequest.requestStatus != RequestStatus.completed)
                    {
                        userRequest.requestStatus = RequestStatus.withdraw;
                        ((NeuLeaveCancelation)userRequest.requestPayload).requestStatus = RequestStatus.withdraw;
                        userRequest.requestLogs.AddLast(requestLog);
                        userRequest.requestLogs.AddLast(requestLogCommand);
                        var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                        ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                        if (dbResponse.ModifiedCount > 0)
                        {
                            return Json(new JsonResponse("Failed", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new JsonResponse("Failed", "An error occerd while approving request"), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "Unabele to withdraw request now, Request is in "+ userRequest.requestStatus +" stage"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "Invalid request"), JsonRequestBehavior.AllowGet);
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
            string ntplId = formCollection["userId"];
            try
            {
                if (userComment == null || requestId == null || ntplId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();

                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                if (userComment.Trim() == "")
                {
                    userComment = "Request Closed";
                }
                RequestLog requestLog = new RequestLog();
                requestLog.requestId = requestId;
                requestLog.ntplId = user.ntplId;
                requestLog.nueUserProfile = user;
                requestLog.userComment = userComment;
                requestLog.dateCreated = DateTime.UtcNow;

                RequestLog requestLogCommand = new RequestLog();
                requestLogCommand.requestId = requestId;
                requestLogCommand.ntplId = user.ntplId;
                requestLogCommand.nueUserProfile = user;
                requestLogCommand.userComment = "<neulog>Closed</neulog>";
                requestLogCommand.dateCreated = DateTime.UtcNow;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeaveCancelation)
                {
                    NeuLeaveCancelation neuLeaveCancelationReq = (NeuLeaveCancelation)userRequest.requestPayload;
                    var app = neuLeaveCancelationReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeaveCancelationReq.approvalProcess;
                    if(userRequest.requestStatus != RequestStatus.completed 
                        && neuLeaveCancelationReq.requestStatus != RequestStatus.completed
                        && approvalProcess.requestStatusStage != RequestStatus.completed)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not in approved stage"), JsonRequestBehavior.AllowGet);
                    }
                    var approvals = approvalProcess.requestApprovals;
                    bool able = false;
                    if (app && approvals != null && approvals.Count > 0)
                    {
                        for (int i = 0; i < approvals.Count; i++)
                        {
                            RequestApproval requestApproval = approvals.ElementAt(i);
                            if (requestApproval.isApproved != true)
                            {
                                able = true;
                                break;
                            }
                        }
                        if (!able)
                        {
                            userRequest.requestLogs.AddLast(requestLogCommand);
                            approvalProcess.requestStatusStage = RequestStatus.completed;
                            approvalProcess.dateApproved = DateTime.UtcNow;
                            neuLeaveCancelationReq.requestStatus = RequestStatus.completed;
                            userRequest.requestStatus = RequestStatus.completed;
                        }
                    }
                    if (able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.requestStatus = RequestStatus.close;
                    neuLeaveCancelationReq.requestStatus = RequestStatus.close;
                    
                    var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                    if (dbResponse.ModifiedCount > 0)
                    {
                        return Json(new JsonResponse("Failed", "Request closed successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "An error occerd while approving request"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "Invalid request"), JsonRequestBehavior.AllowGet);
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
            string ntplId = formCollection["userId"];
            try
            {

                if (userComment == null || requestId == null || ntplId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();

                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);

                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                bool userAccess = false;
                if(user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
                {
                    userAccess = true;
                }

                if (!userAccess)
                {
                    return Json(new JsonResponse("Failed", "You are not authorised to perform hcm approvals"), JsonRequestBehavior.AllowGet);
                }
                
                if (userComment.Trim() == "")
                {
                    userComment = "Request Approved";
                }
                RequestLog requestLog = new RequestLog();
                requestLog.requestId = requestId;
                requestLog.ntplId = user.ntplId;
                requestLog.nueUserProfile = user;
                requestLog.userComment = userComment;
                requestLog.dateCreated = DateTime.UtcNow;

                RequestLog requestLogCommand = new RequestLog();
                requestLogCommand.requestId = requestId;
                requestLogCommand.ntplId = user.ntplId;
                requestLogCommand.nueUserProfile = user;
                requestLogCommand.userComment = "<neulog>Approved</neulog>";
                requestLogCommand.dateCreated = DateTime.UtcNow;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if (userRequest != null && userAccess && userRequest.requestType == RequestType.LeaveCancelation)
                {
                    NeuLeaveCancelation neuLeaveCancelationReq = (NeuLeaveCancelation)userRequest.requestPayload;
                    var app = neuLeaveCancelationReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeaveCancelationReq.approvalProcess;
                    var approvals = approvalProcess.requestApprovals;
                    bool able = false;
                    if (app && approvals != null && approvals.Count > 0)
                    {
                        for (int i = 0; i < approvals.Count; i++)
                        {
                            RequestApproval requestApproval = approvals.ElementAt(i);
                            if (requestApproval.isApproved != true)
                            {
                                able = true;
                                break;
                            }
                        }
                        if (!able)
                        {
                            approvalProcess.requestStatusStage = RequestStatus.completed;
                            approvalProcess.dateApproved = DateTime.UtcNow;
                            neuLeaveCancelationReq.requestStatus = RequestStatus.completed;
                            userRequest.requestStatus = RequestStatus.completed;
                        }
                    }
                    if (able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.requestLogs.AddLast(requestLog);
                    userRequest.requestLogs.AddLast(requestLogCommand);
                    var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                    if (dbResponse.ModifiedCount > 0)
                    {
                        return Json(new JsonResponse("Failed", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "An error occerd while approving request"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "Invalid request"), JsonRequestBehavior.AllowGet);
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
            string ntplId = formCollection["userId"];
            try
            {
                if (userComment == null || requestId == null || ntplId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();

                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);

                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                RequestLog requestLog = new RequestLog();
                requestLog.requestId = requestId;
                requestLog.ntplId = user.ntplId;
                requestLog.nueUserProfile = user;
                requestLog.userComment = userComment;
                requestLog.dateCreated = DateTime.UtcNow;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if(userRequest != null && userRequest.requestType == RequestType.LeaveCancelation)
                {
                    NeuLeaveCancelation neuLeaveCancelationReq = (NeuLeaveCancelation) userRequest.requestPayload;
                    var app = neuLeaveCancelationReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeaveCancelationReq.approvalProcess;
                    var approvals = approvalProcess.requestApprovals;
                    bool able = false;
                    RequestStatus rtTemp = RequestStatus.assigned;
                    if (app && approvals != null && approvals.Count > 0)
                    {
                        LinkedList<RequestApproval> requestApprovals = new LinkedList<RequestApproval>();
                        for (int i = 0; i < approvals.Count; i++)
                        {
                            RequestApproval requestApproval = approvals.ElementAt(i);
                            if(requestApproval.ntplId == user.ntplId)
                            {
                                requestApproval.isApproved = true;
                                requestApproval.dateApproved = DateTime.UtcNow;
                                requestApproval.approvalComments = userComment;
                                able = true;
                                rtTemp = requestApproval.requestStatusStage;
                            }
                            requestApprovals.AddLast(requestApproval);
                        }
                        approvalProcess.requestApprovals = requestApprovals;
                        if (able)
                        {
                            approvalProcess.requestStatusStage = rtTemp;
                            userRequest.requestStatus = RequestStatus.In_Approval;
                        }
                    }
                    if (!able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Unable to locate your information in approver list"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.requestLogs.AddLast(requestLog);
                    RequestLog requestLogCommand = new RequestLog();
                    requestLogCommand.requestId = requestId;
                    requestLogCommand.ntplId = user.ntplId;
                    requestLogCommand.nueUserProfile = user;
                    requestLogCommand.userComment = "<neulog>Pre Approve "+ rtTemp.ToString()+ "</neulog>";
                    requestLogCommand.dateCreated = DateTime.UtcNow;
                    userRequest.requestLogs.AddLast(requestLogCommand);
                    var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                    if (dbResponse.ModifiedCount > 0)
                    {
                        return Json(new JsonResponse("Failed", "Request approved successfully."), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new JsonResponse("Failed", "An error occerd while approving request"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new JsonResponse("Failed", "Invalid request"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while updating data"), JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public ActionResult AddUserComment(FormCollection formCollection)
        {
            string retrunResponse = "";
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            string ntplId = formCollection["userId"];
            try
            {
                
                if(userComment == null || requestId == null || ntplId == null 
                    || userComment.Trim() == "" || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();


                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);

                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                bool userAdmin = false;
                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
                {
                    userAdmin = true;
                }

                bool userAccess = false;

                RequestLog requestLog = new RequestLog();
                requestLog.requestId = requestId;
                requestLog.ntplId = user.ntplId;
                requestLog.nueUserProfile = user;
                requestLog.userComment = userComment;
                requestLog.dateCreated = DateTime.UtcNow;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if(userRequest != null)
                {
                    if (userAdmin && userRequest.ntplId == user.ntplId)
                    {//self req comment
                        userAccess = true;
                    }
                    else
                    {//check is user approver
                        NeuLeaveCancelation neuLeaveCancelationReq = (NeuLeaveCancelation)userRequest.requestPayload;
                        var app = neuLeaveCancelationReq.isApprovalProcess;
                        ApprovalProcess approvalProcess = neuLeaveCancelationReq.approvalProcess;
                        var approvals = approvalProcess.requestApprovals;
                        if (app && approvals != null && approvals.Count > 0)
                        {
                            for (int i = 0; i < approvals.Count; i++)
                            {
                                RequestApproval requestApproval = approvals.ElementAt(i);
                                if (requestApproval.ntplId == user.ntplId)
                                {
                                    userAccess = true;
                                    break;
                                }
                            }
                        }
                    }
                  if (userAccess)
                  {
                        userRequest.requestLogs.AddLast(requestLog);
                        var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                        ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                        if (dbResponse.ModifiedCount > 0)
                        {
                            retrunResponse = "Comment added successfully.";
                        }
                        else
                        {
                            retrunResponse = "An error occerd while adding comment.";
                        }
                    }
                    else
                    {
                        retrunResponse = "Invalid request";
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
            return RedirectToAction("SelfRequestDetails", new { requestId = requestId, userId = ntplId, message = retrunResponse });
        }

        // GET: HcmAHDDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HcmAHDDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HcmAHDDashboard/Create
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

        // GET: HcmAHDDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HcmAHDDashboard/Edit/5
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

        // GET: HcmAHDDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HcmAHDDashboard/Delete/5
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
