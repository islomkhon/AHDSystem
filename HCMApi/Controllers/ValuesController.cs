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
                string userEmail = User.Identity.Name.ToLower();

                //List<UserProfile> userProfiles = new DataAccess(this.AzureAdSettings).getAllUserProfileExcept(userEmail);
                List<DAL.NueUserProfile> nueUserProfilesMaster = new DataAccess(this.AzureAdSettings).getAllUserProfilesDinamic();

                List<DAL.NueUserProfile> nueUserProfiles = nueUserProfilesMaster.Where(x => x.Email != userEmail).ToList<DAL.NueUserProfile>();

                ListUserId listUserId = new Modal.Utils().generateUserDropdownList(nueUserProfiles);
                ListUserRender listUserRender = new ListUserRender();

                listUserRender.UserId = JsonConvert.SerializeObject(listUserId.userIds);
                listUserRender.UserMail = JsonConvert.SerializeObject(listUserId.emails);

                //string webRootPath = _hostingEnvironment.WebRootPath;
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                //var path = Path.Combine(contentRootPath, @"\MyStaticFiles\hcmtemplate.json");
                var path = contentRootPath + "\\MyStaticFiles\\hcmtemplate.json";
                var requestTemplate = System.IO.File.ReadAllText(path);

                //String str = String.Format(requestTemplate, userIdStr, userEmailStr);

                string str = requestTemplate;
                str = str.Replace("@UserIdList", listUserRender.UserId);
                str = str.Replace("@UserMailList", listUserRender.UserMail);

                return new JsonResult(new JsonResponse("Ok", "Data Loaded.", str));

            }
            catch (Exception e1)
            {
                return new JsonResult(new JsonResponse("Failed", "An error occerd"));
            }
            
        }

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
        }

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
                        DAL.MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster = new DataAccess(this.AzureAdSettings).GetDepartmentRequestDetails(departmentId, requestId);
                        if (michaelDepartmentRequestTypeMaster != null)
                        {
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
        }




        [HttpPost]
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
        }

        [HttpPost]
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
