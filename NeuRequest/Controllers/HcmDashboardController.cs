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
            if(!Utils.isValidUserObject(currentUser))
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
            if (!Utils.isValidUserObject(currentUser))
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
            if (!Utils.isValidUserObject(currentUser))
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
            if (!Utils.isValidUserObject(currentUser))
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
            if (!Utils.isValidUserObject(currentUser))
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


        public ActionResult PreApprovalHCMHistory()
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

            var userAceess = currentUser.userAccess;
            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getHcmActivePreApproverRequests();
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
            if (!Utils.isValidUserObject(currentUser))
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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }

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
            List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);

            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
            
            List<NuRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
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
                    NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                    string viewRender = new Utils().generateLeaveCancelationUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeaveCancelationModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Cancelation";
                }
                else if (userRequest.RequestSubType == "LeavePastApply")
                {
                    NeLeavePastApplyModal neuLeavePastApplyModal = new DataAccess().getNeuLeavePastApplyDetails(requestId);
                    string viewRender = new Utils().generateLeavePastApplyUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeavePastApplyModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Past Apply";
                }
                else if (userRequest.RequestSubType == "LeaveWFHApply")
                {
                    NeLeaveWFHApplyModal neuLeaveWFHApplyModal = new DataAccess().getNeuLeaveWFHApplyDetails(requestId);
                    string viewRender = new Utils().generateLeaveWFHApplyUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, neuLeaveWFHApplyModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Work From Home";
                }
                else if (userRequest.RequestSubType == "LeaveBalanceEnquiry")
                {
                    LeaveBalanceEnquiryModal leaveBalanceEnquiryModal = new DataAccess().getNeuLeaveBalanceEnquiryDetails(requestId);
                    string viewRender = new Utils().generateLeaveBalanceEnquiryUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, leaveBalanceEnquiryModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Leave Balance Enquiry";
                }
                else if (userRequest.RequestSubType == "HCMAddressProof")
                {
                    AddressProofModal addressProofModal = new DataAccess().getNeuAddressProofModalDetails(requestId);
                    string viewRender = new Utils().generateAddressProofUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, addressProofModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Address Proof"; 
                }
                else if (userRequest.RequestSubType == "HCMEmployeeVerification")
                {
                    EmployeeVerificationReqModal employeeVerificationReqModal = new DataAccess().getNeuEmployeeVerificationReqModalDetails(requestId);
                    string viewRender = new Utils().generateEmployeeVerificationReqUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, employeeVerificationReqModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Employee Verification";
                }
                else if (userRequest.RequestSubType == "SalaryCertificate")
                {
                    SalaryCertificateModal salaryCertificateModal = new DataAccess().getNeuSalaryCertificateModalDetails(requestId);
                    string viewRender = new Utils().generateSalaryCertificateUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, salaryCertificateModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Salary Certificate";
                }
                else if (userRequest.RequestSubType == "HCMGeneral")
                {
                    GeneralRequestModal generalRequestModal = new DataAccess().getNeuGeneralRequestModalDetails(requestId);
                    string viewRender = new Utils().generateGeneralRequestUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, generalRequestModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Common Request";
                }
                else if (userRequest.RequestSubType == "DomesticTrip")
                {
                    DomesticTripRequestModal domesticTripRequestModal = new DataAccess().getNeuDomesticTripRequestModal(requestId);
                    string viewRender = new Utils().generateDomesticTripRequestUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, domesticTripRequestModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "Domestic Travel";
                }
                else if (userRequest.RequestSubType == "InternationalTrip")
                {
                    InternationalTripRequestModal internationalTripRequestModal = new DataAccess().getInternationalTripRequestModal(requestId);
                    string viewRender = new Utils().generateInternationalTripUiRender(isOwner, ishcm, isApprover, currentUser, userRequest, internationalTripRequestModal, nueRequestAceessLogs, nueUserProfiles, nueRequestActivityModels, attachmentLogModels);
                    ViewData["UiRender"] = viewRender;

                    ViewData["UserRequest"] = userRequest;

                    ViewData["RequestType"] = "International Travel";
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                List<NuRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
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

        private ActionResult PrcessUserImportDb()
        {

            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"C:\Users\Monin.Jose\Desktop\em1.xlsx1" + ";Extended Properties='Excel 12.0;HDR=NO';";
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            if(dtexcel.Rows.Count > 0)
            {
                dtexcel.Rows.RemoveAt(0);
            }
            List<UserProfile> userProfiles = new List<UserProfile>();
            foreach (DataRow item in dtexcel.Rows)
            {
                try
                {
                    UserProfile userProfile = new UserProfile();
                    userProfile.NTPLID = item[0].ToString();
                    userProfile.Email = item[1].ToString();
                    userProfile.FullName = item[2].ToString();
                    userProfile.FirstName = item[3].ToString();
                    userProfile.MiddleName = item[4].ToString();
                    userProfile.LastName = item[5].ToString();
                    userProfile.EmpStatusId = int.Parse(item[6].ToString());
                    userProfile.DateofJoining = item[7].ToString();
                    userProfile.PracticeId = int.Parse(item[8].ToString());
                    userProfile.Location = item[9].ToString();
                    userProfile.JLId = int.Parse(item[10].ToString());
                    userProfile.DSId = int.Parse(item[11].ToString());
                    userProfile.Active = int.Parse(item[12].ToString());
                    userProfile.AddedOn = DateTime.UtcNow.ToString();
                    if (Utils.isValidUserObject(userProfile))
                    {
                        userProfile.Email = userProfile.Email.ToLower();
                        userProfile.userPreference = new UserPreference();
                        userProfile.userPreference.IsMailCommunication = 1;
                        new DataAccess().saveUserProfile(userProfile);
                        userProfiles.Add(userProfile);
                    }
                }
                catch (Exception e)
                {
                    
                }
            }

            if(userProfiles.Count > 0)
            {

            }


            /*Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Monin.Jose\Desktop\em.xlsx");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            try
            {
                List<UserProfile> userProfiles = new List<UserProfile>();

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                for (int i = 1; i <= rowCount; i++)
                {
                    UserProfile userProfile = new UserProfile();
                    userProfile.NTPLID = xlRange.Cells[i, 0].Value2.ToString();
                    userProfile.Email = xlRange.Cells[i, 1].Value2.ToString();
                    userProfile.FullName = xlRange.Cells[i, 2].Value2.ToString();
                    userProfile.FirstName = xlRange.Cells[i, 3].Value2.ToString();
                    userProfile.MiddleName = xlRange.Cells[i, 4].Value2.ToString();
                    userProfile.LastName = xlRange.Cells[i, 5].Value2.ToString();
                    userProfile.EmpStatusId = xlRange.Cells[i, 6].Value2.ToString();
                    userProfile.DateofJoining = xlRange.Cells[i, 7].Value2.ToString();
                    userProfile.PracticeId = xlRange.Cells[i, 8].Value2.ToString();
                    userProfile.Location = xlRange.Cells[i, 9].Value2.ToString();
                    userProfile.JLId = xlRange.Cells[i, 10].Value2.ToString();
                    userProfile.DSId = xlRange.Cells[i, 11].Value2.ToString();
                    userProfile.Active = xlRange.Cells[i, 12].Value2.ToString();
                    userProfile.AddedOn = DateTime.UtcNow.ToString();
                    if (Utils.isValidUserObject(userProfile))
                    {
                        userProfiles.Add(userProfile);
                    }
                }
            }
            catch (Exception e)
            {

                
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }*/
            return null;
        }






































        /********************** International Trip Request ***************************/
        public ActionResult InternationalTripRequest()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["InternationalTripUiRender"] = new InternationalTripUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult InternationalTripRequest(InternationalTripUiRender internationalTripUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (internationalTripUiRender.isValid())
                {

                    //List<DAL.NueUserProfile> nueUserProfiles1 = new DataAccess().getAllUserProfilesDinamic();
                    //List<DAL.NueUserProfile> userProfilesAdmin1 = nueUserProfiles1.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();


                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "InternationalTrip");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    InternationalTripRequest internationalTripRequest = new InternationalTripRequest();
                    internationalTripRequest.UserId = currentUser.Id;
                    internationalTripRequest.RequestId = newRequestId;
                    internationalTripRequest.NeedVisiaProcessing = internationalTripUiRender.NeedVisiaProcessing;
                    internationalTripRequest.PlaceToVisit = internationalTripUiRender.PlaceToVisit;
                    internationalTripRequest.StartDate = internationalTripUiRender.StartDate;
                    internationalTripRequest.ProjectName = internationalTripUiRender.ProjectName;
                    internationalTripRequest.Message = internationalTripUiRender.Message;
                    internationalTripRequest.AddedOn = dateCreated;
                    internationalTripRequest.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addInternationalTripRequest(internationalTripRequest);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            //List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.NueAccessMapper.Any(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User"))).ToList();
                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();
                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesAdmin)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "International Travel Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited international travel request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                    ViewData["UserMasterList"] = userProfiles;
                    ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                    ViewData["InternationalTripUiRender"] = internationalTripUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["InternationalTripUiRender"] = internationalTripUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawInternationalTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
        public JsonResult CloseInternationalTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
        public JsonResult ApproveInternationalTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "International Travel Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) international travel request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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

        /********************** International Trip Request ***************************/




        /********************** Domestic Trip Request ***************************/
        public ActionResult DomesticTripRequest()
        {
            TempData["Message"] = null;
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
            ViewData["DomesticTripRequestUiRender"] = new DomesticTripRequestUiRender();
            return View();
        }

        [HttpPost]
        public ActionResult DomesticTripRequest(DomesticTripRequestUiRender domesticTripRequestUiRender)
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (domesticTripRequestUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "DomesticTrip");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    DomesticTripRequest domesticTripRequest = new DomesticTripRequest();
                    domesticTripRequest.UserId = currentUser.Id;
                    domesticTripRequest.RequestId = newRequestId;
                    domesticTripRequest.Accommodation = domesticTripRequestUiRender.Accommodation;
                    domesticTripRequest.LocationFrom = domesticTripRequestUiRender.LocationFrom;
                    domesticTripRequest.LocationTo = domesticTripRequestUiRender.LocationTo;
                    domesticTripRequest.StartDate = domesticTripRequestUiRender.StartDate;
                    domesticTripRequest.EndDate = domesticTripRequestUiRender.EndDate;
                    domesticTripRequest.Message = domesticTripRequestUiRender.Message;
                    domesticTripRequest.AddedOn = dateCreated;
                    domesticTripRequest.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addDomesticTripRequest(domesticTripRequest);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesAdmin)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Domestic Travel Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited domestic travel request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);
                            }


                            new DataAccess().addNeuMessagess(messages);


                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                    ViewData["UserMasterList"] = userProfiles;
                    ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                    ViewData["DomesticTripRequestUiRender"] = domesticTripRequestUiRender;
                    TempData["Message"] = "Invalid request";
                    return View();
                }
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["DomesticTripRequestUiRender"] = domesticTripRequestUiRender;
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

        [HttpPost]
        public JsonResult WithdrawDomesticTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
        public JsonResult CloseDomesticTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
        public JsonResult ApproveDomesticTripRequest(FormCollection formCollection)
        {
            string userComment = formCollection["userComment"];
            string requestId = formCollection["requestId"];
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
                        nueRequestActivity2.Payload = appCmt;
                        nueRequestActivity2.PayloadType = nueRequestCmtActivityMaster.Id;
                        nueRequestActivity2.UserId = currentUser.Id;
                        nueRequestActivity2.RequestId = userRequest.NueRequestMasterId;
                        nueRequestActivity2.Request = userRequest.RequestId;
                        nueRequestActivity2.AddedOn = dateCreated;
                        nueRequestActivity2.ModifiedOn = dateCreated;
                        new DataAccess().addRequestComment(nueRequestActivity2);

                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = "Domestic Travel Request";
                        messagesModel.EmptyMessage = currentUser.FullName + " approved(HCM) domestic travel request";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = userRequest.OwnerId;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);

                        new DataAccess().addNeuMessagess(messages);

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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

        /********************** Domestic Trip Request ***************************/



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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (generalRequestUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMGeneral");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    GeneralRequest generalRequest = new GeneralRequest();
                    generalRequest.UserId = currentUser.Id;
                    generalRequest.RequestId = newRequestId;
                    generalRequest.Message = generalRequestUiRender.message;
                    generalRequest.AddedOn = dateCreated;
                    generalRequest.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addGeneralRequest(generalRequest);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                            foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));
                        
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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (salaryCertificateUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "SalaryCertificate");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    SalaryCertificate salaryCertificate = new SalaryCertificate();
                    salaryCertificate.UserId = currentUser.Id;
                    salaryCertificate.RequestId = newRequestId;
                    salaryCertificate.Message = salaryCertificateUiRender.message;
                    salaryCertificate.AddedOn = dateCreated;
                    salaryCertificate.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addSalaryCertificateRequest(salaryCertificate);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                            foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (employeeVerificationReqUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMEmployeeVerification");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    EmployeeVerificationReq employeeVerificationReq = new EmployeeVerificationReq();
                    employeeVerificationReq.UserId = currentUser.Id;
                    employeeVerificationReq.RequestId = newRequestId;
                    employeeVerificationReq.Message = employeeVerificationReqUiRender.message;
                    employeeVerificationReq.AddedOn = dateCreated;
                    employeeVerificationReq.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addEmployeeVerificationRequest(employeeVerificationReq);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();


                            foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));
                
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (addressProofUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "HCMAddressProof");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    AddressProof addressProof = new AddressProof();
                    addressProof.UserId = currentUser.Id;
                    addressProof.RequestId = newRequestId;
                    addressProof.Message = addressProofUiRender.message;
                    addressProof.AddedOn = dateCreated;
                    addressProof.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addAddressProofRequest(addressProof);
                    if (newRequestInternId != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                            foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));
                
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            TempData["Message"] = null;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveBalanceEnquiryUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveBalanceEnquiry");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

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
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();
                            //List<MailItem> mailItems = new List<MailItem>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            new DataAccess().addNeuRequestAccessLogs(nueRequestAceessLogs);

                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                            List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                            foreach (var item in userProfilesAdmin)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Leave Balance Enquiry";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited leave balance enquiry request";
                                messagesModel.Processed = 0;
                                messagesModel.UserId = item.Id;
                                messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + newRequestId;
                                messagesModel.MessageDate = dateCreated;
                                messages.Add(messagesModel);

                                /*MailItem mailItem = new MailItem();
                                mailItem.Subject = messagesModel.Message;
                                mailItem.Body = mailTemplate;
                                mailItem.To = "monin.jose@neudesic.com";
                                mailItem.Priority = true;
                                mailItems.Add(mailItem);*/
                            }

                            
                            new DataAccess().addNeuMessagess(messages);
                            //new Utils().mailHandilar(mailItems);
                            
                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser)  || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    var dateCreated = DateTime.UtcNow;
                    NuRequestMaster nueRequestMaster = new NuRequestMaster();
                    nueRequestMaster.Id = userRequest.NueRequestMasterId;
                    nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                    nueRequestMaster.ModifiedOn = dateCreated;
                    int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                    if (newRequestTempInternId != -1)
                    {
                        List<MessagesModel> messages = new List<MessagesModel>();

                        NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                        string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                        if (userComment != null && userComment.Trim() != "")
                        {
                            NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                        NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                        HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveWFHApplyUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveWFHApply");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

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
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leaveWFHApplyUiRender.leaveWFHApplyApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            UserPreference userPreference = new UserPreference();
                            userPreference.UserId = currentUser.Id;
                            userPreference.FirstApprover = int.Parse(leaveWFHApplyUiRender.leaveWFHApplyApprover);

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
                            new DataAccess().addUserPreferenceL1(userPreference);

                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));
                            
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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if (nueRequestAceessLogs.Where(x => x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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
                    NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if (updated != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            List<NuRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if (nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                                List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                                //List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                                foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leavePastApplyUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeavePastApply");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    NeLeavePastApply neuLeavePastApply = new NeLeavePastApply();
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
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leavePastApplyUiRender.leavePastApplyApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            UserPreference userPreference = new UserPreference();
                            userPreference.UserId = currentUser.Id;
                            userPreference.FirstApprover = int.Parse(leavePastApplyUiRender.leavePastApplyApprover);

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
                            new DataAccess().addUserPreferenceL1(userPreference);

                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));

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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if(nueRequestAceessLogs.Where(x=>x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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
                    NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if (updated != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            List<NuRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if (nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                //List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                                List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                                List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                                foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
            if (!Utils.isValidUserObject(currentUser))
            {
                return RedirectToAction("SignIn", "Account");
            }
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (leaveCancelationUiRender.isValid())
                {
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "LeaveCancelation");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    NeLeaveCancelation neuLeaveCancelation = new NeLeaveCancelation();
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
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
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
                            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = currentUser.Id;
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 1;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                            nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.RequestId = newRequestTempInternId;
                            nueRequestAceessLog.UserId = int.Parse(leaveCancelationUiRender.leaveCancelationApprover);
                            nueRequestAceessLog.OwnerId = currentUser.Id;
                            nueRequestAceessLog.Completed = 0;
                            nueRequestAceessLog.AddedOn = dateCreated;
                            nueRequestAceessLog.ModifiedOn = dateCreated;
                            nueRequestAceessLogs.Add(nueRequestAceessLog);

                            UserPreference userPreference = new UserPreference();
                            userPreference.UserId = currentUser.Id;
                            userPreference.FirstApprover = int.Parse(leaveCancelationUiRender.leaveCancelationApprover);


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
                            new DataAccess().addUserPreferenceL1(userPreference);

                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/request-number-tracker.db"), newRequestId);

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, newRequestId, messages));
                            
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
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("withdraw");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                NuRequestActivityMaster nueRequestStatus1 = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus2 = new DataAccess().getRequestStatus("withdraw");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId != nueRequestStatus.Id 
                        && userRequest.NueRequestStatusId != nueRequestStatus1.Id
                        && userRequest.NueRequestStatusId != nueRequestStatus2.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - withdrawn the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("close");
                NuRequestActivityMaster nueRequestStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if(userRequest.OwnerId == currentUser.Id)
                {
                    if (userRequest.NueRequestStatusId == nueRequestStatus.Id)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - close the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if (userComment != null)
                {
                    userComment = userComment.Trim();
                }

                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("completed");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    userAccess = true;
                }

                if (userAccess)
                {
                    List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                    UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                    if(nueRequestAceessLogs.Where(x=>x.Completed != 1 && x.UserId != userRequest.OwnerId).Count() <= 0)
                    {
                        var dateCreated = DateTime.UtcNow;
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if (newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") - HCM approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                   || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }

                if(userComment != null)
                {
                    userComment = userComment.Trim();
                }
                
                NuRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("L1 Approval");
                NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("In_Approval");
                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();

                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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
                    NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                    nueRequestAceessLog.RequestId = userRequest.NueRequestMasterId;
                    nueRequestAceessLog.UserId = currentUser.Id;
                    nueRequestAceessLog.Completed = 1;
                    nueRequestAceessLog.ModifiedOn = dateCreated;
                    int updated = new DataAccess().updateNeuRequestAccessLogs(nueRequestAceessLog);
                    if(updated != -1)
                    {
                        NuRequestMaster nueRequestMaster = new NuRequestMaster();
                        nueRequestMaster.Id = userRequest.NueRequestMasterId;
                        nueRequestMaster.RequestStatus = nueRequestNewStatus.Id;
                        nueRequestMaster.ModifiedOn = dateCreated;
                        int newRequestTempInternId = new DataAccess().updateNeuRequestStatusLogs(nueRequestMaster);
                        if(newRequestTempInternId != -1)
                        {
                            List<MessagesModel> messages = new List<MessagesModel>();

                            NuRequestActivityMaster nueRequestCmtActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                            string appCmt = currentUser.FullName + " (" + currentUser.NTPLID + ") approved the request.";
                            if (userComment != null && userComment.Trim() != "")
                            {
                                NuRequestActivity nueRequestActivity1 = new NuRequestActivity();
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
                            NuRequestActivity nueRequestActivity2 = new NuRequestActivity();
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

                            List<NuRequestAceessLog> nueRequestAceessLogsTemp = new DataAccess().getRequestAccessList(requestId);
                            if(nueRequestAceessLogsTemp.Where(x => x.Completed == 0).Count() <= 0)
                            {
                                //List<UserProfile> userProfilesTemp = userProfiles.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();

                                List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                                List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                                foreach (var item in userProfilesAdmin)
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

                            HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, userRequest.RequestId, messages));

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
        public ActionResult AddUserAttachment(NuRequestAttchment nueRequestAttchment)
        {
            string retrunResponse = "";
            string requestId = nueRequestAttchment.requestId;
            try
            {
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || requestId == null
                    || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                NuRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("File");
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if (userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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
                        NuRequestActivity nueRequestActivity = new NuRequestActivity();
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
                if (!Utils.isValidUserObject(currentUser) || userComment == null || requestId == null
                    || userComment.Trim() == "" || requestId.Trim() == "" )
                {
                    throw new Exception("Invalid request");
                }
                userComment = userComment.Trim();
                NuRequestActivityMaster nueRequestActivityMaster = new DataAccess().getRequestActivityMasterId("Comment");
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if(userRequest == null)
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                //List<UserProfile> userProfiles1 = new DataAccess().getAllUserProfiles();
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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

                    NuRequestActivity nueRequestActivity = new NuRequestActivity();
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

        [HttpPost]
        public JsonResult DirectMail(FormCollection formCollection)
        {
            var dateCreated = DateTime.UtcNow;
            string userComment = formCollection["Direct_Mail_Request_Comment"];
            string recipient = formCollection["Direct_Mail_Request_Recipient"];
            string requestId = formCollection["Direct_Mail_Request_Id"];
            try
            {

                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (!Utils.isValidUserObject(currentUser) || userComment == null || recipient == null || requestId == null
                   || userComment.Trim() == "" || recipient.Trim() == "" || requestId.Trim() == "")
                {
                    throw new Exception("Invalid request");
                }
                
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));
                List<MessagesModel> messages = new List<MessagesModel>();

                if (!recipient.EndsWith(";"))
                {
                    recipient += ";";
                }
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                if(userRequest == null || userRequest.RequestId != requestId)
                {
                    throw new Exception("Invalid request");
                }

                var userAceess = currentUser.userAccess;
                bool userAccess = false;

                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
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

                if (!userAccess)
                {
                    return Json(new JsonResponse("Failed", "You are not autharised to perform this action"), JsonRequestBehavior.AllowGet);
                }

                var toUsers = recipient.Split(';').ToList();

                List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();

                foreach (var item in toUsers)
                {
                    var user = nueUserProfiles.Where(x => x.Email == item);
                    if(user != null && user.Count() > 0 && user.First().Email != null && user.First().Email.Trim() != "")
                    {
                        MessagesModel messagesModel = new MessagesModel();
                        messagesModel.Message = currentUser.FullName +" send you a message";
                        messagesModel.EmptyMessage = "<div style=\"font-size: 15px; font-weight: 500; text-align: left; line-height: 17px;\">" + userComment+"</div>";
                        messagesModel.Processed = 0;
                        messagesModel.UserId = user.First().Id;
                        messagesModel.Target = "/HcmDashboard/SelfRequestDetails?requestId=" + userRequest.RequestId;
                        messagesModel.MessageDate = dateCreated;
                        messages.Add(messagesModel);
                    }
                }
                

                HostingEnvironment.QueueBackgroundWorkItem(ct => new Utils().renderGenerateMailItem(domainName, mailTemplate, requestId, messages));


                return Json(new JsonResponse("Ok", "Message send successfully."), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new JsonResponse("Failed", "An error occerd while sending mail"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
