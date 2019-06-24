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
    public class GoalDashboardController : Controller
    {
        public ActionResult Index()
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

            List<UserRequestUiGridRender> userRequestUiGridRenders = new DataAccess().getUserHcmActiveRequests(currentUser.Id);
            ViewData["userRequestUiGridRenders"] = userRequestUiGridRenders;
            ViewData["UserProfileSession"] = currentUser;

            return View();
        }

        public ActionResult CreateNeuTeamGoal()
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

            if(currentUser.JLDesc.ToLower().Trim() != "jl2" && currentUser.JLDesc.ToLower().Trim() != "jl3")
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access", errorCode = "404" });
            }

            List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
            ViewData["UserMasterList"] = userProfiles;
            TeamGoalUiRender teamGoal = new TeamGoalUiRender();
            teamGoal.Active = 1;
            ViewData["TeamGoalUiRender"] = teamGoal;
            ViewData["UserProfileSession"] = currentUser;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNeuTeamGoal(TeamGoalUiRender teamGoalUiRender)
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
            bool userAccess = false;

            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                userAccess = true;
            }

            if (currentUser.JLDesc.ToLower().Trim() != "jl2" && currentUser.JLDesc.ToLower().Trim() != "jl3" && userAccess != true)
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access", errorCode = "404" });
            }
            TempData["Message"] = null;
            var dateCreated = DateTime.UtcNow;
            try
            {
                string domainName = Request.Url.GetLeftPart(UriPartial.Authority);
                if (teamGoalUiRender.isValid())
                {

                    NuRequestActivityMaster nueGoalCatgoryType = new DataAccess().getGoalCatgoryType("Team");
                    NuRequestActivityMaster assigneeAccess = new DataAccess().getGoalAccessType("assignee");
                    NuRequestActivityMaster approverAccess = new DataAccess().getGoalAccessType("approver1");
                    NuRequestActivityMaster goalStatusM = new DataAccess().getGoalStatusType("assigned");
                    List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                    List<TeamGoalMapper> teamGoalMappers = new List<TeamGoalMapper>();

                    foreach (var item in teamGoalUiRender.Users)
                    {
                        if (nueUserProfiles.Where(x => x.Active == 1 && x.Id == int.Parse(item)).Count() > 0)
                        {
                            var user = nueUserProfiles.Where(x => x.Active == 1 && x.Id == int.Parse(item)).First();
                            TeamGoal teamGoal = new TeamGoal();
                            teamGoal.UserId = user.Id;
                            teamGoal.InitiOwner = currentUser.Id;
                            teamGoal.GoalTypeId = teamGoalUiRender.GoalCategory;
                            teamGoal.GoalCatType = nueGoalCatgoryType.Id;
                            teamGoal.GoalTitle = teamGoalUiRender.GoalTitle;
                            teamGoal.GoalDesc = teamGoalUiRender.GoalDesc;
                            teamGoal.StartDate = teamGoalUiRender.StartDate;
                            teamGoal.EndDate = teamGoalUiRender.EndDate;
                            teamGoal.Active = teamGoalUiRender.Active;
                            teamGoal.AddedOn = dateCreated;
                            teamGoal.ModifiedOn = dateCreated;

                            List<TeamGoalAccess> teamGoalAccessesList = new List<TeamGoalAccess>();
                            TeamGoalAccess teamGoalAccess = new TeamGoalAccess();
                            teamGoalAccess.UserId = user.Id;
                            teamGoalAccess.OwnerId = currentUser.Id;
                            teamGoalAccess.GoalTypeId = teamGoalUiRender.GoalCategory;
                            teamGoalAccess.GoalAccessTypeId = assigneeAccess.Id;
                            teamGoalAccess.Active = teamGoalUiRender.Active;
                            teamGoalAccess.AddedOn = dateCreated;
                            teamGoalAccess.ModifiedOn = dateCreated;
                            teamGoalAccessesList.Add(teamGoalAccess);
                            teamGoalAccess = new TeamGoalAccess();
                            teamGoalAccess.UserId = currentUser.Id;
                            teamGoalAccess.OwnerId = currentUser.Id;
                            teamGoalAccess.GoalTypeId = teamGoalUiRender.GoalCategory;
                            teamGoalAccess.GoalAccessTypeId = approverAccess.Id;
                            teamGoalAccess.Active = teamGoalUiRender.Active;
                            teamGoalAccess.AddedOn = dateCreated;
                            teamGoalAccess.ModifiedOn = dateCreated;
                            teamGoalAccessesList.Add(teamGoalAccess);

                            GoalStatus goalStatus = new GoalStatus();
                            goalStatus.UserId = user.Id;
                            goalStatus.OwnerId = currentUser.Id;
                            goalStatus.GoalTypeId = teamGoalUiRender.GoalCategory;
                            goalStatus.GoalCatType = nueGoalCatgoryType.Id;
                            goalStatus.GoalStartDate = teamGoalUiRender.StartDate;
                            goalStatus.GoalEndDate = teamGoalUiRender.EndDate;
                            goalStatus.GoalStatusId = goalStatusM.Id;
                            goalStatus.AddedOn = dateCreated;
                            goalStatus.ModifiedOn = dateCreated;

                            TeamGoalMapper teamGoalMapper = new TeamGoalMapper();
                            teamGoalMapper.teamGoal = teamGoal;
                            teamGoalMapper.teamGoalAccesses = teamGoalAccessesList;
                            teamGoalMapper.goalStatus = goalStatus;
                            teamGoalMappers.Add(teamGoalMapper);
                        }
                    }



                }
                else
                {
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfilesX();
                    ViewData["UserMasterList"] = userProfiles;
                    ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                    ViewData["TeamGoalUiRender"] = new TeamGoalUiRender();
                    TempData["Message"] = "Invalid request";
                    return View();
                }

                //NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("PAM", "PGBRequest");
                
                /*var mailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate.txt"));

                var dateCreated = DateTime.UtcNow;
                //List<UserProfile> userProfiles = new DataAccess().getAllUserProfileExcept(currentUser.Email.ToLower());
                if (teamGoalUiRender.isValid())
                {

                    //List<DAL.NueUserProfile> nueUserProfiles1 = new DataAccess().getAllUserProfilesDinamic();
                    //List<DAL.NueUserProfile> userProfilesAdmin1 = nueUserProfiles1.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();

                    List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                    List<PGBRequestUsers> posibleUsers = new List<PGBRequestUsers>();
                    
                    var data = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/request-number-tracker.db"));
                    string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();

                    NuRequestActivityMaster nueRequestCat = new DataAccess().getRequestType("HCM", "PGBRequest");
                    NuRequestActivityMaster nueRequestNewStatus = new DataAccess().getRequestStatus("created");

                    PGBRequest pGBRequest = new PGBRequest();
                    pGBRequest.UserId = currentUser.Id;
                    pGBRequest.RequestId = newRequestId;

                    pGBRequest.ProjectName = pGBRequestUiRender.ProjectName;
                    pGBRequest.ClientName = pGBRequestUiRender.ClientName;
                    pGBRequest.CountryId = pGBRequestUiRender.CountryId;
                    pGBRequest.StartDate = pGBRequestUiRender.StartDate;
                    pGBRequest.EndDate = pGBRequestUiRender.EndDate;
                    pGBRequest.StartFinancialQuarter = pGBRequestUiRender.StartFinancialQuarter;
                    pGBRequest.OpMode = pGBRequestUiRender.OpMode;
                    pGBRequest.OpportunitiesCount = pGBRequestUiRender.OpportunitiesCount;
                    pGBRequest.EstimatedRevenue = pGBRequestUiRender.EstimatedRevenue;
                    pGBRequest.NeedVisiaProcessing = pGBRequestUiRender.NeedVisiaProcessing;
                    pGBRequest.Message = pGBRequestUiRender.Message;

                    pGBRequest.AddedOn = dateCreated;
                    pGBRequest.ModifiedOn = dateCreated;
                    int newRequestInternId = new DataAccess().addPGBRequest(pGBRequest);
                    if (newRequestInternId != -1)
                    {
                        if (pGBRequestUiRender.Users != null && pGBRequestUiRender.Users.Count > 0)
                        {
                            foreach (var item in pGBRequestUiRender.Users)
                            {
                                if (nueUserProfiles.Where(x => x.Active == 1 && x.Id == int.Parse(item)).Count() > 0)
                                {
                                    PGBRequestUsers pGBRequestUsers = new PGBRequestUsers();
                                    pGBRequestUsers.RequestId = newRequestId;
                                    pGBRequestUsers.UserId = int.Parse(item);
                                    pGBRequestUsers.PGBRequestId = newRequestInternId;
                                    pGBRequestUsers.AddedOn = dateCreated;
                                    pGBRequestUsers.ModifiedOn = dateCreated;
                                    posibleUsers.Add(pGBRequestUsers);
                                }
                            }
                        }

                        if (posibleUsers.Count > 0)
                        {
                            new DataAccess().addPGBRequestUsers(posibleUsers);
                        }

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
                            //List<DAL.NueUserProfile> nueUserProfiles = new DataAccess().getAllUserProfilesDinamic();
                            List<DAL.NueUserProfile> userProfilesAdmin = nueUserProfiles.Where(x => x.Active == 1 && x.NueAccessMapper.Where(y => (y.NueAccessMaster.AccessDesc == "Root_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_Admin" || y.NueAccessMaster.AccessDesc == "Hcm_User")).Count() > 0).ToList();
                            //List<UserProfile> userProfilesX = new DataAccess().getAllUserProfiles();
                            //List<UserProfile> userProfilesTemp = userProfilesX.Where(x => x.userAccess.Any(y => (y.AccessDesc == "Root_Admin" || y.AccessDesc == "Hcm_Admin" || y.AccessDesc == "Hcm_User"))).ToList();
                            foreach (var item in userProfilesAdmin)
                            {
                                MessagesModel messagesModel = new MessagesModel();
                                messagesModel.Message = "Project Background Verification Request";
                                messagesModel.EmptyMessage = currentUser.FullName + " submited projects background verification request";
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
                    return RedirectToAction("Index");
                }
                else
                {
                    List<UserProfile> userProfiles = new DataAccess().getAllUserProfilesX();
                    ViewData["UserMasterList"] = userProfiles;
                    ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                    ViewData["PGBRequestUiRender"] = pGBRequestUiRender;
                    ViewData["Countries"] = new DataAccess().getAllCountries();
                    TempData["Message"] = "Invalid request";
                    return View();
                }*/

                return null;
            }
            catch (Exception e)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfilesX();
                ViewData["UserMasterList"] = userProfiles;
                ViewData["UserProfileSession"] = (Session["UserProfileSession"] as UserProfile);
                ViewData["TeamGoalUiRender"] = new TeamGoalUiRender();
                TempData["Message"] = "Invalid request";
                return View();
            }
        }

    }
}
