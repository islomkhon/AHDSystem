using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using NeuRequest.Models;
using NeuRequest.DB;

namespace NeuRequest.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
        private string graphResourceID = "https://graph.windows.net";


        public async Task<ActionResult> Update()
        {
            Session["ErrorMessage"] = null;
            Session["ErrorCode"] = null;
            Session["ErrorType"] = null;

            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            try
            {
                Uri servicePointUri = new Uri(graphResourceID);
                Uri serviceRoot = new Uri(servicePointUri, tenantID);
                ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                      async () => await GetTokenForApplication());

                // use the token for querying the graph to get the user details

                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectID))
                    .ExecuteAsync();
                IUser user = result.CurrentPage.ToList().First();

                UserProfile userProfile = new DataAccess().getUserProfile(user.Mail.ToLower());
                //UserProfile userProfile = new DataAccess().getUserProfile("bhavani.kannan@neudesic.com");
                //UserProfile userProfile = new DataAccess().getUserProfile("priya.ignatius@neudesic.com");

                if (!Models.Utils.isValidUserObject(userProfile))
                {
                    userProfile = new UserProfile();
                    userProfile.Email = user.Mail.ToLower();
                    userProfile.FullName = user.DisplayName;
                }
                else if (userProfile.Active != 1)
                {
                    Session["ErrorType"] = "InactiveUser";
                    Session["ErrorMessage"] = "Your account is disabled. <br/> Please contact HCM";
                    Session["ErrorCode"] = "500";
                    return RedirectToAction("OpError", "ErrorHandilar");
                }

                ViewData["UserProfile"] = userProfile;
                return View();
            }
            catch (AdalException)
            {
                // Return to error page.
                return View("Error");
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception)
            {
                return View("Relogin");
            }
        }

        [HttpPost]
        public ActionResult Update(FormCollection formCollection)
        {
            string NTPLID = formCollection["NTPLID"];
            string Email = formCollection["Email"];
            string FullName = formCollection["FullName"];
            string FirstName = formCollection["FirstName"];
            string MiddleName = formCollection["MiddleName"];
            string LastName = formCollection["LastName"];
            string DateofJoining = formCollection["DateofJoining"];
            string EmpStatusId = formCollection["EmpStatusId"];
            string PracticeId = formCollection["PracticeId"];
            string JLId = formCollection["JLId"];
            string DSId = formCollection["DSId"];
            string Location = formCollection["Location"];
            string Active = formCollection["Active"];
            string IsMailCommunication = formCollection["IsMailCommunication"];
            UserProfile userProfile = new UserProfile();
            try
            {
                userProfile.NTPLID = NTPLID;
                userProfile.Email = Email;
                userProfile.FullName = FullName;
                userProfile.FirstName = FirstName;
                userProfile.MiddleName = MiddleName;
                userProfile.LastName = LastName;
                userProfile.DateofJoining = DateofJoining;
                userProfile.EmpStatusId = int.Parse(EmpStatusId);
                userProfile.PracticeId = int.Parse(PracticeId);
                userProfile.JLId = int.Parse(JLId);
                userProfile.DSId = int.Parse(DSId);
                userProfile.Location = Location;
                userProfile.Active = int.Parse(Active);
                userProfile.userPreference = new UserPreference();
                userProfile.userPreference.IsMailCommunication = int.Parse(IsMailCommunication);
                if (userProfile.isValid())
                {
                    userProfile = new DataAccess().saveUserProfile(userProfile);
                    TempData["Message"] = "Data updated, relogin to take changes effect.";
                    ViewData["UserProfile"] = userProfile;
                    return View();
                }
                else
                {
                    throw new Exception("Invalid request");
                }
            }
            catch (Exception e)
            {
                TempData["Message"] = "Invalid request "+e.Message;
                ViewData["UserProfile"] = userProfile;
                return View();
            }
        }
        
        // GET: UserProfile
        public async Task<ActionResult> Index()
        {
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            try
            {
                Uri servicePointUri = new Uri(graphResourceID);
                Uri serviceRoot = new Uri(servicePointUri, tenantID);
                ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                      async () => await GetTokenForApplication());

                // use the token for querying the graph to get the user details

                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectID))
                    .ExecuteAsync();
                IUser user = result.CurrentPage.ToList().First();
                


                return View(user);
            }
            catch (AdalException)
            {
                // Return to error page.
                return View("Error");
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception)
            {
                return View("Relogin");
            }
        }

        public void RefreshSession()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties { RedirectUri = "/UserProfile" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }

        public async Task<string> GetTokenForApplication()
        {
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            ClientCredential clientcred = new ClientCredential(clientId, appKey);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            AuthenticationContext authenticationContext = new AuthenticationContext(aadInstance + tenantID, new ADALTokenCache(signedInUserID));
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenSilentAsync(graphResourceID, clientcred, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
            return authenticationResult.AccessToken;
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;
        }
    }
}
