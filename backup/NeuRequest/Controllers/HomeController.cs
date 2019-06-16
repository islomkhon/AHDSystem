﻿using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using NeuRequest.DB;

namespace NeuRequest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
        private string graphResourceID = "https://graph.windows.net";

        public async Task<ActionResult> Index()
        {
            Session["ErrorMessage"] = null;
            Session["ErrorCode"] = null;
            Session["ErrorType"] = null;
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }
            int authUser = -1;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            try
            {
                Uri servicePointUri = new Uri(graphResourceID);
                Uri serviceRoot = new Uri(servicePointUri, tenantID);
                ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                      async () => await GetTokenForApplication());
                
                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectID))
                    .ExecuteAsync();
                IUser user = result.CurrentPage.ToList().First();

                if (user != null)
                {
                    bool isAuthenticated = isAuth();
                    if (isAuthenticated == false)
                    {
                        UserProfile userProfile = new DataAccess().getUserProfile(user.Mail.ToLower());
                        //UserProfile userProfile = new DataAccess().getUserProfile("bhavani.kannan@neudesic.com");
                        //UserProfile userProfile = new DataAccess().getUserProfile("priya.ignatius@neudesic.com");
                        if (!Models.Utils.isValidUserObject(userProfile))
                        {
                            return RedirectToAction("Update", "UserProfile");
                        }
                        else if (userProfile.Active != 1)
                        {
                            Session["ErrorType"] = "InactiveUser";
                            Session["ErrorMessage"] = "Your account is disabled. <br/> Please contact HCM";
                            Session["ErrorCode"] = "500";
                            return RedirectToAction("OpError", "ErrorHandilar");
                        }
                        Session["UserProfileSession"] = userProfile;
                    }
                }
                else
                {
                    throw new Exception("Invalid opration");
                }
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getUserHcmActiveRequests((Session["UserProfileSession"] as UserProfile).Id, 5);
                List<UserRequestUiGridRender> userRequestApproverUiGridRenders = new DataAccess().getHcmActiveApproverRequests((Session["UserProfileSession"] as UserProfile).Id, 5);
                ViewData["userRequestApproverUiGridRenders"] = userRequestApproverUiGridRenders;
                ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;

                return View();

            }
            catch (AdalException)
            {
                // Return to error page.
                return View("Error");
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception e1)
            {
                return RedirectToAction("SignIn", "Account");
            }
        }
        
        public ActionResult GetMessages()
        {
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetAllUnreadMessages(currentUser.Id));
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

        public bool isAuth()
        {
            try
            {
                if (Session["UserProfileSession"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Session["UserProfileSession"] = null;
                return false;
            }
        }

    }
}