using Hangfire;
using HCMApi.DAL;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi.Modal
{

    public class MaterialTableCol
    {
        public string title { get; set; }
        public string field { get; set; }

        public MaterialTableCol() { }

        public MaterialTableCol(string title, string field)
        {
            this.title = title;
            this.field = field;
        }
    }

    public class Utils
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UiMaterialTableModel DepartmentRequestListUiTableRender(List<DAL.MichaelDepartmentRequestMaster> michaelDepartmentRequestMasters)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            //"[{\"Id\":1,\"Name\":\"Leave Cancellation\"},{\"Id\":2,\"Name\":\"Past Leave Apply\"}]"
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"Name\",\"field\":\"Name\"}");
            dataCols.Add("{\"title\":\"Active/Deactive\",\"field\":\"Active\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            var requestTypes = michaelDepartmentRequestMasters.Select(i => new { i.Id, i.RequestTypeName, i.Active });
            List<string> dataRows = new List<string>();
            foreach (var item in requestTypes)
            {
                dataRows.Add("{\"Id\":\"" + item.Id + "\",\"Name\":\"" + item.RequestTypeName + "\",\"Active\":\"" + (item.Active == 1 ? "Active" : "Deactive") + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetSelfNewRequestSummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            //"[{\"Id\":1,\"Name\":\"Leave Cancellation\"},{\"Id\":2,\"Name\":\"Past Leave Apply\"}]"
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            //dataCols.Add("{\"title\":\"Active/Deactive\",\"field\":\"Active\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            //var requestTypes = michaelDepartmentRequestMasters.Select(i => new { i.Id, i.RequestTypeName, i.Active });
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetSelfNewRequestHistorySummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetApproverNewRequestSummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"User\",\"field\":\"User\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"User\":\"" + item.User + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetApproverNewRequestHistorySummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"User\",\"field\":\"User\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"User\":\"" + item.User + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetAssigneeNewRequestSummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"User\",\"field\":\"User\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"User\":\"" + item.User + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public UiMaterialTableModel GetAssigneeNewRequestHistorySummaryUiTableRender(List<MichaeRequestSummaryItem> michaeRequestSummaryItems)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"User\",\"field\":\"User\"}");
            dataCols.Add("{\"title\":\"RequestType\",\"field\":\"RequestType\"}");
            dataCols.Add("{\"title\":\"RequestStatus\",\"field\":\"RequestStatus\"}");
            dataCols.Add("{\"title\":\"DateAdded\",\"field\":\"DateAdded\"}");
            dataCols.Add("{\"title\":\"DateModified\",\"field\":\"DateModified\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            List<string> dataRows = new List<string>();
            foreach (var item in michaeRequestSummaryItems)
            {
                dataRows.Add("{\"Id\":\"" + item.RequestId + "\",\"User\":\"" + item.User + "\",\"RequestType\":\"" + item.RequestType + "\",\"RequestStatus\":\"" + item.RequestStatus + "\",\"DateAdded\":\"" + item.DateAdded + "\",\"DateModified\":\"" + item.DateModified + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public void mailHandilar(List<MailItem> mailItems)
        {
            try
            {
                foreach (var item in mailItems)
                {
                    BackgroundJob.Enqueue(() => SendMailAsync(item));
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

                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpEmail = "nuhcmuser@gmail.com";
                string smtpPassword = "GoodPassword@#neudesic.net";
                //var message = new MailMessage(new MailAddress(smtpEmail), new MailAddress(mailItem.To));
                var message = new MailMessage(new MailAddress(smtpEmail), new MailAddress("monin.jose@neudesic.com"));
                message.Subject = mailItem.Subject;
                message.Body = mailItem.Body;
                message.IsBodyHtml = true;
                message.HeadersEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.Headers.Add("From", smtpEmail);
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

        public void renderGenerateMailItem(string domainName, string mailTemplate, MichaelRequestViewerData michaelRequestViewerData, List<MessagesModel> messages)
        {
            Dictionary<string, string> messageData = new Dictionary<string, string>();
            messageData.Add("Request Category", michaelRequestViewerData.RequestType);
            foreach (var item in michaelRequestViewerData.SidebarData)
            {
                messageData.Add(Utils.FirstCharToUpper(item.key), Utils.FirstCharToUpper(item.value));
            }
            List<MailItem> mailItems = new List<MailItem>();
            foreach (MessagesModel messagesModel in messages)
            {
                string messageTitle = messagesModel.EmptyMessage;
                string requestUrl = messagesModel.Target;
                var mailToUser = messagesModel.Email;

                string mailTemplateGen = mailTemplate;
                mailTemplateGen = mailTemplateGen.Replace("{TitleMessage}", messageTitle).Replace("{RequestLink}", requestUrl)
                    .Replace("{RequestBody}", generateMailDataRow(messageData)).Replace("{RequestMessage}", messagesModel.EmptyMessage);

                MailItem mailItem = new MailItem();
                mailItem.Subject = messagesModel.Message;
                mailItem.Body = mailTemplateGen;
                mailItem.To = mailToUser;
                mailItem.Priority = true;
                mailItems.Add(mailItem);
            }

            if(mailItems.Count > 0)
            {
                mailHandilar(mailItems);
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

        /*public UiMaterialTableModel DepartmentRequestListUiTableRender(List<DAL.MichaelDepartmentRequestTypeMaster> departmentRequestList)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            //"[{\"Id\":1,\"Name\":\"Leave Cancellation\"},{\"Id\":2,\"Name\":\"Past Leave Apply\"}]"
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"Name\",\"field\":\"Name\"}");
            dataCols.Add("{\"title\":\"Active/Deactive\",\"field\":\"Active\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            var requestTypes = departmentRequestList.Select(i => new { i.Id, i.RequestTypeName, i.Active });
            List<string> dataRows = new List<string>();
            foreach (var item in requestTypes)
            {
                dataRows.Add("{\"Id\":\"" + item.Id + "\",\"Name\":\"" + item.RequestTypeName + "\",\"Active\":\"" + (item.Active == 1 ? "Active":"Deactive") + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }*/

        public ListUserId generateUserDropdownList(List<DAL.NueUserProfile> userProfiles)
        {
            ListUserId listUserId = new ListUserId();
            List<int> userIds = new List<int>();
            List<string> emails = new List<string>();

            if(userProfiles != null && userProfiles.Count > 0)
            {
                for (int i = 0; i < userProfiles.Count; i++)
                {
                    try
                    {
                        userIds.Add(userProfiles[i].Id);
                        emails.Add(userProfiles[i].Email);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            listUserId.userIds = userIds;
            listUserId.emails = emails;
            return listUserId;
        }

        public List<BotIntnetItem> getBotWelcomeIntents(List<MichaelDepartmentRequestMaster> michaelDepartmentRequestMasters)
        {
            List<BotIntnetItem> botIntnetItems = new List<BotIntnetItem>();

            botIntnetItems.Add(new BotIntnetItem() { id = "welcome", message = "Hi, I am Michael", trigger = "0" });
            botIntnetItems.Add(new BotIntnetItem() { id = "0", message = "These are some of the things I can help you with", trigger = "MenuOption" });
            List<Option> optionList = new List<Option>();
            optionList.Add(new Option() { value = "GetBotRequestSearchIntent", label = "Search Request", trigger = "IntentSwitch" });
            optionList.Add(new Option() { value = "GetBotHCMRequestIntent", label = "HCM Request", trigger = "IntentSwitch" });
            botIntnetItems.Add(new BotIntnetItem() { id = "MenuOption", options = optionList });
            botIntnetItems.Add(new BotIntnetItem() { id = "IntentSwitch", component = "IntentSwitchComponent", waitAction = true });
            return botIntnetItems;
        }

        public List<BotIntnetItem> getBotIntents(List<MichaelDepartmentRequestMaster> michaelDepartmentRequestMasters, MichaeUserAccess michaeUserAccess)
        {
            List<BotIntnetItem> botIntnetItems = new List<BotIntnetItem>();

            botIntnetItems.Add(new BotIntnetItem() { id= "welcome", message= "Hi, I am Michael", trigger="0" });
            botIntnetItems.Add(new BotIntnetItem() { id = "0", message = "These are some of the things I can help you with", trigger = "MenuOption" });
            List<Option> optionList = new List<Option>();
            optionList.Add(new Option() { value = "requestId", label = "Search Ticket", trigger = "RequestSearchInit" });
            optionList.Add(new Option() { value = "HCMRequest", label = "New Ticket", trigger = "HCMRequest" });
            optionList.Add(new Option() { value = "/HCM/RequestSummary", label = "Ticket Summary", trigger = "IntentSwitchTigger" });
            if (michaeUserAccess.IsAssignee == 1)
            {
                optionList.Add(new Option() { value = "/HCM/AssigneeRequestSummary", label = "Admin Approval", trigger = "IntentSwitchTigger" });
            }

            if (michaeUserAccess.AcessType == "Administrator")
            {
                optionList.Add(new Option() { value = "/HCM/ManageAdminUser", label = "Admin Users", trigger = "IntentSwitchTigger" });
                optionList.Add(new Option() { value = "/HCM/ManageDepartment", label = "Manage Department", trigger = "IntentSwitchTigger" });
            }
            
            botIntnetItems.Add(new BotIntnetItem() { id = "MenuOption", options = optionList });
            List<Option> hcmRequestList = new List<Option>();
            foreach (var item in michaelDepartmentRequestMasters)
            {
                hcmRequestList.Add(new Option() { value = ""+ item.DepartmentId+"/"+item.Id, label = item.RequestTypeName, trigger = "HCMRequestHandilar" });
            }
            botIntnetItems.Add(new BotIntnetItem() { id = "HCMRequest", options = hcmRequestList });
            botIntnetItems.Add(new BotIntnetItem() { id = "HCMRequestHandilar", component = "HCMRequestComponent", waitAction=true, trigger= "MenuOption" });
            botIntnetItems.Add(new BotIntnetItem() { id = "RequestSearchInit", message = "Enter Request id", trigger = "RequestSearchInput" });
            botIntnetItems.Add(new BotIntnetItem() { id = "RequestSearchInput", user = true, trigger = "RequestSearchTigger" });
            botIntnetItems.Add(new BotIntnetItem() { id = "RequestSearchTigger", component = "RequestSearchPrevBotComponent", waitAction = true, trigger = "MenuOption" });
            botIntnetItems.Add(new BotIntnetItem() { id = "IntentSwitchTigger", component = "IntentSwitchComponent", waitAction = true, trigger = "MenuOption" });

            return botIntnetItems;
        }

        public static string FirstCharToUpper(string s)
        {
            try
            {
                // Check for empty string.  
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                // Return char and concat substring.  
                return char.ToUpper(s[0]) + s.Substring(1);
            }
            catch(Exception e1)
            {
                return s;
            }
            
        }

        public List<EscalationMapper> getUiMetaEscaltionList(ICollection<MichaelRequestEscalationMapper> michaelRequestEscalationMapper, List<DAL.NueUserProfile> nueUserProfilesMaster)
        {
            List<EscalationMapper> escalationMappers = new List<EscalationMapper>();

            foreach (var item in michaelRequestEscalationMapper)
            {
                EscalationMapper escalationMapper = new EscalationMapper();
                escalationMapper.Level = (int) item.Level;
                escalationMapper.Active = (int)item.Active;
                escalationMapper.MaxSla = (int)item.MaxSla;
                List<UiDropdownItem> Assignees = new List<UiDropdownItem>();

                var slaUsers = item.MichaelRequestEscalationUserBaseMapper;
                foreach (var itemU in slaUsers)
                {
                    if(itemU.Active == 1)
                    {
                        Assignees.Add(new UiDropdownItem(nueUserProfilesMaster.Where(x => x.Id == itemU.UserId && x.Active == 1).SingleOrDefault().Email, itemU.UserId.ToString()));
                    }
                }
                escalationMapper.Assignees = Assignees;
                escalationMappers.Add(escalationMapper);
            }

            return escalationMappers;
        }

        public string getUniqRequestId(string contentRootPath)
        {
            string contentRootPath1 = contentRootPath;
            var path1 = contentRootPath1 + "\\MyStaticFiles\\request-number-tracker.db";
            var data = System.IO.File.ReadAllText(path1);
            string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
            System.IO.File.WriteAllText(path1, newRequestId);
            return newRequestId;
        }

        public void setUniqRequestId(string contentRootPath, string requestId)
        {
            string contentRootPath1 = contentRootPath;
            var path1 = contentRootPath1 + "\\MyStaticFiles\\request-number-tracker.db";
            System.IO.File.WriteAllText(path1, requestId);
        }

        public static Random getRandom()
        {
            return new Random();
        }

        
    }
}
