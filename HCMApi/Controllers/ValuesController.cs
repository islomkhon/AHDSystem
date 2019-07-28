using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using HCMApi.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Graph;
using System.IO;
using HCMApi.DB;
using Newtonsoft.Json;
using FormatWith;
using HCMApi.Shedules;
using Microsoft.AspNetCore.Http;

namespace HCMApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AzureAd AzureAdSettings { get; set; }

        private readonly IHostingEnvironment _hostingEnvironment;

        public ValuesController(IOptions<AzureAd> settings, IHostingEnvironment hostingEnvironment)
        {
            AzureAdSettings = settings.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                
            }
            else
            {

            }
            return new string[] { "value1", "value2" };
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [Route("WeatherForecasts")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }


        [HttpGet]
        [Route("HCMRequestTemplate")]
        public JsonResult HCMRequestTemplate(string templateType)
        {

            if (!User.Identity.IsAuthenticated)
            {

            }
            try
            {
                var item = templateType.Split('/');
                string userEmail = User.Identity.Name.ToLower();

                int departmentId = int.Parse(item[0]);
                int requestId = int.Parse(item[1]);

                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                    if (michaelDepartmentRequestMaster != null)
                    {

                        michaelDepartmentRequestMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestMaster.DepartmentId);
                        DepartmentRequestTemplateRender departmentRequestTemplateRender = new DepartmentRequestTemplateRender();
                        departmentRequestTemplateRender.DepartmentId = (int)michaelDepartmentRequestMaster.DepartmentId;
                        departmentRequestTemplateRender.RequestId = michaelDepartmentRequestMaster.Id;

                        if (michaelDepartmentRequestMaster.Department.Active == 1 && michaelDepartmentRequestMaster.Active == 1)
                        {
                            string contentRootPath1 = _hostingEnvironment.ContentRootPath;
                            //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                            var dirPath1 = contentRootPath1 + "\\MyStaticFiles\\" + departmentRequestTemplateRender.DepartmentId + "\\" + departmentRequestTemplateRender.RequestId;
                            var path1 = dirPath1 + "\\template.json";
                            var requestTemplate1 = System.IO.File.ReadAllText(path1);
                            departmentRequestTemplateRender.AvilableField = JsonConvert.DeserializeObject<List<object>>(requestTemplate1);
                            departmentRequestTemplateRender.michaelDepartmentRequestMaster = michaelDepartmentRequestMaster;

                            return new JsonResult(new JsonResponse("Ok", "Data Loaded.", departmentRequestTemplateRender));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request."));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                    }

                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e1)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }

        }

        /*[HttpGet]
        [Route("HCMRequestTemplate")]
        public JsonResult HCMRequestTemplate(string templateType)
        {

            if (!User.Identity.IsAuthenticated)
            {

            }
            try
            {
                var item = templateType.Split('/');
                string userEmail = User.Identity.Name.ToLower();

                int departmentId = int.Parse(item[0]);
                int requestId = int.Parse(item[1]);

                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    DAL.MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                    if (michaelDepartmentRequestTypeMaster != null)
                    {
                        
                        michaelDepartmentRequestTypeMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestTypeMaster.DepartmentId);
                        DepartmentRequestTemplateRender departmentRequestTemplateRender = new DepartmentRequestTemplateRender();
                        departmentRequestTemplateRender.DepartmentId = (int) michaelDepartmentRequestTypeMaster.DepartmentId;
                        departmentRequestTemplateRender.RequestId = michaelDepartmentRequestTypeMaster.Id;

                        if (michaelDepartmentRequestTypeMaster.Department.Active == 1 && michaelDepartmentRequestTypeMaster.Active == 1)
                        {
                            string contentRootPath1 = _hostingEnvironment.ContentRootPath;
                            //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                            var dirPath1 = contentRootPath1 + "\\MyStaticFiles\\" + departmentRequestTemplateRender.DepartmentId + "\\" + departmentRequestTemplateRender.RequestId;
                            var path1 = dirPath1 + "\\template.json";
                            var requestTemplate1 = System.IO.File.ReadAllText(path1);
                            departmentRequestTemplateRender.AvilableField = JsonConvert.DeserializeObject<List<object>>(requestTemplate1);
                            departmentRequestTemplateRender.michaelDepartmentRequestTypeMaster = michaelDepartmentRequestTypeMaster;

                            return new JsonResult(new JsonResponse("Ok", "Data Loaded.", departmentRequestTemplateRender));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request."));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                    }

                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                ////List<UserProfile> userProfiles = new DataAccess(this.AzureAdSettings).getAllUserProfileExcept(userEmail);
                //List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(this.AzureAdSettings).getAllUserProfilesDinamic();

                //List<DAL.NueUserProfile> nueUserProfiles = nueUserProfilesMaster.Where(x => x.Email != userEmail).ToList<DAL.NueUserProfile>();

                //ListUserId listUserId = new Modal.Utils().generateUserDropdownList(nueUserProfiles);
                //ListUserRender listUserRender = new ListUserRender();

                //listUserRender.UserId = JsonConvert.SerializeObject(listUserId.userIds);
                //listUserRender.UserMail = JsonConvert.SerializeObject(listUserId.emails);

               

                //DepartmentRequestTemplate departmentRequestTemplate = new DepartmentRequestTemplate();
                //departmentRequestTemplate.DepartmentId = int.Parse(item[0]);
                //departmentRequestTemplate.RequestId = int.Parse(item[1]);

                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                ////var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                //var dirPath = contentRootPath + "\\MyStaticFiles\\" + departmentRequestTemplate.DepartmentId + "\\" + departmentRequestTemplate.RequestId;
                //var path = dirPath + "\\template.json";
                //var requestTemplate = System.IO.File.ReadAllText(path);
                //departmentRequestTemplate.AvilableField = JsonConvert.DeserializeObject<List<object>>(requestTemplate);


                ////string webRootPath = _hostingEnvironment.WebRootPath;
                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                ////var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                //var path = contentRootPath + "\\MyStaticFiles\\hcmtemplate.json";
                //var requestTemplate = System.IO.File.ReadAllText(path);

                ////String str = String.Format(requestTemplate, userIdStr, userEmailStr);



                //string str = requestTemplate;
                //str = str.Replace("@UserIdList", listUserRender.UserId);
                //str = str.Replace("@UserMailList", listUserRender.UserMail);

                //return new JsonResult(new JsonResponse("Ok", "Data Loaded.", departmentRequestTemplate));

            }
            catch (Exception e1)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            
        }*/

        [HttpGet]
        [Route("GetDepartments")]
        public JsonResult GetDepartments()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        return new JsonResult(new JsonResponse("Ok", "Data Loaded.", new DataAccess(this.AzureAdSettings).getDepartments()));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetDepartmentDetails")]
        public JsonResult GetDepartmentDetails(int departmentId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentMaster michaelDepartmentMaster = new DataAccess(this.AzureAdSettings).GetDepartmentDetails(departmentId);
                        if(michaelDepartmentMaster != null)
                        {
                            return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaelDepartmentMaster));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }
                        
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetDepartmentRequestMetaDetails")]
        public JsonResult GetDepartmentRequestMetaDetails(int departmentId, int requestTypeId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestMetaDetails(departmentId, requestTypeId);
                        if (michaelDepartmentRequestMaster != null)
                        {
                            List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(this.AzureAdSettings).getAllUserProfilesDinamic();
                            MichaelDepartmentRequest michaelDepartmentRequest = new MichaelDepartmentRequest();
                            michaelDepartmentRequest.Id = michaelDepartmentRequestMaster.Id;
                            michaelDepartmentRequest.RequestPriorityId = (int) michaelDepartmentRequestMaster.RequestPriorityId;
                            michaelDepartmentRequest.Active = (int) michaelDepartmentRequestMaster.Active;
                            michaelDepartmentRequest.EscalationMapper = new Modal.Utils().getUiMetaEscaltionList(michaelDepartmentRequestMaster.MichaelRequestEscalationMapper, nueUserProfilesMaster);
                            michaelDepartmentRequest.DepartmentId = michaelDepartmentRequestMaster.DepartmentId.ToString();
                            michaelDepartmentRequest.RequestTypeName = michaelDepartmentRequestMaster.RequestTypeName;
                            michaelDepartmentRequest.RequestTypeDescription = michaelDepartmentRequestMaster.RequestTypeDescription;
                            michaelDepartmentRequest.UserId = (int) michaelDepartmentRequestMaster.UserId;
                            michaelDepartmentRequest.AddedOn = michaelDepartmentRequestMaster.AddedOn;
                            michaelDepartmentRequest.ModifiedOn = michaelDepartmentRequestMaster.ModifiedOn;
                            return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaelDepartmentRequest));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }

                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("DepartmentStatusToggle")]
        public JsonResult DepartmentStatusToggle(int departmentId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List <DAL.MichaelDepartmentMaster> departmentList = new DataAccess(this.AzureAdSettings).DepartmentStatusToggle(departmentId);
                        if(departmentList != null)
                        {
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", departmentList));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "AN error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }
        
        [HttpGet]
        [Route("GetSearchResult")]
        public JsonResult GetSearchResult(string q)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {

                        List<MichaeSearchResultItem> michaeSearchResultItems = new DataAccess(this.AzureAdSettings).getUserSearchResultForId(q, nueUserProfile.Id);
                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaeSearchResultItems));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetUserNotifications")]
        public JsonResult GetUserNotifications()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {

                        List<DAL.NeuMessages> neuMessages =  new DataAccess(this.AzureAdSettings).getUserNotifications(nueUserProfile.Id);
                        var messages = from m in neuMessages orderby m.MessageId descending select m;
                        List<MichaeNotificationItem> michaeNotificationItem = new List<MichaeNotificationItem>();
                        foreach (var item in messages)
                        {
                            michaeNotificationItem.Add(new MichaeNotificationItem() { MessageId = item.MessageId,
                                                                                     UserId = (int)item.UserId,
                                                                                     Target = item.Target, Processed = (int)item.Processed,
                                                                                     Message = item.Message, EmptyMessage = item.EmptyMessage, DateAdded = item.Date.ToString()  });
                        }
                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaeNotificationItem));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public JsonResult GetUserProfile()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        try
                        {
                            Hubs.MessagesHub.SendMessagesAsync();
                        }
                        catch (Exception)
                        {

                        }
                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", nueUserProfile));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }
        
        [HttpGet]
        [Route("GetCurrentUserAccess")]
        public JsonResult GetCurrentUserAccess()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<UiDropdownItem> adminUserList = new DataAccess(this.AzureAdSettings).GetAdminUserList();
                        string AcessType = "User";
                        try
                        {
                            var isAdmin = adminUserList.Where(x => int.Parse(x.value) == nueUserProfile.Id);
                            if (isAdmin != null & isAdmin.FirstOrDefault() != null && isAdmin.Count() > 0)
                            {
                                AcessType = "Administrator";
                            }
                        }
                        catch (Exception e1)
                        {
                        }
                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", AcessType));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetAdminUserList")]
        public JsonResult GetAdminUserList()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<UiDropdownItem> adminUserList = new DataAccess(this.AzureAdSettings).GetAdminUserList();

                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", adminUserList));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        [HttpGet]
        [Route("GetMichaeApproverChangeModelPayload")]
        public JsonResult GetMichaeApproverChangeModelPayload(string requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                        michaelRequestViewerData.RequestId = requestId;
                        michaelRequestViewerData.UserId = nueUserProfile.Id;

                        JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                        if (requestData.status == "Ok")
                        {
                            michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                            if (michaelRequestViewerData.IsPermitted == 1 || michaelRequestViewerData.IsAdmin == 1)
                            {

                                List<MichaeRequestAcessItem> accessUsers = new DataAccess(this.AzureAdSettings).getRequestAllAccessUsers(michaelRequestViewerData.Id);
                                if(accessUsers != null && accessUsers.Count > 0)
                                {
                                    var accessList = accessUsers.Where(x => x.AcessType == "Approver");
                                    if(accessList != null && accessList.FirstOrDefault() != null && accessList.Count() > 0)
                                    {
                                        var accessUser = accessList.ToList();
                                        var userList = new DataAccess(this.AzureAdSettings).getAllUserProfilesActiveDinamic();
                                        List<UiDropdownItem> accessDropdownItems = new List<UiDropdownItem>();
                                        List<UiDropdownItem> usersDropdownItems = new List<UiDropdownItem>();
                                        for (int i = 0; i < accessUser.Count(); i++)
                                        {
                                            accessDropdownItems.Add(new UiDropdownItem(userList.FirstOrDefault(x=> x.Id == accessUser[i].UserId).Email, accessUser[i].UserId.ToString()));
                                        }

                                        for (int i = 0; i < userList.Count; i++)
                                        {
                                            usersDropdownItems.Add(new UiDropdownItem(userList[i].Email, userList[i].Id.ToString()));
                                        }
                                        
                                        return new JsonResult(new JsonResponse("Ok", "Data loaded.", new MichaeApproverChangeModelPayload() { accessDropdownItems = accessDropdownItems, usersDropdownItems = usersDropdownItems }));
                                    }
                                    else
                                    {
                                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                                    }
                                }
                                else
                                {
                                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                                }
                            }
                            else
                            {
                                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                        //List<UiDropdownItem> adminUserList = new DataAccess(this.AzureAdSettings).GetAdminUserList();
                        //return new JsonResult(new JsonResponse("Ok", "Data loaded.", adminUserList));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }




        [HttpGet]
        [Route("GetRequestAssigneeHistorySummary")]
        public JsonResult GetRequestAssigneeHistorySummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetRequestAssigneeHistorySummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetAssigneeNewRequestHistorySummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetRequestAssigneeSummary")]
        public JsonResult GetRequestAssigneeSummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetRequestAssigneeSummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetAssigneeNewRequestSummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetApproverRequestHistorySummary")]
        public JsonResult GetApproverRequestHistorySummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetApproverRequestHistorySummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetApproverNewRequestHistorySummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        [HttpGet]
        [Route("GetApproverRequestSummary")]
        public JsonResult GetApproverRequestSummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetApproverRequestSummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetApproverNewRequestHistorySummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetSelfNewRequestHistorySummary")]
        public JsonResult GetSelfNewRequestHistorySummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetSelfNewRequestHistorySummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {

                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetApproverNewRequestSummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetSelfNewRequestSummary")]
        public JsonResult GetSelfNewRequestSummary()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        List<MichaeRequestSummaryItem> michaeRequestSummaryItems = new DataAccess(this.AzureAdSettings).GetSelfNewRequestSummary(nueUserProfile.Id);
                        if (michaeRequestSummaryItems != null)
                        {
                            
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().GetSelfNewRequestSummaryUiTableRender(michaeRequestSummaryItems);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("DepartmentRequestListUiTableRender")]
        public JsonResult DepartmentRequestListUiTableRender(int departmentId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        //new MessagesRepository().GetAllUnreadMessages(nueUserProfile.Id);
                        DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new DAL.MichaelDepartmentRequestMaster();
                        michaelDepartmentRequestMaster.UserId = nueUserProfile.Id;
                        michaelDepartmentRequestMaster.DepartmentId = departmentId;
                        List<DAL.MichaelDepartmentRequestMaster> michaelDepartmentRequestMasters = new DataAccess(this.AzureAdSettings).GetDepartmentRequestList(michaelDepartmentRequestMaster);
                        if (michaelDepartmentRequestMasters != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().DepartmentRequestListUiTableRender(michaelDepartmentRequestMasters);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "An error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        /*[HttpGet]
        [Route("DepartmentRequestListUiTableRender")]
        public JsonResult DepartmentRequestListUiTableRender(int departmentId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        Department department = new Department();
                        department.UserId = nueUserProfile.Id;
                        department.Id = departmentId;
                        List<DAL.MichaelDepartmentRequestTypeMaster> departmentRequestList = new DataAccess(this.AzureAdSettings).GetDepartmentRequestList(department);
                        if (departmentRequestList != null)
                        {
                            UiMaterialTableModel uiMaterialTableModel = new Modal.Utils().DepartmentRequestListUiTableRender(departmentRequestList);
                            return new JsonResult(new JsonResponse("Ok", "Data updated.", JsonConvert.SerializeObject(uiMaterialTableModel)));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "AN error occerd while updating the data"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }*/

        /*[HttpGet]
        [Route("GetDepartmentRequestDetails")]
        public JsonResult GetDepartmentRequestDetails(int departmentId, int requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                        if (michaelDepartmentRequestTypeMaster != null)
                        {
                            michaelDepartmentRequestTypeMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestTypeMaster.DepartmentId);
                            return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaelDepartmentRequestTypeMaster));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }

                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }*/

        [HttpGet]
        [Route("GetDepartmentRequestDetails")]
        public JsonResult GetDepartmentRequestDetails(int departmentId, int requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                        if (michaelDepartmentRequestMaster != null)
                        {
                            michaelDepartmentRequestMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestMaster.DepartmentId);
                            return new JsonResult(new JsonResponse("Ok", "Data loaded.", michaelDepartmentRequestMaster));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }

                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetDepartmentRequestTemplateRaw")]
        public JsonResult GetDepartmentRequestTemplateRaw(int departmentId, int requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                        if (michaelDepartmentRequestMaster != null)
                        {
                            michaelDepartmentRequestMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestMaster.DepartmentId);
                            if (michaelDepartmentRequestMaster != null && michaelDepartmentRequestMaster.Active == 1 && michaelDepartmentRequestMaster.Department.Active == 1)
                            {
                                string contentRootPath = _hostingEnvironment.ContentRootPath;
                                //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                                var dirPath = contentRootPath + "\\MyStaticFiles\\" + michaelDepartmentRequestMaster.DepartmentId + "\\" + michaelDepartmentRequestMaster.Id;
                                var path = dirPath + "\\template.json";
                                var requestTemplate = System.IO.File.ReadAllText(path);

                                DepartmentRequestTemplate departmentRequestTemplate = new DepartmentRequestTemplate();
                                departmentRequestTemplate.DepartmentId = departmentId;
                                departmentRequestTemplate.RequestId = requestId;
                                departmentRequestTemplate.AvilableField = JsonConvert.DeserializeObject<List<object>>(requestTemplate);

                                return new JsonResult(new JsonResponse("Ok", "Data loaded.", departmentRequestTemplate));
                            }
                            else
                            {
                                return new JsonResult(new JsonResponse("Failed", "Request type is not active"));
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }

                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        [HttpGet]
        [Route("GetMichaelRequestViewerData")]
        public JsonResult GetMichaelRequestViewerData(string requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                        michaelRequestViewerData.RequestId = requestId;
                        michaelRequestViewerData.UserId = nueUserProfile.Id;
                        return new JsonResult(new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetMichaelRequestBotPrev")]
        public JsonResult GetMichaelRequestBotPrev(string requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                        michaelRequestViewerData.RequestId = requestId;
                        michaelRequestViewerData.UserId = nueUserProfile.Id;
                        var requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestBotPrevData(michaelRequestViewerData);
                        MichaelRequestBotPrevData michaelRequestBotPrevData = new MichaelRequestBotPrevData();
                        if (requestData.status == "Ok")
                        {
                            michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                            if (michaelRequestViewerData.IsPermitted == 1)
                            {
                                michaelRequestBotPrevData.IsPermitted = 1;
                                michaelRequestBotPrevData.michaelRequestViewerData = michaelRequestViewerData;
                                michaelRequestBotPrevData.ResponseMode = "Single";
                            }
                            else
                            {
                                michaelRequestBotPrevData.IsPermitted = 0;
                                michaelRequestBotPrevData.michaelRequestViewerData = null;
                                michaelRequestBotPrevData.ResponseMode = "Single";
                            }
                        }
                        else
                        {
                            michaelRequestBotPrevData.IsPermitted = 0;
                            michaelRequestBotPrevData.michaelRequestViewerData = null;
                            michaelRequestBotPrevData.ResponseMode = "SearchRedirect";
                        }
                        return new JsonResult(new JsonResponse("Ok", "Data loaded successfully.", michaelRequestBotPrevData));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetMichaelRequestViewerLogData")]
        public JsonResult GetMichaelRequestViewerLogData(string requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                        michaelRequestViewerData.RequestId = requestId;
                        michaelRequestViewerData.UserId = nueUserProfile.Id;

                        JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                        if (requestData.status == "Ok")
                        {
                            michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                            if (michaelRequestViewerData.IsPermitted == 1)
                            {
                                JsonResponse requestLogResponse = new DataAccess(this.AzureAdSettings).getMichaelRequestLogs(michaelRequestViewerData);
                                return new JsonResult(requestLogResponse);
                            }
                            else
                            {
                                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpGet]
        [Route("GetMichaelBotSteps")]
        public JsonResult GetMichaelBotWelcomeSteps()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        var activeDepartments = new DataAccess(this.AzureAdSettings).GetDepartmentActiveRequestTypes();
                        if(activeDepartments != null)
                        {
                            return new JsonResult(new JsonResponse("Ok", "Data loaded successfully.", new Modal.Utils().getBotIntents(activeDepartments, new DataAccess(this.AzureAdSettings).getMichaeUserAccess(nueUserProfile.Id))));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }
        
        [HttpGet]
        [Route("GetMichaelRequestResourceFile")]
        public async Task<IActionResult> GetMichaelRequestResourceFile(string resId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return null;
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {

                        DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                        michaelRequestLog.UserId = nueUserProfile.Id;
                        michaelRequestLog.Id = int.Parse(resId);
                        var response = new DataAccess(this.AzureAdSettings).getMichaelRequestAttachmentItem(michaelRequestLog);
                        if (response != null && response.status == "Ok")
                        {
                            DAL.MichaelRequestAttachmentLog michaelRequestAttachmentLog = (DAL.MichaelRequestAttachmentLog)response.payload;
                            var VFileName = michaelRequestAttachmentLog.VfileName;
                            var FileName = michaelRequestAttachmentLog.FileName;
                            var FileExtension = michaelRequestAttachmentLog.FileExt;
                            var requestId = michaelRequestAttachmentLog.RequestId;
                            string contentRootPath = _hostingEnvironment.ContentRootPath;
                            var dirPath = contentRootPath + "\\Uploads\\" + requestId;
                            var path = dirPath + "\\" + VFileName;

                            //var memory = new MemoryStream();
                            //using (var stream = new FileStream(path, FileMode.Open))
                            //{
                            //    await stream.CopyToAsync(memory);
                            //}
                            //memory.Position = 0;
                            //return File(memory, GetContentType(path), Path.GetFileName(path));

                            //Stream stream = await { { __get_stream_based_on_id_here__} }
                            //FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

                            //if (fileStream == null)
                            //    return NotFound(); // returns a NotFoundResult with Status404NotFound response.

                            //return File(fileStream, "application/octet-stream");

                            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                            string fileName = FileName + FileExtension;
                            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                            //Stream stream = await { { __get_stream_based_on_id_here__} }

                            //if (stream == null)
                            //    return NotFound();

                            //return File(stream, "application/octet-stream"); // returns a FileStreamResult

                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        /*[HttpGet]
        [Route("GetDepartmentRequestTemplateRaw")]
        public JsonResult GetDepartmentRequestTemplateRaw(int departmentId, int requestId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);
                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        DAL.MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                        if (michaelDepartmentRequestTypeMaster != null)
                        {
                            michaelDepartmentRequestTypeMaster.Department = new DataAccess(this.AzureAdSettings).GetDepartmentDetails((int)michaelDepartmentRequestTypeMaster.DepartmentId);
                            if(michaelDepartmentRequestTypeMaster != null && michaelDepartmentRequestTypeMaster.Active == 1 && michaelDepartmentRequestTypeMaster.Department.Active == 1)
                            {
                                string contentRootPath = _hostingEnvironment.ContentRootPath;
                                //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                                var dirPath = contentRootPath + "\\MyStaticFiles\\" + michaelDepartmentRequestTypeMaster.DepartmentId + "\\" + michaelDepartmentRequestTypeMaster.Id;
                                var path = dirPath + "\\template.json";
                                var requestTemplate = System.IO.File.ReadAllText(path);

                                DepartmentRequestTemplate departmentRequestTemplate = new DepartmentRequestTemplate();
                                departmentRequestTemplate.DepartmentId = departmentId;
                                departmentRequestTemplate.RequestId = requestId;
                                departmentRequestTemplate.AvilableField = JsonConvert.DeserializeObject<List<object>>(requestTemplate);

                                return new JsonResult(new JsonResponse("Ok", "Data loaded.", departmentRequestTemplate));
                            }
                            else
                            {
                                return new JsonResult(new JsonResponse("Failed", "Request type is not active"));
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request. Unable to locate requested information"));
                        }

                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }*/

        [HttpGet]
        [Route("TestSync")]
        public JsonResult TestSync()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    string userEmail = User.Identity.Name.ToLower();

                    SyncUsersAd.SyncUsers(this.AzureAdSettings);

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }






        
        [HttpPost]
        [Route("ChangeApproverMichaelRequest")]
        public JsonResult ChangeApproverMichaelRequest([FromBody] MichaeApproverChangeRequestPayload michaeApproverChangeRequestPayload)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {

                    MichaeApproverChangeRequest michaeApproverChangeRequest = new MichaeApproverChangeRequest();
                    michaeApproverChangeRequest.FromUser = int.Parse(michaeApproverChangeRequestPayload.FromUser.value);
                    michaeApproverChangeRequest.ToUser = int.Parse(michaeApproverChangeRequestPayload.ToUser.value);
                    michaeApproverChangeRequest.requestId = michaeApproverChangeRequestPayload.requestId;

                    michaeApproverChangeRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaeApproverChangeRequest.requestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 || michaelRequestViewerData.IsAdmin == 1)
                        {
                            List<MichaeRequestAcessItem> accessUsers = new DataAccess(this.AzureAdSettings).getRequestAllAccessUsers(michaelRequestViewerData.Id);
                            if (accessUsers != null && accessUsers.Count > 0)
                            {
                                var assigneeList = accessUsers.Where(x => x.AcessType == "Assignee");
                                if (assigneeList != null && assigneeList.FirstOrDefault() != null && assigneeList.Count() > 0)
                                {
                                    var isAssinee =  assigneeList.Where(x=>x.UserId == michaeApproverChangeRequest.UserId);
                                    if((isAssinee != null && isAssinee.FirstOrDefault() != null) || (michaelRequestViewerData.IsAdmin == 1))
                                    {
                                        return new JsonResult(new DataAccess(this.AzureAdSettings).ChangeApproverMichaelRequest(michaeApproverChangeRequest, michaelRequestViewerData, AzureAdSettings, _hostingEnvironment));
                                    }
                                    else
                                    {
                                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                                    }
                                }
                                else
                                {
                                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                                }
                            }
                            else
                            {
                                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                     //michaeNotificationUpdateRequest.UserId = nueUserProfile.Id;
                     //return new JsonResult(new DataAccess(this.AzureAdSettings).UpdateMichaelNotificationStatusSeen(michaeNotificationUpdateRequest));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        [HttpPost]
        [Route("UpdateMichaelNotificationStatusSeen")]
        public JsonResult UpdateMichaelNotificationStatusSeen([FromBody] MichaeNotificationUpdateRequest michaeNotificationUpdateRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaeNotificationUpdateRequest.UserId = nueUserProfile.Id;
                    return new JsonResult(new DataAccess(this.AzureAdSettings).UpdateMichaelNotificationStatusSeen(michaeNotificationUpdateRequest));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("AddMichaelRequestFeedback")]
        public JsonResult AddMichaelRequestFeedback([FromBody] MichaelRequestFeedbackRequest michaelRequestFeedbackRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestFeedbackRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestFeedbackRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsOwner == 1 && michaelRequestViewerData.MichaelRequestUserOp.Close == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestFeedbackRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).addMichaelRequestFeedback(michaelRequestLog, michaelRequestViewerData, michaelRequestFeedbackRequest, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("AdminRejectMichaelRequest")]
        public JsonResult AdminRejectMichaelRequest([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsAssignee == 1 && michaelRequestViewerData.MichaelRequestUserOp.AdminApprove == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).adminRejectMichaelRequest(michaelRequestLog, michaelRequestViewerData, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("AdminApproveMichaelRequest")]
        public JsonResult AdminApproveMichaelRequest([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsAssignee == 1 && michaelRequestViewerData.MichaelRequestUserOp.AdminApprove == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).adminApproveMichaelRequest(michaelRequestLog, michaelRequestViewerData, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("RejectMichaelRequest")]
        public JsonResult RejectMichaelRequest([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsApprover == 1 && michaelRequestViewerData.MichaelRequestUserOp.Approve == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).rejectMichaelRequest(michaelRequestLog, michaelRequestViewerData, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("ApproveMichaelRequest")]
        public JsonResult ApproveMichaelRequest([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsApprover == 1 && michaelRequestViewerData.MichaelRequestUserOp.Approve == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).approveMichaelRequest(michaelRequestLog, michaelRequestViewerData, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("WithdrawMichaelRequest")]
        public JsonResult WithdrawMichaelRequest([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData = new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserAcces.IsOwner == 1 && michaelRequestViewerData.MichaelRequestUserOp.Withdraw == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            return new JsonResult(new DataAccess(this.AzureAdSettings).withdrawMichaelRequest(michaelRequestLog, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("AddMichaelRequestAttachment")]
        public JsonResult AddMichaelRequestAttachment(IFormCollection form)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1 && form.Files != null && form.Files.Count > 0)
                {
                    MichaelRequestAttachment michaelRequestAttachment = new MichaelRequestAttachment();
                    michaelRequestAttachment.RequestId = form["requestId"];
                    michaelRequestAttachment.file = form.Files[0];

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestAttachment.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData =  new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if(requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserOp.Attach == 1)
                        {
                            string FileName = Path.GetFileNameWithoutExtension(michaelRequestAttachment.file.FileName);
                            string FileExtension = Path.GetExtension(michaelRequestAttachment.file.FileName);
                            string VFileName = DateTime.UtcNow.ToString("yyyyMMddhhmmss") + "_" + FileExtension;

                            DAL.MichaelRequestAttachmentLog michaelRequestAttachmentLog = new DAL.MichaelRequestAttachmentLog();
                            michaelRequestAttachmentLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestAttachmentLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestAttachmentLog.FileName = FileName;
                            michaelRequestAttachmentLog.FileExt = FileExtension;
                            michaelRequestAttachmentLog.VfileName = VFileName;
                            var ops = new DataAccess(this.AzureAdSettings).addNewMichaelRequestAttchment(michaelRequestAttachmentLog, michaelRequestAttachment, AzureAdSettings, _hostingEnvironment);
                            if(ops.status == "Ok")
                            {
                                string contentRootPath = _hostingEnvironment.ContentRootPath;
                                var dirPath = contentRootPath + "\\Uploads\\" + michaelRequestViewerData.Id;
                                var path = dirPath + "\\"+ VFileName;
                                System.IO.Directory.CreateDirectory(dirPath);
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    michaelRequestAttachment.file.CopyTo(fileStream);
                                }
                                return new JsonResult(ops);
                            }
                            else
                            {
                                return new JsonResult(ops);
                            }
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                    //michaelRequestCommentRequest.UserId = nueUserProfile.Id;
                    //return new JsonResult(new DataAccess(this.AzureAdSettings).updateDepartmentRequestType(michaelDepartmentRequest));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("AddMichaelRequestComment")]
        public JsonResult AddMichaelRequestComment([FromBody] MichaelRequestCommentRequest michaelRequestCommentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    michaelRequestCommentRequest.UserId = nueUserProfile.Id;

                    MichaelRequestViewerData michaelRequestViewerData = new MichaelRequestViewerData();
                    michaelRequestViewerData.RequestId = michaelRequestCommentRequest.RequestId;
                    michaelRequestViewerData.UserId = nueUserProfile.Id;

                    JsonResponse requestData =  new DataAccess(this.AzureAdSettings).GetMichaelRequestViewerData(michaelRequestViewerData);
                    if (requestData.status == "Ok")
                    {
                        michaelRequestViewerData = (MichaelRequestViewerData)requestData.payload;
                        if (michaelRequestViewerData.IsPermitted == 1 && michaelRequestViewerData.MichaelRequestUserOp.Comment == 1)
                        {
                            DAL.MichaelRequestLog michaelRequestLog = new DAL.MichaelRequestLog();
                            michaelRequestLog.RequestId = michaelRequestViewerData.Id;
                            michaelRequestLog.UserId = michaelRequestViewerData.UserId;
                            michaelRequestLog.Payload = michaelRequestCommentRequest.RequestComment;
                            
                            return new JsonResult(new DataAccess(this.AzureAdSettings).addNewMichaelRequestComment(michaelRequestLog, AzureAdSettings, _hostingEnvironment));
                        }
                        else
                        {
                            return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                        }
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        [HttpPost]
        [Route("GetUsersDropdownList")]
        public JsonResult GetUsersDropdownList([FromBody] GetAPIDataTemplate getAPIDataTemplate)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(this.AzureAdSettings).getAllUserProfilesDinamic();
                List<DAL.NueUserProfile> nueUserProfiles = nueUserProfilesMaster.Where(x => x.Active == 1).ToList<DAL.NueUserProfile>();

                List<UiDropdownItem> uiDropdownItems = new List<UiDropdownItem>();
                for (int i = 0; i < nueUserProfiles.Count; i++)
                {
                    uiDropdownItems.Add(new UiDropdownItem(nueUserProfiles[i].Email, nueUserProfiles[i].Id.ToString()));
                }

                return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));
                
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("GetApiData")]
        public JsonResult GetApiData([FromBody] GetAPIDataTemplate getAPIDataTemplate)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                string userEmail = User.Identity.Name.ToLower();
                string requestedApi = getAPIDataTemplate.Name;
                if(requestedApi.Trim().ToLower() == "approverapi")
                {

                    List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(this.AzureAdSettings).getAllUserProfilesDinamic();
                    List<DAL.NueUserProfile> nueUserProfiles = nueUserProfilesMaster.Where(x => x.Email != userEmail && x.Active == 1).ToList<DAL.NueUserProfile>();

                    List<UiDropdownItem> uiDropdownItems = new List<UiDropdownItem>();
                    for (int i = 0; i < nueUserProfiles.Count; i++)
                    {
                        uiDropdownItems.Add(new UiDropdownItem(nueUserProfiles[i].Email, nueUserProfiles[i].Id.ToString()));
                    }

                    return new JsonResult(new JsonResponse("Ok", "Data updated", uiDropdownItems));


                    /*ListUserId listUserId = new Modal.Utils().generateUserDropdownList(nueUserProfiles);
                    ListUserRender listUserRender = new ListUserRender();

                    listUserRender.UserId = JsonConvert.SerializeObject(listUserId.userIds);
                    listUserRender.UserMail = JsonConvert.SerializeObject(listUserId.emails);*/
                    
                }

                //var dbField = departmentRequestSaveTemplate.DataFields;
                //var temp = JsonConvert.DeserializeObject<List<DepartmentRequestSaveFormItem>>(dbField.ToString());

                /*else if (departmentRequestTemplate != null && departmentRequestTemplate.DepartmentId != 0 && departmentRequestTemplate.RequestId != 0)
                {
                    var json = JsonConvert.SerializeObject(departmentRequestTemplate.AvilableField);

                    string contentRootPath = _hostingEnvironment.ContentRootPath;
                    //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                    var dirPath = contentRootPath + "\\MyStaticFiles\\" + departmentRequestTemplate.DepartmentId + "\\" + departmentRequestTemplate.RequestId;
                    var path = dirPath + "\\template.json";
                    System.IO.Directory.CreateDirectory(dirPath);
                    System.IO.File.WriteAllText(path, json);
                    return new JsonResult(new JsonResponse("Ok", "Data updated"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }*/

                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }
        
        [HttpPost]
        [Route("CreateNewRequest")]
        public JsonResult CreateNewRequest([FromBody] DepartmentRequestSaveTemplate departmentRequestSaveTemplate)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                string userEmail = User.Identity.Name.ToLower();
                DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                {
                    string contentRootPath = _hostingEnvironment.ContentRootPath;
                    return new JsonResult(new DataAccess(this.AzureAdSettings).addNewMichaelRequest(departmentRequestSaveTemplate, nueUserProfile, contentRootPath));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }

                //var temp = JsonConvert.DeserializeObject<List<DepartmentRequestSaveFormItem>>(dbField.ToString());

                /*else if (departmentRequestTemplate != null && departmentRequestTemplate.DepartmentId != 0 && departmentRequestTemplate.RequestId != 0)
                {
                    var json = JsonConvert.SerializeObject(departmentRequestTemplate.AvilableField);

                    string contentRootPath = _hostingEnvironment.ContentRootPath;
                    //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                    var dirPath = contentRootPath + "\\MyStaticFiles\\" + departmentRequestTemplate.DepartmentId + "\\" + departmentRequestTemplate.RequestId;
                    var path = dirPath + "\\template.json";
                    System.IO.Directory.CreateDirectory(dirPath);
                    System.IO.File.WriteAllText(path, json);
                    return new JsonResult(new JsonResponse("Ok", "Data updated"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }*/

                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("UpdateDepartmentRequestTemplateType")]
        public JsonResult UpdateDepartmentRequestTemplateType([FromBody] DepartmentRequestTemplate departmentRequestTemplate)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (departmentRequestTemplate != null && departmentRequestTemplate.DepartmentId != 0 && departmentRequestTemplate.RequestId != 0)
                {
                    var json = JsonConvert.SerializeObject(departmentRequestTemplate.AvilableField);

                    string contentRootPath = _hostingEnvironment.ContentRootPath;
                    //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                    var dirPath = contentRootPath + "\\MyStaticFiles\\" + departmentRequestTemplate.DepartmentId + "\\" + departmentRequestTemplate.RequestId;
                    var path = dirPath + "\\template.json";
                    System.IO.Directory.CreateDirectory(dirPath);
                    System.IO.File.WriteAllText(path, json);
                    return new JsonResult(new JsonResponse("Ok", "Data updated"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }


        /*[HttpPost]
        [Route("EditDepartmentRequestType")]
        public JsonResult EditDepartmentRequestType([FromBody] DepartmentRequest departmentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (departmentRequest != null && departmentRequest.DepartmentId != 0 && departmentRequest.RequestTypeName != null && departmentRequest.RequestTypeName.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        departmentRequest.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).editDepartmentRequestType(departmentRequest);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }*/

        /*[HttpPost]
        [Route("CreateDepartmentRequestType")]
        public JsonResult CreateDepartmentRequestType([FromBody] DepartmentRequest departmentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (departmentRequest != null && departmentRequest.DepartmentId != 0 && departmentRequest.RequestTypeName != null && departmentRequest.RequestTypeName.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        departmentRequest.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).addNewDepartmentRequestType(departmentRequest);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }*/

        [HttpPost]
        [Route("UpdateAdminUsers")]
        public JsonResult UpdateAdminUsers([FromBody] MichaeAdminUserRequest michaeAdminUserRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (michaeAdminUserRequest != null && michaeAdminUserRequest.AdminUserList != null && michaeAdminUserRequest.AdminUserList.Count > 0)
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        return new JsonResult(new DataAccess(this.AzureAdSettings).updateAdminUsers(michaeAdminUserRequest.AdminUserList, nueUserProfile.Id));
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                        
                    //string userEmail = User.Identity.Name.ToLower();
                    //DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    //if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    //{
                    //    michaelDepartmentRequest.UserId = nueUserProfile.Id;
                    //    JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).updateDepartmentRequestType(michaelDepartmentRequest);
                    //    return new JsonResult(dbStatus);
                    //}
                    //else
                    //{
                    //    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    //}

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("UpdateDepartmentRequestType")]
        public JsonResult UpdateDepartmentRequestType([FromBody] MichaelDepartmentRequest michaelDepartmentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (michaelDepartmentRequest != null && int.Parse(michaelDepartmentRequest.DepartmentId) != 0 && michaelDepartmentRequest.RequestTypeName != null && michaelDepartmentRequest.RequestTypeName.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        michaelDepartmentRequest.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).updateDepartmentRequestType(michaelDepartmentRequest);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("CreateDepartmentRequestType")]
        public JsonResult CreateDepartmentRequestType([FromBody] MichaelDepartmentRequest michaelDepartmentRequest)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (michaelDepartmentRequest != null && int.Parse(michaelDepartmentRequest.DepartmentId) != 0 && michaelDepartmentRequest.RequestTypeName != null && michaelDepartmentRequest.RequestTypeName.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        michaelDepartmentRequest.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).addNewDepartmentRequestType(michaelDepartmentRequest);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }

                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public JsonResult CreateDepartment([FromBody] Department department)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (department != null && department.Departmentname != null && department.Departmentname.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if(nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        department.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).addNewDepartment(department);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        [HttpPost]
        [Route("EditDepartment")]
        public JsonResult EditDepartment([FromBody] Department department)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
                else if (department != null && department.Departmentname != null && department.Departmentname.Trim() != "")
                {
                    string userEmail = User.Identity.Name.ToLower();
                    DAL.NueUserProfile nueUserProfile = new DataAccess(this.AzureAdSettings).getSpecificUserProfilesByEmail(userEmail);

                    if (nueUserProfile != null && nueUserProfile.Email != null && nueUserProfile.Email.ToLower() == userEmail && nueUserProfile.Active == 1)
                    {
                        department.UserId = nueUserProfile.Id;
                        JsonResponse dbStatus = new DataAccess(this.AzureAdSettings).updateDepartMentDetails(department);
                        return new JsonResult(dbStatus);
                    }
                    else
                    {
                        return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                    }
                }
                else
                {
                    return new JsonResult(new JsonResponse("Failed", "Invalid Request"));
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats  officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
        }
    }
}
