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

        public static bool isValidUserObject(UserProfile userProfile)
        {
            try
            {
                if(userProfile == null)
                {
                    throw new Exception();
                }

                if(userProfile.Email == null || userProfile.Email.Trim() == "")
                {
                    throw new Exception();
                }

                if (userProfile.FullName == null || userProfile.FullName.Trim() == "")
                {
                    throw new Exception();
                }

                return true;


            }
            catch (Exception e1)
            {
                return false;
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
                List<NuRequestAceessLog> nueRequestAceessLogs = new DataAccess().getRequestAccessList(requestId);
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                List<NuRequestActivityModel> nueRequestActivityModels = new DataAccess().getRequestLogs(requestId);
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
                    NeLeaveCancelationModal neuLeaveCancelationModal = new DataAccess().getNeuLeaveCancelationDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Leave Cancelation");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeaveCancelationModal.StartDate);
                    messageData.Add("End Date", neuLeaveCancelationModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeaveCancelationModal.Message;
                    int approverC = 0;
                    foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
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
                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "LeavePastApply")
                {
                    NeLeavePastApplyModal neuLeavePastApplyModal = new DataAccess().getNeuLeavePastApplyDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Past Leave Apply");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeavePastApplyModal.StartDate);
                    messageData.Add("End Date", neuLeavePastApplyModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeavePastApplyModal.Message;
                    int approverC = 0;
                    foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
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

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "LeaveWFHApply")
                {
                    NeLeaveWFHApplyModal neuLeaveWFHApplyModal = new DataAccess().getNeuLeaveWFHApplyDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Work From Home");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Start Date", neuLeaveWFHApplyModal.StartDate);
                    messageData.Add("End Date", neuLeaveWFHApplyModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    string userMessage = neuLeaveWFHApplyModal.Message;
                    int approverC = 0;
                    foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
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

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "LeaveBalanceEnquiry")
                {
                    LeaveBalanceEnquiryModal leaveBalanceEnquiryModal = new DataAccess().getNeuLeaveBalanceEnquiryDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Leave Balance Enquiry");
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

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "HCMAddressProof")
                {
                    AddressProofModal addressProofModal = new DataAccess().getNeuAddressProofModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Address Proof");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", addressProofModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = addressProofModal.Message;
                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "HCMEmployeeVerification")
                {
                    EmployeeVerificationReqModal employeeVerificationReqModal = new DataAccess().getNeuEmployeeVerificationReqModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Employee Verification");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", employeeVerificationReqModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = employeeVerificationReqModal.Message;
                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "SalaryCertificate")
                {
                    SalaryCertificateModal salaryCertificateModal = new DataAccess().getNeuSalaryCertificateModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Salary Certificate");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", salaryCertificateModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = salaryCertificateModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "HCMGeneral")
                {
                    GeneralRequestModal generalRequestModal = new DataAccess().getNeuGeneralRequestModalDetails(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    messageData.Add("Request Category", "Common");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", generalRequestModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = generalRequestModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if(mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "DomesticTrip")
                {
                    DomesticTripRequestModal domesticTripRequestModal = new DataAccess().getNeuDomesticTripRequestModal(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    string accommodation = "No";
                    if (domesticTripRequestModal.Accommodation == 1)
                    {
                        accommodation = "Yes";
                    }
                    messageData.Add("Request Category", "Domestic Travel");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Accommodation", accommodation);
                    messageData.Add("From", domesticTripRequestModal.LocationFrom);
                    messageData.Add("To", domesticTripRequestModal.LocationTo);
                    messageData.Add("Start Date", domesticTripRequestModal.StartDate);
                    messageData.Add("End Date", domesticTripRequestModal.EndDate);
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", domesticTripRequestModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = domesticTripRequestModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if (mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                }
                else if (userRequest.RequestSubType == "InternationalTrip")
                {
                    InternationalTripRequestModal internationalTripRequestModal = new DataAccess().getInternationalTripRequestModal(requestId);
                    var requestOwner = userProfiles.Where(x => x.Id == userRequest.OwnerId).First();
                    Dictionary<string, string> messageData = new Dictionary<string, string>();
                    string needVisiaProcessing = "No";
                    if (internationalTripRequestModal.NeedVisiaProcessing == 1)
                    {
                        needVisiaProcessing = "Yes";
                    }
                    messageData.Add("Request Category", "International Travel");
                    messageData.Add("Ticket Creator", requestOwner.FullName + " (" + requestOwner.NTPLID + ")");
                    messageData.Add("Need Visia Processing", needVisiaProcessing);
                    messageData.Add("Place To Visit", internationalTripRequestModal.PlaceToVisit);
                    messageData.Add("Project Name", internationalTripRequestModal.ProjectName);
                    messageData.Add("Planned Travel Date", internationalTripRequestModal.StartDate);
                    messageData.Add("Request Status", requestStatusStr);
                    messageData.Add("Created On", internationalTripRequestModal.AddedOn.ToLocalTime().ToString());
                    string userMessage = internationalTripRequestModal.Message;

                    foreach (MessagesModel messagesModel in messages)
                    {
                        string messageTitle = messagesModel.EmptyMessage;
                        string requestUrl = domainName + messagesModel.Target;
                        var mailToUser = userProfiles.Where(x => x.Id == messagesModel.UserId).First();

                        if (mailToUser.userPreference.IsMailCommunication == 1)
                        {
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
                    else if (userRequest.RequestSubType == "DomesticTrip")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> Domestic Travel Request";
                    }
                    else if (userRequest.RequestSubType == "InternationalTrip")
                    {
                        heading = "<i class=\"mdi mdi-apple-keyboard-command\"></i> International Travel Request";
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

        public string generateLeaveCancelationUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeLeaveCancelationModal neuLeaveCancelationModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfile, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfile.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\""+ userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }


            foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if(nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {

                    var userApp = nueUserProfile.Where(x => x.Id == nueRequestAceessLog.UserId).First<DAL.NueUserProfile>();

                    string communicaterLink = "";

                    if (userApp.NeuUserPreference2.First().IsMailCommunication == 1)
                    {
                        communicaterLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                        communicaterLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                    }

                    approverStr += "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                                "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                                "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                                "                                                                   <h4 class=\"timeline-title\">Request Approver</h4>" +
                                "                                                                   <p>" + userApp.FullName + " (" + userApp.NTPLID + ") " + communicaterLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                                "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n";
                    

                    /*approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    communicaterLink+
                    "                            <br>\r\n";*/

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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = "                                    <span class=\"label label-danger\">Withdraw</span>\r\n";
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = "                                    <span class=\"label label-warning\">In Approval</span>\r\n";
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = "                                    <span class=\"label label-primary\">Created</span>\r\n";
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\""+ userRequest.RequestId + "\" title=\"copy\"></i>"+
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((neuLeaveCancelationModal.Message != null && neuLeaveCancelationModal.Message.Trim() != "") ? neuLeaveCancelationModal.Message.Trim() : "has created new Leave Cancelation Request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + neuLeaveCancelationModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(nueUserProfile, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Leave Cancelation Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> "+ requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> "+ neuLeaveCancelationModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +
                    
                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +
                    
                    "\r\n" +
                    approverStr+


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Leave Start Date</h4>" +
                    "                                                                   <p>" + neuLeaveCancelationModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Leave End Date</h4>" +
                    "                                                                   <p>" + neuLeaveCancelationModal.EndDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>"+






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
        

        public string generateLeavePastApplyUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeLeavePastApplyModal neuLeavePastApplyModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }

            foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = nueUserProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<DAL.NueUserProfile>();

                    string communicaterLink = "";

                    if(userApp.NeuUserPreference2.First().IsMailCommunication == 1)
                    {
                        communicaterLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                        communicaterLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                    }

                    approverStr += "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                                "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                                "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                                "                                                                   <h4 class=\"timeline-title\">Request Approver</h4>" +
                                "                                                                   <p>" + userApp.FullName + " (" + userApp.NTPLID + ") " + communicaterLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                                "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n";


                    /*approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    communicaterLink+
                    "                            <br>\r\n";*/
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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Leave Past Apply Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + neuLeavePastApplyModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Leave Start Date</h4>" +
                    "                                                                   <p>" + neuLeavePastApplyModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Leave End Date</h4>" +
                    "                                                                   <p>" + neuLeavePastApplyModal.EndDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateLeaveWFHApplyUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, NeLeaveWFHApplyModal neuLeaveWFHApplyModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }

            foreach (NuRequestAceessLog nueRequestAceessLog in nueRequestAceessLogs)
            {
                if (nueRequestAceessLog.UserId != nueRequestAceessLog.OwnerId)
                {
                    var userApp = nueUserProfiles.Where(x => x.Id == nueRequestAceessLog.UserId).First<DAL.NueUserProfile>();
                    string communicaterLink = "";

                    if (userApp.NeuUserPreference2.First().IsMailCommunication == 1)
                    {
                        communicaterLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                        communicaterLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\"  data-target=\"" + userApp.Email + "\"></i>";
                    }
                    approverStr += "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                                "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                                "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                                "                                                                   <h4 class=\"timeline-title\">Request Approver</h4>" +
                                "                                                                   <p>" + userApp.FullName + " (" + userApp.NTPLID + ") " + communicaterLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                                "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n";


                    /*approverStr += "                            <h5 class=\"p-t-20\">Ticket Approver</h5>\r\n" +
                    "                            <span>" + userApp.FullName + " (" + userApp.NTPLID + ")</span>\r\n" +
                    communicaterLink+
                    "                            <br>\r\n";*/
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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Work From Home Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + neuLeaveWFHApplyModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Start Date</h4>" +
                    "                                                                   <p>" + neuLeaveWFHApplyModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">End Date</h4>" +
                    "                                                                   <p>" + neuLeaveWFHApplyModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateLeaveBalanceEnquiryUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, LeaveBalanceEnquiryModal leaveBalanceEnquiryModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }
            
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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Leave Balance Enquiry Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + leaveBalanceEnquiryModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Start Date</h4>" +
                    "                                                                   <p>" + leaveBalanceEnquiryModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">End Date</h4>" +
                    "                                                                   <p>" + leaveBalanceEnquiryModal.EndDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                   
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateAddressProofUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, AddressProofModal addressProofModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }


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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Address Proof Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + addressProofModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline single small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success hide\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +
                    
                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                   
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateEmployeeVerificationReqUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, EmployeeVerificationReqModal employeeVerificationReqModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }


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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Employee Verification Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + employeeVerificationReqModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline single small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success hide\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +
                    

                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                   
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateSalaryCertificateUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, SalaryCertificateModal salaryCertificateModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }


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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Salary Certificate Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + salaryCertificateModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                   "                                                            <div class=\"vertical-without-time vertical-timeline single small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success hide\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +
                    
                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateGeneralRequestUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, GeneralRequestModal generalRequestModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }


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
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
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
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Common Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + generalRequestModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline single small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success hide\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +
                    
                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateDomesticTripRequestUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, DomesticTripRequestModal domesticTripRequestModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }

            string accommodation = "No";
            if(domesticTripRequestModal.Accommodation == 1)
            {
                accommodation = "Yes";
            }

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
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-domestic-travel-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
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
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-domestic-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-hcm-domestic-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-domestic-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((domesticTripRequestModal.Message != null && domesticTripRequestModal.Message.Trim() != "") ? domesticTripRequestModal.Message.Trim() : "has created new domestic travel request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + domesticTripRequestModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +



                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            Domestic Travel Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + domesticTripRequestModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Accommodation</h4>" +
                    "                                                                   <p>" + accommodation + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">From</h4>" +
                    "                                                                   <p>" + domesticTripRequestModal.LocationFrom + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">To</h4>" +
                    "                                                                   <p>" + domesticTripRequestModal.LocationTo + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\"> Start Date</h4>" +
                    "                                                                   <p>" + domesticTripRequestModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">End Date</h4>" +
                    "                                                                   <p>" + domesticTripRequestModal.EndDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                   
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +






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

        public string generateInternationalTripUiRender(bool isOwner, bool ishcm, bool isApprover, UserProfile currentUser, UserRequest userRequest, InternationalTripRequestModal internationalTripRequestModal, List<NuRequestAceessLog> nueRequestAceessLogs, List<DAL.NueUserProfile> nueUserProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
        {
            string uiRender = "";
            string uiMenuRender = "";
            string approverStr = "";

            string communicaterOwnerLink = "";

            DAL.NueUserProfile requestOwner = nueUserProfiles.Where(x => x.Id == userRequest.OwnerId).First<DAL.NueUserProfile>();
            if (requestOwner.NeuUserPreference2.First().IsMailCommunication == 1)
            {
                communicaterOwnerLink += "<i class=\"mdi mdi-facebook-messenger im-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
                communicaterOwnerLink += "<i class=\"mdi mdi-email-outline mail-tigger cursor-pointer\" data-id=\"" + userRequest.RequestId + "\" data-target=\"" + requestOwner.Email + "\"></i>";
            }

            string needVisiaProcessing = "No";
            if (internationalTripRequestModal.NeedVisiaProcessing == 1)
            {
                needVisiaProcessing = "Yes";
            }

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
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('close-hcm-International-travel-request')\"><i class=\"mdi mdi-close-circle-outline\"></i> Close </button>\r\n";
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
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('withdraw-hcm-International-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Withdraw </button>\r\n";
                }
                if (isApprover)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn hide\" onclick=\"showSwal('inter-approve-hcm-International-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve </button>\r\n";
                }
                if (ishcm)
                {
                    uiMenuRender += "                        <button type=\"button\" class=\"btn btn-sm btn-inverse-info inbox-inline-btn\" onclick=\"showSwal('final-hcm-International-travel-request')\"><i class=\"mdi mdi-compare text-primary\"></i> Approve Request </button>";
                }
            }

            string requestStatusStr = "";
            if (userRequest.RequestStatus == "close")
            {
                requestStatusStr = " Close <span class=\"badge badge-dark badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "completed")
            {
                requestStatusStr = " Completed  <span class=\"badge badge-success badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "withdraw")
            {
                requestStatusStr = " Withdraw  <span class=\"badge badge-danger badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "In_Approval")
            {
                requestStatusStr = " In Approval  <span class=\"badge badge-warning badge-dot badge-dot-lg super\"></span>";
            }
            else if (userRequest.RequestStatus == "created")
            {
                requestStatusStr = " Created  <span class=\"badge badge-primary badge-dot badge-dot-lg super\"></span>";
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
                    "                            <h4 class=\"card-title hide\">Request: <span class=\"editable editable-click cursor-default\">#" + userRequest.RequestId + "</span></h4>\r\n" +
                    "\r\n" +
                    "                            <div class=\"row\">\r\n" +
                    "                                <div class=\"col-8\">\r\n";


            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-store icon-gradient bg-mixed-hopes\"></i>\r\n" +
                    "                            Request: #" + userRequest.RequestId + "\r\n" +
                    "<i class=\"mdi mdi-content-copy ml-1 cursor-pointer ml-4 jq-copy\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "\r\n" +

                    "                                        <div class=\"vertical-timeline\">\r\n" +

                            "                                            <div class=\"timeline-wrapper timeline-wrapper-primary\">\r\n" +
                                    "                                                <div class=\"timeline-badge\"></div>\r\n" +
                                    "                                                <div class=\"timeline-panel\">\r\n" +
                                    "                                                    <div class=\"timeline-heading\">\r\n" +
                                    "                                                        <h6 class=\"timeline-title\">Request Created</h6>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-body\">\r\n" +
                                    "                                                        <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + ((internationalTripRequestModal.Message != null && internationalTripRequestModal.Message.Trim() != "") ? internationalTripRequestModal.Message.Trim() : "has created new international travel request") + "</p>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                    <div class=\"timeline-footer d-flex align-items-center\">\r\n" +
                                    "                                                        <i class=\"mdi mdi-heart-outline text-muted mr-1 hide\"></i>\r\n" +
                                    "                                                        <span class=\"hide\">19</span>\r\n" +
                                    "                                                        <span class=\"ml-auto font-weight-bold\">" + internationalTripRequestModal.AddedOn.ToLocalTime() + "</span>\r\n" +
                                    "                                                    </div>\r\n" +
                                    "                                                </div>\r\n" +
                                    "                                            </div>\r\n";
            uiRender += generateRequestLog(nueUserProfiles, nueRequestActivityModels, attachmentLogModels);

            uiRender += "                                        </div>\r\n" +

                

                    "\r\n" +
                    "                    </div>\r\n" +
                   
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +



           "                                </div>\r\n" +
           "\r\n" +
           "\r\n" +
           "                                <div class=\"col-4\">\r\n";

            uiRender += "<div class=\"card-hover-shadow-2x mb-3 card widget\">\r\n" +
                    "\r\n" +
                    "                    <div class=\"card-header-tab card-header\">\r\n" +
                    "                        <div class=\"card-header-title font-size-lg text-capitalize font-weight-normal\">\r\n" +
                    "                            <i class=\"header-icon lnr lnr-dice icon-gradient bg-happy-itmeo\"></i>\r\n" +
                    "                            International Travel Request\r\n" +
                    "                        </div>\r\n" +
                    "                    </div>\r\n" +
                    "\r\n" +
                    "                    <div class=\"p-0 card-body\">\r\n" +
                    "                        <div class=\"dropdown-menu-header mt-0 mb-0\">\r\n" +
                    "                            <div class=\"dropdown-menu-header-inner bg-heavy-rain\">\r\n" +
                    "                                <div class=\"menu-header-image opacity-2 dd-header-bg-5\"></div>\r\n" +
                    "                                <div class=\"menu-header-content text-dark\">\r\n" +
                    "                                    <h5 class=\"menu-header-title\"> " + requestStatusStr + " </h5>\r\n" +
                    "                                    <h6 class=\"menu-header-subtitle\"> \r\n" +
                    "                                        Created: \r\n" +
                    "                                        <b class=\"text-danger\"> " + internationalTripRequestModal.AddedOn.ToLocalTime() + " </b> \r\n" +
                    "                                        <i class=\"mdi mdi-content-copy ml-1 cursor-pointer\" data-target=\"" + userRequest.RequestId + "\" title=\"copy\"></i>\r\n" +
                    "                                    </h6>\r\n" +
                    "                                 </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                        <div class=\"card-tabbed-header\">\r\n" +
                    "                            <div class=\"tabs-animated tabs-animated-shadow\" justify=\"justified\">\r\n" +
                    "                                <div class=\"tab-content\">\r\n" +
                    "                                    <div class=\"tab-pane active ng-star-inserted\">\r\n" +
                    "                                        <div class=\"scroll-gradient ng-star-inserted\">\r\n" +
                    "                                            <div class=\"scroll-area-sm shadow-overflow\">\r\n" +
                    "                                                <perfect-scrollbar class=\"ps-show-limits\">\r\n" +
                    "\r\n" +
                    "                                                    <div class=\"ps ps--active-y\">\r\n" +
                    "                                                        <div class=\"ps-content\">\r\n" +
                    "                                                            <div class=\"vertical-without-time vertical-timeline small widget vertical-timeline--animate vertical-timeline--one-column\">\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Request Creator</h4>" +
                    "                                                                   <p>" + requestOwner.FullName + " (" + requestOwner.NTPLID + ") " + communicaterOwnerLink + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    approverStr +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Need Visia Processing</h4>" +
                    "                                                                   <p>" + needVisiaProcessing + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +


                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Place To Visit</h4>" +
                    "                                                                   <p>" + internationalTripRequestModal.PlaceToVisit + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Planned Travel Date</h4>" +
                    "                                                                   <p>" + internationalTripRequestModal.StartDate + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +

                    "\r\n" +
                    "                                                                <div class=\"vertical-timeline-item vertical-timeline-element\"><div>" +
                    "                                                                   <span class=\"vertical-timeline-element-icon bounce-in\"><i class=\"badge badge-dot badge-dot-xl badge-success\"></i></span>" +
                    "                                                                   <div class=\"vertical-timeline-element-content bounce-in\">" +
                    "                                                                   <h4 class=\"timeline-title\">Project Name</h4>" +
                    "                                                                   <p>" + internationalTripRequestModal.ProjectName + " <a class=\"hide\" href=\"null\"></a></p>" +
                    "                                                                   <span class=\"vertical-timeline-element-date\"></span></div></div></div>\r\n" +

                    "\r\n" +
                    
                    "\r\n" +
                    "                                                            </div>\r\n" +
                    "                                                        </div>\r\n" +
                    "                                                    </div>\r\n" +
                    "\r\n" +
                    "                                                </perfect-scrollbar>\r\n" +
                    "                                            </div>\r\n" +
                    "                                        </div>\r\n" +
                    "                                    </div>\r\n" +
                    "                                </div>\r\n" +
                    "                            </div>\r\n" +
                    "                        </div>\r\n" +
                    "\r\n" +
                    "                    </div>\r\n" +
                    
                    "<div class=\"d-block text-right card-footer bg-dark\"></div>" +

                    "                </div>" +

                    

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
        public string generateRequestLog(List<DAL.NueUserProfile> nueUserProfile, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
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
                    NuRequestActivityModel nueRequestActivityModel = nueRequestActivityModels.ElementAt(i);
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
                            DAL.NueUserProfile attachmentOwner = nueUserProfile.Where(x => x.Id == attachmentLogModel.UserId).First<DAL.NueUserProfile>();
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

        public string generateRequestLog(List<UserProfile> userProfiles, List<NuRequestActivityModel> nueRequestActivityModels, List<AttachmentLogModel> attachmentLogModels)
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
                if (nueRequestActivityModels.ElementAt(i) != null)
                {
                    NuRequestActivityModel nueRequestActivityModel = nueRequestActivityModels.ElementAt(i);
                    var className = "";
                    if ((i) % 2 == 0)
                    {
                        className = " timeline-wrapper timeline-inverted " + colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    else
                    {
                        className = " timeline-wrapper " + colorPallet[rnd.Next(colorPallet.Count)];
                    }
                    string heading = "";
                    string body = "";
                    bool able = false;
                    if (nueRequestActivityModel.PayloadTypeDesc == "Comment")
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
                        if (internalFile != null && internalFile.Count() > 0)
                        {
                            able = true;
                            AttachmentLogModel attachmentLogModel = internalFile.First();
                            UserProfile attachmentOwner = userProfiles.Where(x => x.Id == attachmentLogModel.UserId).First<UserProfile>();
                            heading = "                                                        <h6 class=\"timeline-title\"> File Attached <i class=\"mdi mdi-attachment\"></i> </h6>\r\n";
                            body = "                                                        <div>\r\n" +
                                            "                                                            <div class=\"thumb hide\"><i class=\"mdi mdi-attachment\"></i></div>\r\n" +
                                            "                                                            <div class=\"details\">\r\n" +
                                            "                                                                <p class=\"file-name\">" + nueRequestActivityModel.FullName + " (" + nueRequestActivityModel.NTPLID + ") </p>\r\n" +
                                            "                                                                <div class=\"buttons\">\r\n" +
                                            "                                                                    <a href=\"/HcmDashboard/DownloadAttachment?requestId=" + attachmentLogModel.Request + "&amp;vFile=" + attachmentLogModel.VFileName + "\" target=\"_blank\" class=\"download\">" + attachmentLogModel.FileName + "" + attachmentLogModel.FileExt + "</a>\r\n" +
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