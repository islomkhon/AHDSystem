using AHD.App_Start;
using AHD.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

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
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();
            ViewData["NueUserProfile"] = user;
            
            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            var filterDup = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", nueUserProfile.ntplId)
                    & (Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.close)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.withdraw))));

            List<NueRequestModel> userRequests = document.Find<NueRequestModel>(filterDup).ToList<NueRequestModel>();

            ViewData["UserMasterList"] = userRequests;
            return View();
        }

        public ActionResult RequestHistory()
        {

            NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();
            ViewData["NueUserProfile"] = user;

            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            var filterDup = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", nueUserProfile.ntplId)
                    &( Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.close)
                    | Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.withdraw))));

            List<NueRequestModel> userRequests = document.Find<NueRequestModel>(filterDup).ToList<NueRequestModel>();

            ViewData["UserMasterList"] = userRequests;
            return View();
        }

        public ActionResult ApprovalInbox()
        {

            NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();
            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            List<NueRequestModel> userRequests = new List<NueRequestModel>();
            ViewData["NueUserProfile"] = user;
            bool userAdmin = false;
            if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
            {
                userAdmin = true;
                var filterFind = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", user.ntplId)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.close)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.withdraw)));
                userRequests = document.Find<NueRequestModel>(filterFind).ToList<NueRequestModel>();
            }
            else
            {
                var filterFind = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", user.ntplId)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.close)
                    & Builders<NueRequestModel>.Filter.Ne("RequestStatus", RequestStatus.withdraw)));
                List<NueRequestModel> userRequestsTemp = document.Find<NueRequestModel>(filterFind).ToList<NueRequestModel>();
                userRequests = userRequestsTemp.Where(x => x.accessLists.Contains(user.ntplId)).ToList<NueRequestModel>();
            }

            ViewData["UserMasterList"] = userRequests;
            return View();
        }

        public ActionResult ApprovalHistory()
        {

            NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();
            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            List<NueRequestModel> userRequests = new List<NueRequestModel>();
            ViewData["NueUserProfile"] = user;
            bool userAdmin = false;
            if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
            {
                userAdmin = true;
                var filterFind = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", user.ntplId)
                    &( Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.close)
                    | Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.withdraw))));
                userRequests = document.Find<NueRequestModel>(filterFind).ToList<NueRequestModel>();
            }
            else
            {
                var filterFind = ((Builders<NueRequestModel>.Filter.Eq("NTPLID", user.ntplId)
                    & (Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.close)
                    | Builders<NueRequestModel>.Filter.Eq("RequestStatus", RequestStatus.withdraw))));
                List<NueRequestModel> userRequestsTemp = document.Find<NueRequestModel>(filterFind).ToList<NueRequestModel>();
                userRequests = userRequestsTemp.Where(x => x.accessLists.Contains(user.ntplId)).ToList<NueRequestModel>();
            }

            ViewData["UserMasterList"] = userRequests;
            return View();
        }

        public ActionResult SelfRequestDetails(String requestId, String userId, String message = null)
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            List<NueUserProfile> userListTemp = returnUserList.ToList<NueUserProfile>();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            ViewData["RequestId"] = requestId;
            ViewData["UserIdStr"] = (Session["userProfileSession"] as NueUserProfile).ntplId;
            if (message != null)
            {
                TempData["Message"] = message;
            }

            NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
            NueUserProfile user = userListTemp.Where(x => x.ntplId == nueUserProfile.ntplId).First();
            var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

            var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

            bool userAccess = false;
            if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
            {
                userAccess = true;
            }

            NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
            if (!userAccess && userRequest != null && (userRequest.ntplId == user.ntplId ||  userRequest.accessLists.Contains(user.ntplId)))
            {
                userAccess = true;
            }

            if (userAccess)
            {
                bool isOwner = false;
                bool ishcm = false;
                bool isApprover = false;

                if (userRequest.ntplId == user.ntplId)
                {
                    isOwner = true;
                }

                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
                {
                    ishcm = true;
                }

                if (userRequest.requestType != RequestType.LeaveWFHApply && userRequest.accessLists.Contains(user.ntplId))
                {
                    isApprover = true;
                }

                if(userRequest.requestType == RequestType.LeaveCancelation)
                {
                    string viewRender = new Utils().generateLeaveCancelationUiRender(isOwner, ishcm, isApprover, userRequest, user, userListTemp);

                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Cancelation";
                }
                else if (userRequest.requestType == RequestType.LeavePastApply)
                {
                    string viewRender = new Utils().generatePastLeaveUiRender(isOwner, ishcm, isApprover, userRequest, user, userListTemp);

                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Past Leave Apply";
                }
                else if (userRequest.requestType == RequestType.LeaveWFHApply)
                {
                    string viewRender = new Utils().generateWFHUiRender(isOwner, ishcm, isApprover, userRequest, user, userListTemp);

                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Work From Home";
                }
            }
            else
            {
                TempData["Message"] = "You are not authorized to access";
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


        public ActionResult LeavePastApplyCreate()
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            ViewData["LeavePastApplyUiRender"] = new LeavePastApplyUiRender();

            return View(returnUserList);
        }

        public ActionResult LeaveWFHApplyCreate()
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            ViewData["LeaveWFHApplyUiRender"] = new LeaveWFHApplyUiRender();

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
                    nueRequestModel.dateModified = dateCreated;
                    nueRequestModel.requestLogs = new LinkedList<RequestLog>();
                    nueRequestModel.attachmentLogs = new LinkedList<AttachmentLog>();
                    nueRequestModel.accessLists = new LinkedList<string>();
                    nueRequestModel.accessLists.AddLast(approver1.ntplId);

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
        public ActionResult LeavePastApplyCreate(LeavePastApplyUiRender leavePastApplyUiRender)
        {
            try
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                List<NueUserProfile> returnUserListTemp = returnUserList.ToList<NueUserProfile>();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                if (leavePastApplyUiRender.isValid())
                {

                    NueUserProfile approver1 = returnUserListTemp.Where(x => x.ntplId == leavePastApplyUiRender.leaveCancelationApprover).First();
                    var dateCreated = DateTime.UtcNow;
                    NeuLeavePastApply neuLeavePastApply = new NeuLeavePastApply();
                    NueUserProfile owner = (Session["userProfileSession"] as NueUserProfile);
                    neuLeavePastApply.ntplId = owner.ntplId;
                    neuLeavePastApply.nueUserProfile = owner;
                    neuLeavePastApply.leaveStartDate = leavePastApplyUiRender.leaveStartDate;
                    neuLeavePastApply.leaveEndDate = leavePastApplyUiRender.leaveEndDate;
                    neuLeavePastApply.message = leavePastApplyUiRender.message;
                    neuLeavePastApply.isApprovalProcess = true;
                    neuLeavePastApply.approvalProcess = new ApprovalProcess();
                    neuLeavePastApply.requestStatus = RequestStatus.created;
                    neuLeavePastApply.dateCreated = dateCreated;
                    neuLeavePastApply.leaveCancelationUiRender = leavePastApplyUiRender;
                    neuLeavePastApply.approvalProcess.requestStatusStage = neuLeavePastApply.requestStatus;
                    neuLeavePastApply.approvalProcess.requestApprovals = new LinkedList<RequestApproval>();

                    //craete and add requestApprovals
                    RequestApproval requestApproval = new RequestApproval();
                    requestApproval.isApproved = false;
                    requestApproval.requestStatusStage = RequestStatus.L1;
                    requestApproval.ntplId = approver1.ntplId;
                    requestApproval.nueUserProfile = approver1;
                    neuLeavePastApply.approvalProcess.requestApprovals.AddLast(requestApproval);

                    var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
                    neuLeavePastApply.requestId = newRequestId;

                    NueRequestModel nueRequestModel = new NueRequestModel();
                    nueRequestModel.ntplId = owner.ntplId;
                    nueRequestModel.requestId = newRequestId;
                    nueRequestModel.nueUserProfile = owner;
                    nueRequestModel.requestType = RequestType.LeavePastApply;
                    nueRequestModel.requestPayload = neuLeavePastApply;
                    nueRequestModel.requestStatus = RequestStatus.created;
                    nueRequestModel.dateCreated = dateCreated;
                    nueRequestModel.dateModified = dateCreated;
                    nueRequestModel.requestLogs = new LinkedList<RequestLog>();
                    nueRequestModel.attachmentLogs = new LinkedList<AttachmentLog>();
                    nueRequestModel.accessLists = new LinkedList<string>();
                    nueRequestModel.accessLists.AddLast(approver1.ntplId);

                    document.InsertOne(nueRequestModel);
                    System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);
                    return RedirectToAction("Index");
                }
                else
                {
                    returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                    ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                    ViewData["UserMasterList"] = returnUserList;
                    ViewData["LeavePastApplyUiRender"] = leavePastApplyUiRender;
                    TempData["Message"] = "Invalid request";
                    return View(returnUserList); ;
                }
            }
            catch (Exception e)
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                ViewData["UserMasterList"] = returnUserList;
                ViewData["LeavePastApplyUiRender"] = new LeavePastApplyUiRender();
                TempData["Message"] = "Invalid request";
                return View(returnUserList);
            }
        }

        [HttpPost]
        public ActionResult LeaveWFHApplyCreate(LeaveWFHApplyUiRender leaveWFHApplyUiRender)
        {
            try
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                List<NueUserProfile> returnUserListTemp = returnUserList.ToList<NueUserProfile>();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                if (leaveWFHApplyUiRender.isValid())
                {

                    //NueUserProfile approver1 = returnUserListTemp.Where(x => x.ntplId == leaveWFHApplyUiRender.leaveCancelationApprover).First();
                    var dateCreated = DateTime.UtcNow;
                    NeuLeaveWFHApply neuLeaveWFHApply = new NeuLeaveWFHApply();
                    NueUserProfile owner = (Session["userProfileSession"] as NueUserProfile);
                    neuLeaveWFHApply.ntplId = owner.ntplId;
                    neuLeaveWFHApply.nueUserProfile = owner;
                    neuLeaveWFHApply.leaveStartDate = leaveWFHApplyUiRender.leaveStartDate;
                    neuLeaveWFHApply.leaveEndDate = leaveWFHApplyUiRender.leaveEndDate;
                    neuLeaveWFHApply.message = leaveWFHApplyUiRender.message;
                    neuLeaveWFHApply.isApprovalProcess = false;
                    neuLeaveWFHApply.approvalProcess = new ApprovalProcess();
                    neuLeaveWFHApply.requestStatus = RequestStatus.created;
                    neuLeaveWFHApply.dateCreated = dateCreated;
                    neuLeaveWFHApply.leaveCancelationUiRender = leaveWFHApplyUiRender;
                    neuLeaveWFHApply.approvalProcess.requestStatusStage = neuLeaveWFHApply.requestStatus;
                    neuLeaveWFHApply.approvalProcess.requestApprovals = null;

                    //craete and add requestApprovals NA for wfh
                    

                    var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
                    neuLeaveWFHApply.requestId = newRequestId;

                    NueRequestModel nueRequestModel = new NueRequestModel();
                    nueRequestModel.ntplId = owner.ntplId;
                    nueRequestModel.requestId = newRequestId;
                    nueRequestModel.nueUserProfile = owner;
                    nueRequestModel.requestType = RequestType.LeaveWFHApply;
                    nueRequestModel.requestPayload = neuLeaveWFHApply;
                    nueRequestModel.requestStatus = RequestStatus.created;
                    nueRequestModel.dateCreated = dateCreated;
                    nueRequestModel.dateModified = dateCreated;
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
                    ViewData["LeaveWFHApplyUiRender"] = leaveWFHApplyUiRender;
                    TempData["Message"] = "Invalid request";
                    return View(returnUserList); ;
                }
            }
            catch (Exception e)
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                ViewData["UserMasterList"] = returnUserList;
                ViewData["LeaveWFHApplyUiRender"] = new LeaveWFHApplyUiRender();
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
                        userRequest.dateModified = DateTime.UtcNow;
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
        public JsonResult WithdrawLeavePastApplyRequest(FormCollection formCollection)
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
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeavePastApply)
                {
                    if (userRequest.requestStatus != RequestStatus.close && userRequest.requestStatus != RequestStatus.completed)
                    {
                        userRequest.requestStatus = RequestStatus.withdraw;
                        ((NeuLeavePastApply)userRequest.requestPayload).requestStatus = RequestStatus.withdraw;
                        userRequest.requestLogs.AddLast(requestLog);
                        userRequest.requestLogs.AddLast(requestLogCommand);
                        userRequest.dateModified = DateTime.UtcNow;
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
                        return Json(new JsonResponse("Failed", "Unabele to withdraw request now, Request is in " + userRequest.requestStatus + " stage"), JsonRequestBehavior.AllowGet);
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
        public JsonResult WithdrawLeaveWFHApplyRequest(FormCollection formCollection)
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
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeaveWFHApply)
                {
                    if (userRequest.requestStatus != RequestStatus.close && userRequest.requestStatus != RequestStatus.completed)
                    {
                        userRequest.requestStatus = RequestStatus.withdraw;
                        ((NeuLeaveWFHApply)userRequest.requestPayload).requestStatus = RequestStatus.withdraw;
                        userRequest.requestLogs.AddLast(requestLog);
                        userRequest.requestLogs.AddLast(requestLogCommand);
                        userRequest.dateModified = DateTime.UtcNow;
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
                        return Json(new JsonResponse("Failed", "Unabele to withdraw request now, Request is in " + userRequest.requestStatus + " stage"), JsonRequestBehavior.AllowGet);
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
                            userRequest.dateModified = DateTime.UtcNow;
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
        public JsonResult CloseLeavePastApplyRequest(FormCollection formCollection)
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
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeavePastApply)
                {
                    NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
                    var app = neuLeavePastApplyReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
                    if (userRequest.requestStatus != RequestStatus.completed
                        && neuLeavePastApplyReq.requestStatus != RequestStatus.completed
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
                            neuLeavePastApplyReq.requestStatus = RequestStatus.completed;
                            userRequest.requestStatus = RequestStatus.completed;
                        }
                    }
                    if (able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.requestStatus = RequestStatus.close;
                    userRequest.dateModified = DateTime.UtcNow;
                    neuLeavePastApplyReq.requestStatus = RequestStatus.close;

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
        public JsonResult CloseWFHLeaveApplyRequest(FormCollection formCollection)
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
                if (userRequest != null && userRequest.ntplId == user.ntplId && userRequest.requestType == RequestType.LeaveWFHApply)
                {
                    NeuLeaveWFHApply neuLeaveWFHApplyReq = (NeuLeaveWFHApply)userRequest.requestPayload;
                    var app = neuLeaveWFHApplyReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeaveWFHApplyReq.approvalProcess;
                    if (userRequest.requestStatus != RequestStatus.completed
                        && neuLeaveWFHApplyReq.requestStatus != RequestStatus.completed
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
                            neuLeaveWFHApplyReq.requestStatus = RequestStatus.completed;
                            userRequest.requestStatus = RequestStatus.completed;
                        }
                    }
                    if (able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.dateModified = DateTime.UtcNow;
                    userRequest.requestStatus = RequestStatus.close;
                    neuLeaveWFHApplyReq.requestStatus = RequestStatus.close;

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
                    userRequest.dateModified = DateTime.UtcNow;
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
        public JsonResult ApproveLeavePastApplyRequest(FormCollection formCollection)
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
                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
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
                if (userRequest != null && userAccess && userRequest.requestType == RequestType.LeavePastApply)
                {
                    NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
                    var app = neuLeavePastApplyReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
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
                            neuLeavePastApplyReq.requestStatus = RequestStatus.completed;
                            userRequest.requestStatus = RequestStatus.completed;
                        }
                    }
                    if (able)
                    {
                        return Json(new JsonResponse("Failed", "Invalid request, Request is not approved by all approvers"), JsonRequestBehavior.AllowGet);
                    }
                    userRequest.dateModified = DateTime.UtcNow;
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
        public JsonResult ApproveWFHLeaveRequest(FormCollection formCollection)
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
                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
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
                if (userRequest != null && userAccess && userRequest.requestType == RequestType.LeaveWFHApply)
                {
                    NeuLeaveWFHApply neuLeaveWFHApplyReq = (NeuLeaveWFHApply)userRequest.requestPayload;
                    var app = neuLeaveWFHApplyReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeaveWFHApplyReq.approvalProcess;
                    approvalProcess.requestStatusStage = RequestStatus.completed;
                    approvalProcess.dateApproved = DateTime.UtcNow;
                    neuLeaveWFHApplyReq.requestStatus = RequestStatus.completed;
                    userRequest.requestStatus = RequestStatus.completed;

                    userRequest.dateModified = DateTime.UtcNow;
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
                    userRequest.dateModified = DateTime.UtcNow;

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
        public JsonResult SubApproveLeavePastApplyRequest(FormCollection formCollection)
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
                if (userRequest != null && userRequest.requestType == RequestType.LeavePastApply)
                {

                    NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
                    var app = neuLeavePastApplyReq.isApprovalProcess;
                    ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
                    var approvals = approvalProcess.requestApprovals;
                    bool able = false;
                    RequestStatus rtTemp = RequestStatus.assigned;
                    if (app && approvals != null && approvals.Count > 0)
                    {
                        LinkedList<RequestApproval> requestApprovals = new LinkedList<RequestApproval>();
                        for (int i = 0; i < approvals.Count; i++)
                        {
                            RequestApproval requestApproval = approvals.ElementAt(i);
                            if (requestApproval.ntplId == user.ntplId)
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
                    requestLogCommand.userComment = "<neulog>Pre Approve " + rtTemp.ToString() + "</neulog>";
                    requestLogCommand.dateCreated = DateTime.UtcNow;
                    userRequest.dateModified = DateTime.UtcNow;
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

        public FileResult DownloadAttachment(String requestId, String vFile)
        {
            try
            {
                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();
                bool userAdmin = false;
                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
                {
                    userAdmin = true;
                }

                bool userAccess = false;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if (userRequest != null)
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
                        var files = userRequest.attachmentLogs.Where(x => x.requestId == userRequest.requestId && x.VFileName == vFile);
                        if(files != null && files.Count() > 0)
                        {
                            var attchment = files.First();
                            string UploadPath = ConfigurationManager.AppSettings["UserFilePath"].ToString();
                            var tempUPath = (UploadPath + requestId).Replace(@"\", "/");
                            byte[] fileBytes = System.IO.File.ReadAllBytes(@UploadPath + requestId + "/" + attchment.VFileName);
                            string fileName = attchment.fileName+ attchment.fileExt;
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
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        [HttpPost]
        public ActionResult AddUserAttachment(NueRequestAttchment nueRequestAttchment)
        {
            string retrunResponse = "";
            string requestId = nueRequestAttchment.requestId;
            string ntplId = nueRequestAttchment.userId;
            try
            {

                if (requestId == null || ntplId == null
                    || requestId.Trim() == "" || ntplId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                NueUserProfile nueUserProfile = (Session["userProfileSession"] as NueUserProfile);

                List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
                NueUserProfile user = returnUserList.Where(x => x.ntplId == nueUserProfile.ntplId).First();

                bool userAdmin = false;
                if (user.userAccess.Contains("Root_Admin") || user.userAccess.Contains("Hcm_Admin") || user.userAccess.Contains("Hcm_User"))
                {
                    userAdmin = true;
                }

                bool userAccess = false;

                var document = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");

                var filterFind = (Builders<NueRequestModel>.Filter.Eq("RequestId", requestId));

                NueRequestModel userRequest = document.Find<NueRequestModel>(filterFind).First<NueRequestModel>();
                if (userRequest != null)
                {
                    if (userRequest.requestStatus == RequestStatus.close
                        || userRequest.requestStatus == RequestStatus.withdraw)
                    {
                        retrunResponse = "Invalid request, Request is in "+ userRequest.requestStatus + " stage";
                    }
                    else
                    {
                        if (userAdmin && userRequest.ntplId == user.ntplId)
                        {//self req comment
                            userAccess = true;
                        }
                        else
                        {//check is user approver
                            if(userRequest.requestType == RequestType.LeaveCancelation)
                            {
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
                            else if (userRequest.requestType == RequestType.LeavePastApply)
                            {
                                NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
                                var app = neuLeavePastApplyReq.isApprovalProcess;
                                ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
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
                        }
                        if (userAccess)
                        {

                            string FileName = Path.GetFileNameWithoutExtension(nueRequestAttchment.requestAtchmentFile.FileName);
                            string FileExtension = Path.GetExtension(nueRequestAttchment.requestAtchmentFile.FileName);
                            string VFileName = DateTime.UtcNow.ToString("yyyyMMddhhmmss") + "_" + FileExtension;

                            AttachmentLog attachmentLog = new AttachmentLog();
                            attachmentLog.requestId = requestId;
                            attachmentLog.ntplId = user.ntplId;
                            attachmentLog.nueUserProfile = user;
                            attachmentLog.fileName = FileName;
                            attachmentLog.fileExt = FileExtension;
                            attachmentLog.VFileName = VFileName;
                            attachmentLog.dateCreated = DateTime.UtcNow;

                            string UploadPath = ConfigurationManager.AppSettings["UserFilePath"].ToString();
                            //var tempUPath = Server.MapPath(UploadPath + requestId).Replace(@"\", "/");
                            var tempUPath = (UploadPath + requestId).Replace(@"\", "/");

                            bool exists = System.IO.Directory.Exists(tempUPath);
                            if (!exists)
                                System.IO.Directory.CreateDirectory(tempUPath);

                            string vFilePath = UploadPath + requestId + "/" + VFileName;
                            nueRequestAttchment.requestAtchmentFile.SaveAs(vFilePath);


                            RequestLog requestLog = new RequestLog();
                            requestLog.requestId = requestId;
                            requestLog.ntplId = user.ntplId;
                            requestLog.nueUserProfile = user;
                            requestLog.userComment = "User attached " + FileName + " file";
                            requestLog.dateCreated = DateTime.UtcNow;

                            userRequest.requestLogs.AddLast(requestLog);
                            userRequest.attachmentLogs.AddLast(attachmentLog);
                            var collection = _dbContext._database.GetCollection<NueRequestModel>("NueRequestModel");
                            ReplaceOneResult dbResponse = collection.ReplaceOne(filterFind, userRequest);
                            if (dbResponse.ModifiedCount > 0)
                            {
                                retrunResponse = "File added successfully.";
                            }
                            else
                            {
                                retrunResponse = "An error occerd while adding file.";
                            }
                        }
                        else
                        {
                            retrunResponse = "Invalid request";
                        }
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
                    if (userRequest.requestStatus == RequestStatus.close
                        || userRequest.requestStatus == RequestStatus.withdraw)
                    {
                        retrunResponse = "Invalid request, Request is in " + userRequest.requestStatus + " stage";
                    }
                    else
                    {
                        if (userAdmin && userRequest.ntplId == user.ntplId)
                        {//self req comment
                            userAccess = true;
                        }
                        else
                        {//check is user approver

                            //userRequest.requestType == RequestType.LeaveCancelation

                            if(userRequest.requestType == RequestType.LeaveCancelation)
                            {
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
                            else if (userRequest.requestType == RequestType.LeavePastApply)
                            {
                                NeuLeavePastApply neuLeavePastApplyReq = (NeuLeavePastApply)userRequest.requestPayload;
                                var app = neuLeavePastApplyReq.isApprovalProcess;
                                ApprovalProcess approvalProcess = neuLeavePastApplyReq.approvalProcess;
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
