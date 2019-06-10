using NeuRequest.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using TimeAgo;

namespace NeuRequest.Models
{
    public class Utils
    {

        public void mailHandilar(List<MailItem> mailItems)
        {
            try
            {
                foreach (var item in mailItems)
                {
                    HostingEnvironment.QueueBackgroundWorkItem(ct => SendMailAsync(item));
                }
            }
            catch (Exception)
            {
                
            }
        }

        public static void SendMailAsync(MailItem mailItem)
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["smtp-server"].ToString();
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["smtp-port"].ToString());
                string smtpEmail = ConfigurationManager.AppSettings["smtp-email"].ToString();
                string smtpPassword = ConfigurationManager.AppSettings["smtp-password"].ToString();
                var message = new MailMessage(new MailAddress(smtpEmail), new MailAddress(mailItem.To));
                message.Subject = mailItem.Subject;
                message.Body = mailItem.Body;
                message.IsBodyHtml = true;
                message.HeadersEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.Headers.Add("From", "test@nudesic.com");
                message.BodyEncoding = Encoding.UTF8;
                if (mailItem.Priority) message.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;

                NetworkCredential smtpUserInfo = new NetworkCredential(smtpEmail, smtpPassword);
                client.Credentials = smtpUserInfo;
                client.Send(message);
                client.Dispose();
                message.Dispose();

            }
            catch (Exception e1)
            {

            } 
        }


        public string RelativeDate(DateTime theDate)
        {
            string result = theDate.TimeAgo();
            return result;
        }

        

        public void renderGenerateMailItem(string domainName, string mailTemplate, string requestId, List<MessagesModel> messages)
        {
            
            string mailBody = "";
            try
            {
                UserRequest userRequest = new DataAccess().getRequestDetailsByReqId(requestId);
                List<NueRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                List<NueRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
                List<AttachmentLogModel> attachmentLogModels = new DataAccess().getAttachmentLogs(requestId);
                List<MailItem> mailItems = new List<MailItem>();

                string requestStatusStr = "";
                if (userRequest.RequestStatus == "close")
                {
                    requestStatusStr = "Close";
                }
                else if (userRequest.RequestStatus == "completed")
                {
                    requestStatusStr = "Completed";
                }
                else if (userRequest.RequestStatus == "withdraw")
                {
                    requestStatusStr = "Withdraw";
                }
                else if (userRequest.RequestStatus == "In_Approval")
                {
                    requestStatusStr = "In Approval";
                }
                else if (userRequest.RequestStatus == "created")
                {
                    requestStatusStr = "Created";
                }

                if (userRequest.RequestSubType == "LeaveCancelation")
                {
                    NeuLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeaveCancelationModal.StartDate);
                    messageData.Add("End Date", neuLeaveCancelationModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeaveCancelationModal.Message;
                    int approverC = 0;
                    foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
                    {
                        if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                        {
                            approverC++;
                            string isSpproved = "Pending";
                            if(nueRequestAceessLog.Completed == 1)
                            {
                                isSpproved = "Completed";
                            }
                            var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                            messageData.Add("Request Approver_"+ approverC.ToString(), userApp.FullName + " (" + userApp.NTPLID + ") - "+ isSpproved);
                        }
                    }
                    messageData.Add("Created On", neuLeaveCancelationModal.AddedOn.ToLocalTime().ToString());

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "LeavePastApply")
                {
                    NeuLeavePastApplyModal neuLeavePastApplyModal = new DataAccess().getNeuLeavePastApplyDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeavePastApplyModal.StartDate);
                    messageData.Add("End Date", neuLeavePastApplyModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeavePastApplyModal.Message;
                    int approverC = 0;
                    foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
                    {
                        if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                        {
                            approverC++;
                            string isSpproved = "Pending";
                            if (nueRequestAceessLog.Completed == 1)
                            {
                                isSpproved = "Completed";
                            }
                            var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                            messageData.Add("Request Approver_" + approverC.ToString(), userApp.FullName + " (" + userApp.NTPLID + ") - " + isSpproved);
                        }
                    }
                    messageData.Add("Created On", neuLeavePastApplyModal.AddedOn.ToLocalTime().ToString());

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "LeaveWFHApply")
                {
                    NeuLeaveWFHApplyModal neuLeaveWFHApplyModal = new DataAccess().getNeuLeaveWFHApplyDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeaveWFHApplyModal.StartDate);
                    messageData.Add("End Date", neuLeaveWFHApplyModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeaveWFHApplyModal.Message;
                    int approverC = 0;
                    foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
                    {
                        if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                        {
                            approverC++;
                            string isSpproved = "Pending";
                            if (nueRequestAceessLog.Completed == 1)
                            {
                                isSpproved = "Completed";
                            }
                            var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                            messageData.Add("Request Approver_" + approverC.ToString(), userApp.FullName + " (" + userApp.NTPLID + ") - " + isSpproved);
                        }
                    }
                    messageData.Add("Created On", neuLeaveWFHApplyModal.AddedOn.ToLocalTime().ToString());

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "LeaveBalanceEnquiry")
                {
                    LeaveBalanceEnquiryModal leaveBalanceEnquiryModal = new DataAccess().getNeuLeaveBalanceEnquiryDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", leaveBalanceEnquiryModal.StartDate);
                    messageData.Add("End Date", leaveBalanceEnquiryModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", leaveBalanceEnquiryModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = leaveBalanceEnquiryModal.Message;
                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "HCMAddressProof")
                {
                    AddressProofModal addressProofModal = new DataAccess().getNeuAddressProofModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", addressProofModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = addressProofModal.Message;
                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "HCMEmployeeVerification")
                {
                    EmployeeVerificationReqModal employeeVerificationReqModal = new DataAccess().getNeuEmployeeVerificationReqModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", employeeVerificationReqModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = employeeVerificationReqModal.Message;
                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "SalaryCertificate")
                {
                    SalaryCertificateModal salaryCertificateModal = new DataAccess().getNeuSalaryCertificateModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", salaryCertificateModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = salaryCertificateModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }
                else if (userRequest.RequestSubType == "HCMGeneral")
                {
                    GeneralRequestModal generalRequestModal = new DataAccess().getNeuGeneralRequestModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", generalRequestModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = generalRequestModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        string mailTemplateGen = mailTemplate;
                        mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                            .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", userMessage);

                        MailItem mailItem = new MailItem();
                        mailItem.Subject = messagesModel.Message;
                        mailItem.Body = mailTemplateGen;
                        mailItem.To = "monin.jose@neudesic.com";
                        mailItem.Priority = true;
                        mailItems.Add(mailItem);
                    }
                }

                mailHandilar(mailItems);

            }
            catch (Exception e)
            {
                
            }
        }

        private string generateMailDataRow(Dictionary<string, string> messageData)
        {
            string returnVal = "";
            foreach (var item in messageData)
            {
                returnVal += "<tr>\r\n" +
                    "                              <td align=\"left\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;\">\r\n" +
                    item.Key +
                    "                              </td>\r\n" +
                    "                              <td align=\"left\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;\">\r\n" +
                    item.Value +
                    "                              </td>\r\n" +
                    "                            </tr>";
            }
            return returnVal;
        }

        public string generateRequestSearchUiRender(List<RequestSearchRender> requestSearchRenders, List<UserProfile> userProfiles)
        {
            string uiRender = "";
            if(requestSearchRenders != null && requestSearchRenders.Count > 0)
            {
                for (int i = 0; i < requestSearchRenders.Count; i++)
                {
                    RequestSearchRender requestSearchRender = requestSearchRenders[i];
                    UserRequest userRequest = requestSearchRender.userRequest;
                    UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();
                    string requestStatusStr = "";
                    string heading = "";
                    if (userRequest.RequestSubType == "LeaveCancelation")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Leave Cancelation";
                    }
                    else if (userRequest.RequestSubType == "LeavePastApply")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Leave Past Apply";
                    }
                    else if (userRequest.RequestSubType == "LeaveWFHApply")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Work From Home";
                    }
                    else if (userRequest.RequestSubType == "LeaveBalanceEnquiry")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Leave Balance Enquiry";
                    }
                    else if (userRequest.RequestSubType == "HCMAddressProof")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Address Proof";
                    }
                    else if (userRequest.RequestSubType == "HCMEmployeeVerification")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Employee Verification Enquiry";
                    }
                    else if (userRequest.RequestSubType == "SalaryCertificate")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Salary Certificate";
                    }
                    else if (userRequest.RequestSubType == "HCMGeneral")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Common Request";
                    }


                    if (userRequest.RequestStatus == "close")
                    {
                        requestStatusStr = "                                    <span class=\"label label-dark mr-2\">Close</span>\r\n";
                    }
                    else if (userRequest.RequestStatus == "completed")
                    {
                        requestStatusStr = "                                    <span class=\"label label-success mr-2\">Completed</span>\r\n";
                    }
                    else if (userRequest.RequestStatus == "withdraw")
                    {
                        requestStatusStr = "                                    <span class=\"label label-danger mr-2\">Withdraw</span>\r\n";
                    }
                    else if (userRequest.RequestStatus == "In_Approval")
                    {
                        requestStatusStr = "                                    <span class=\"label label-warning mr-2\">In Approval</span>\r\n";
                    }
                    else if (userRequest.RequestStatus == "created")
                    {
                        requestStatusStr = "                                    <span class=\"label label-primary mr-2\">Created</span>\r\n";
                    }
                    uiRender += "<div class=\"col-12 results\">\r\n" +
                    "                        <div class=\"pt-4 border-bottom\">\r\n" +
                    "                            <a class=\"d-block link h4 mb-0\" href=\"/HcmDashboard/SelfRequestDetails?requestId="+ userRequest.RequestId + "\" target=\"_blank\"><i class=\"mdi mdi-apple-keyboard-command\"></i> " + heading + "</a>\r\n" +
                    "                            <a class=\"page-url text-primary\" href=\"javascript:void(0)\">#" + userRequest.RequestId + "</a>\r\n" +
                    "                            <p class=\"page-description mt-1 w-75 text-muted\">\r\n" +
                    requestStatusStr +
                    "                                <span class=\"ml-2 mr-2\">" + requestOwner.FullName + " (" + requestOwner.NTPLID + ")</span>\r\n" +
                    "                                <span class=\"text-muted\">\r\n" +
                    "                                    <i class=\"mdi mdi-clock\"></i>\r\n" +
                    userRequest.AddedOn.ToLocalTime()  +
                    "                                </span>\r\n" +
                    "                            </p>\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>";
                }
            }
            if(uiRender.Trim() == "")
            {
                uiRender = "<div class=\"col-12 results\">\r\n" +
                    "                        <div class=\"pt-4 border-bottom\">\r\n" +
                    "                            <p class=\"page-description mt-1 w-75 text-muted\"> No data avilable for search query</p>\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>";
            }
            return uiRender;
        }

        public string generateLeaveCancelationUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeuLeaveCancelationModal neuLeaveCancelationModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if(nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-leave-cancelation-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('inter-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-approve-leave-cancelation-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#"+ userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>"+ requestOwner.FullName + " (" + requestOwner.NTPLID + ") "+((neuLeaveCancelationModal.Message != null && neuLeaveCancelationModal.Message.Trim() != "") ? neuLeaveCancelationModal.Message.Trim() : "has created new Leave Cancelation Request") +"</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">"+ neuLeaveCancelationModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);
           var temp ="                                            <div class=\"timeline-wrapper timeline-wrapper-warning hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Monin Jose (0790) has created new Leave Cancelation Request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">19</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:07.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-inverted timeline-wrapper-warning hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Comment Added</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis pharetra varius quam sit amet vulputate. Quisque mauris augue,</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-wrapper-success hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">New File Attached</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <div>\r\n" +
            "                                                            <div class=\"thumb\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
            "                                                            <div class=\"details\">\r\n" +
            "                                                                <p class=\"file-name\">favicon.png</p>\r\n" +
            "                                                                <div class=\"buttons\">\r\n" +
            "                                                                    <a href=\"/HcmAHDDashboard/DownloadAttachment?requestId=1000000000000000001&amp;vFile=20190513025413_.png\" target=\"_blank\" class=\"download\">Download</a>\r\n" +
            "                                                                </div>\r\n" +
            "                                                            </div>\r\n" +
            "                                                        </div>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-inverted timeline-wrapper-info hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">L1 Approval</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Mathew Job (0725) approved your request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">2019-05-31 16:40:08.033</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n" +

            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary hide\">\r\n" +
            "                                                <div class=\"timeline-badge\"></div>\r\n" +
            "                                                <div class=\"timeline-panel\">\r\n" +
            "                                                    <div class=\"timeline-heading\">\r\n" +
            "                                                        <h6 class=\"timeline-title\">Approved by HCM</h6>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-body\">\r\n" +
            "                                                        <p>Priya Ignatius (0580) approved your request</p>\r\n" +
            "                                                    </div>\r\n" +
            "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
            "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
            "                                                        <span class=\"hide\">25</span>\r\n" +
            "                                                        <span class=\"ml-auto font-weight-bold\">25th July 2016</span>\r\n" +
            "                                                    </div>\r\n" +
            "                                                </div>\r\n" +
            "                                            </div>\r\n";



                     uiRender += "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                                <div class=\"col-4\">\r\n" +
                    "\r\n" +
                    "                                    <div class=\"card thin-border\">\r\n" +
                    "                                        <div class=\"card-body\">\r\n" +
                    "                                            <h4 class=\"card-title\">Leave Cancelation Request</h4>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                        <div class=\"card-body bg-light\">\r\n" +
                    "                                            <div class=\"row text-center\">\r\n" +
                    "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
                   requestStatusStr +
                    "                                                </div>\r\n" +
                    "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
                    neuLeaveCancelationModal.AddedOn.ToLocalTime()+
                    "                                                </div>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                        <div class=\"card-body\">\r\n" +
                    "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
                    "                                            <span>"+ requestOwner.FullName +" ("+ requestOwner.NTPLID+ ") </span>\r\n" +
                    "                                            <br>\r\n" +
                    approverStr +
                    "                                            <h5 class=\"m-t-30\">Leave Start Date</h5>\r\n" +
                    "                                            <span>"+ neuLeaveCancelationModal.StartDate + "</span>\r\n" +
                    "                                            <br>\r\n" +
                    "                                            <h5 class=\"m-t-30\">Leave End Date</h5>\r\n" +
                    "                                            <span>"+ neuLeaveCancelationModal.EndDate + "</span>\r\n" +
                    "                                            <br>\r\n" +
                    "                                        </div>\r\n" +
                    "\r\n" +
                    "                                    </div>\r\n" +
                    "\r\n" +
                    "                                </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                            </div>\r\n" +
                    "\r\n" +
                    "\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "\r\n" +
                    "        </div>";

            return uiRender;
        }

        public string generateLeavePastApplyUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeuLeavePastApplyModal neuLeavePastApplyModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-leave-past-apply-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('inter-approve-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-approve-leave-past-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((neuLeavePastApplyModal.Message != null && neuLeavePastApplyModal.Message.Trim() != "") ? neuLeavePastApplyModal.Message.Trim() : "has created new Leave Cancelation Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + neuLeavePastApplyModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Leave Past Apply Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           neuLeavePastApplyModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <h5 class=\"m-t-30\">Leave Start Date</h5>\r\n" +
           "                                            <span>" + neuLeavePastApplyModal.StartDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                            <h5 class=\"m-t-30\">Leave End Date</h5>\r\n" +
           "                                            <span>" + neuLeavePastApplyModal.EndDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateLeaveWFHApplyUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeuLeaveWFHApplyModal neuLeaveWFHApplyModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-leave-wfh-apply-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-leave-wfh-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('inter-approve-wfh-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-approve-leave-wfh-apply-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((neuLeaveWFHApplyModal.Message != null && neuLeaveWFHApplyModal.Message.Trim() != "") ? neuLeaveWFHApplyModal.Message.Trim() : "has created new Work From Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + neuLeaveWFHApplyModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);
            
            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Work From Home Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           neuLeaveWFHApplyModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <h5 class=\"m-t-30\">Leave Start Date</h5>\r\n" +
           "                                            <span>" + neuLeaveWFHApplyModal.StartDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                            <h5 class=\"m-t-30\">Leave End Date</h5>\r\n" +
           "                                            <span>" + neuLeaveWFHApplyModal.EndDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateLeaveBalanceEnquiryUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, LeaveBalanceEnquiryModal leaveBalanceEnquiryModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            /*foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }*/

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-leave-bal-enq-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-leave-bal-enq-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-leave-bal-enq-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-approve-leave-bal-enq-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((leaveBalanceEnquiryModal.Message != null && leaveBalanceEnquiryModal.Message.Trim() != "") ? leaveBalanceEnquiryModal.Message.Trim() : "has created new Leave Balance Enquiry Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + leaveBalanceEnquiryModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Leave Balance Enquiry Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           leaveBalanceEnquiryModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <h5 class=\"m-t-30\">Start Date</h5>\r\n" +
           "                                            <span>" + leaveBalanceEnquiryModal.StartDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                            <h5 class=\"m-t-30\">End Date</h5>\r\n" +
           "                                            <span>" + leaveBalanceEnquiryModal.EndDate + "</span>\r\n" +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateAddressProofUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, AddressProofModal addressProofModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            /*foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }*/

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-address-proof-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-address-proof-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-address-proof-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-address-proof-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((addressProofModal.Message != null && addressProofModal.Message.Trim() != "") ? addressProofModal.Message.Trim() : "has created new Address Proof Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + addressProofModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Address Proof Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           addressProofModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateEmployeeVerificationReqUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, EmployeeVerificationReqModal employeeVerificationReqModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            /*foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }*/

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-employee-verification-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-employee-verification-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-employee-verification-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-employee-verification-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((employeeVerificationReqModal.Message != null && employeeVerificationReqModal.Message.Trim() != "") ? employeeVerificationReqModal.Message.Trim() : "has created new Employee Verification Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + employeeVerificationReqModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Employee Verification Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           employeeVerificationReqModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateSalaryCertificateUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, SalaryCertificateModal salaryCertificateModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            /*foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }*/

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-salary-certificate-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-salary-certificate-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-salary-certificate-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-salary-certificate-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((salaryCertificateModal.Message != null && salaryCertificateModal.Message.Trim() != "") ? salaryCertificateModal.Message.Trim() : "has created new Salary Certificate Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + salaryCertificateModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Salary Certificate Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           salaryCertificateModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }

        public string generateGeneralRequestUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, GeneralRequestModal generalRequestModal, List<NueRequestAceessLog> nueRequestAceessLogs, List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            UserProfile requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First<UserProfile>();

            /*foreach (NueRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = userProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<UserProfile>();
                    approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    "                            <br>\r\n";
                }
            }*/

            if (userRequest.RequestStatus == "close")
            {

            }
            else if (userRequest.RequestStatus == "withdraw")
            {

            }
            else if (userRequest.RequestStatus == "completed")
            {
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-general-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
                }
                else if (isApprover || ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                }
            }
            else
            {
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#commentModal-1\"><i class=\"mdi mdi-comment-outline\"></i> Comment </button>\r\n";
                uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" data-toggle=\"modal\" data-target=\"#fileAttchmentModal-1\"><i class=\"mdi mdi-attachment\"></i> Attach File </button>\r\n";
                if (isOwner)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-general-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-general-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-general-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = "                                    <span class=\"label label-dark\">Close</span>\r\n";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = "                                    <span class=\"label label-success\">Completed</span>\r\n";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
            }

            uiRender += "<div class=\"row\">\r\n" +
                    "            <div class=\"col-md-12 mb-4 mt-4\">\r\n" +
                    "                <div class=\"btn-toolbar\">\r\n" +
                    "                    <div class=\"btn-group inline\">\r\n" +
                    uiMenuRender +
                    "                    </div>\r\n" +
                    "                </div>\r\n" +
                    "            </div>\r\n" +
                    "        </div>\r\n" +
                    "\r\n" +
                    "        <div class=\"ahd-service-container\">\r\n" +
                    "\r\n" +
                    "            <div class=\"row\">\r\n" +
                    "                <div class=\"col-12\">\r\n" +
                    "                    <div class=\"card\">\r\n" +
                    "                        <div class=\"card-body\">\r\n" +
                    "                            <h4 class=\"card-title\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n" +
                    "                                    <p class=\"card-description hide\">Request timeline</p>\r\n" +
                    "                                    <div class=\"mt-4\">\r\n" +
                    "                                        <div class=\"vertical-timeline\">\r\n";

            uiRender += "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((generalRequestModal.Message != null && generalRequestModal.Message.Trim() != "") ? generalRequestModal.Message.Trim() : "has created new Common Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + generalRequestModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(userProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +
           "                                    </div>\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n" +
           "\r\n" +
           "                                    <div class=\"card thin-border\">\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h4 class=\"card-title\">Common Request</h4>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body bg-light\">\r\n" +
           "                                            <div class=\"row text-center\">\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
          requestStatusStr +
           "                                                </div>\r\n" +
           "                                                <div class=\"col-6 m-t-10 m-b-10\">\r\n" +
           generalRequestModal.AddedOn.ToLocalTime() +
           "                                                </div>\r\n" +
           "                                            </div>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                        <div class=\"card-body\">\r\n" +
           "                                            <h5 class=\"p-t-20\">Ticket Creator</h5>\r\n" +
           "                                            <span>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") </span>\r\n" +
           "                                            <br>\r\n" +
           approverStr +
           "                                            <br>\r\n" +
           "                                        </div>\r\n" +
           "\r\n" +
           "                                    </div>\r\n" +
           "\r\n" +
           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "\r\n" +
           "                            </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                        </div>\r\n" +
           "                    </div>\r\n" +
           "                </div>\r\n" +
           "            </div>\r\n" +
           "\r\n" +
           "        </div>";

            return uiRender;
        }


        static Random rnd = new Random();
        public string generateRequestLog(List<UserProfile> userProfiles, List<NueRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            List<string> colorPallet = new List<string>() {
                "timeline-wrapper-warning",
                "timeline-wrapper-danger",
                "timeline-wrapper-success",
                "timeline-wrapper-info",
                "timeline-wrapper-primary",
            };


            string uiRender = "";
            for (int i = 0; i < nueRequestActivityModels.Count; i++)
            {
                if(nueRequestActivityModels.ElementAt(i) != null)
                {
                    NueRequestActivityModel nueRequestActivityModel = nueRequestActivityModels.ElementAt(i);
                    var className = "";
                    if ((i) % 2 == 0)
                    {
                        className = " timeline-wrapper timeline-inverted "+ colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    else
                    {
                        className = " timeline-wrapper " + colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    string heading = "";
                    string body = "";
                    bool able = false;
                    if(nueRequestActivityModel.PayloadTypeDesc == "Comment")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") - " + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Comment Added</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "L1 Approval")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Level 1 Approval</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "HCM Approval")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">HCM Approval</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "Close")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Request Closed</h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "Withdraw")
                    {
                        able = true;
                        body = "                                                        <p>" + nueRequestActivityModel.Payload + "</p>\r\n";
                        heading = "                                                        <h6 class=\"timeline-title\">Request Withdrawn </h6>\r\n";
                    }
                    else if (nueRequestActivityModel.PayloadTypeDesc == "File")
                    {
                        var internalFile = attachmentLogModels.Where(x => x.Request == nueRequestActivityModel.Request && x.VFileName == nueRequestActivityModel.Payload);
                        if(internalFile != null && internalFile.Count() > 0)
                        {
                            able = true;
                            AttachmentLogModel attachmentLogModel = internalFile.First();
                            UserProfile attachmentOwner = userProfiles.Where(x => x.Id == attachmentLogModel.UserId).First<UserProfile>();
                            heading = "                                                        <h6 class=\"timeline-title\"> File Attached <i class=\"mdi mdi-attachment\"></i> </h6>\r\n";
                            body = "                                                        <div>\r\n" +
                                            "                                                            <div class=\"thumb hide\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                                            "                                                            <div class=\"details\">\r\n" +
                                            "                                                                <p class=\"file-name\">"+ nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") </p>\r\n" +
                                            "                                                                <div class=\"buttons\">\r\n" +
                                            "                                                                    <a href=\"/HcmDashboard/DownloadAttachment?requestId="+ attachmentLogModel.Request + "&amp;vFile="+ attachmentLogModel.VFileName + "\" target=\"_blank\" class=\"download\">" + attachmentLogModel.FileName + "" + attachmentLogModel.FileExt + "</a>\r\n" +
                                            "                                                                </div>\r\n" +
                                            "                                                            </div>\r\n" +
                                            "                                                        </div>\r\n";
                        }
                    }
                    if (able)
                    {
                        uiRender += "                                            <div class=\"" + className + "\">\r\n" +
                                        "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                        "                                                <div class=\"timeline-panel\">\r\n" +
                                        "                                                    <div class=\"timeline-heading\">\r\n" +
                                        heading +
                                        "                                                    </div>\r\n" +
                                        "                                                    <div class=\"timeline-body\">\r\n" +
                                        body +
                                        "                                                    </div>\r\n" +
                                        "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                        "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                        "                                                        <span class=\"hide\">19</span>\r\n" +
                                        "                                                        <span class=\"ml-auto font-weight-bold\">" + nueRequestActivityModel.AddedOn.ToLocalTime() + "</span>\r\n" +
                                        "                                                    </div>\r\n" +
                                        "                                                </div>\r\n" +
                                        "                                            </div>\r\n";
                    }
                }
            }
            return uiRender;
        }
    }
}