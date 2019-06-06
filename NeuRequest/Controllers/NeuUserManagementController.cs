using NeuRequest.DB;
using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuRequest.Controllers
{
    public class NeuUserManagementController : Controller
    {
        // GET: NeuUserManagement
        public ActionResult Index()
        {
            if (Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var userAceess = currentUser.userAccess;
            bool userAccess = false;

            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                List<UserProfile> userProfiles = new DataAccess().getAllUserProfiles();
                ViewData["userProfiles"] = userProfiles;
                ViewData["UserProfileSession"] = currentUser;
                return View();
            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access" });
            }
        }


        public ActionResult AddNewNeuUser()
        {
            if (Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var userAceess = currentUser.userAccess;
            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                ViewData["UserProfileSession"] = currentUser;
                UserProfile userProfile = new UserProfile();
                ViewData["userProfile"] = userProfile;
                return View();
            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access" });
            }
        }

        [HttpPost]
        public ActionResult AddNewNeuUser(FormCollection formCollection)
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
            string userAccesParams = formCollection["userAccess"];
            try
            {
                if (Session["UserProfileSession"] == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var userAceess = currentUser.userAccess;
                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    List<UserAccess> userAccesses = new DataAccess().getUserAccess();
                    UserProfile targetUserProfile = new UserProfile();
                    targetUserProfile.NTPLID = NTPLID;
                    targetUserProfile.Email = Email;
                    targetUserProfile.FullName = FullName;
                    targetUserProfile.FirstName = FirstName;
                    targetUserProfile.MiddleName = MiddleName;
                    targetUserProfile.LastName = LastName;
                    targetUserProfile.DateofJoining = DateofJoining;
                    targetUserProfile.EmpStatusId = int.Parse(EmpStatusId);
                    targetUserProfile.PracticeId = int.Parse(PracticeId);
                    targetUserProfile.JLId = int.Parse(JLId);
                    targetUserProfile.DSId = int.Parse(DSId);
                    targetUserProfile.Location = Location;
                    targetUserProfile.Active = int.Parse(Active);
                    if (targetUserProfile.isValid())
                    {
                        targetUserProfile.Email = targetUserProfile.Email.ToLower();
                        List<UserAccess> userAccess = new List<UserAccess>();
                        if (userAccesParams != null && userAccesParams.Trim() != "")
                        {
                            var uAccessTemp = userAccesParams.Split(',');
                            foreach (var item in uAccessTemp)
                            {
                                if (item != null && item.Trim() != "")
                                {
                                    var temp = userAccesses.Where(x => x.AccessItemId == int.Parse(item.Trim()));
                                    if (temp != null && temp.Count() > 0)
                                    {
                                        UserAccess temp1 = temp.First();
                                        temp1.MapperId = targetUserProfile.Id;
                                        userAccess.Add(temp1);
                                    }
                                }
                            }
                        }
                        targetUserProfile.userAccess = userAccess;
                        UserProfile userProfile = new DataAccess().addUserFullProfile(targetUserProfile);
                        if (userProfile != null)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["userProfile"] = targetUserProfile;
                            ViewData["UserProfileSession"] = currentUser;
                            TempData["Message"] = "Invalid Opration";
                            return View();
                        }
                    }
                    else
                    {
                        UserProfile userProfile = targetUserProfile;
                        ViewData["userProfile"] = userProfile;
                        ViewData["UserProfileSession"] = currentUser;
                        TempData["Message"] = "Invalid Opration";
                        return View();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["userProfile"] = new UserProfile();
                    ViewData["UserProfileSession"] = currentUser;
                    TempData["Message"] = "You are not authraized";
                    return View();
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "An error occerd" });
            }
        }

        public ActionResult EditNeuUserDetails(string id)
        {
            if (Session["UserProfileSession"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var userAceess = currentUser.userAccess;
            bool userAccess = false;

            int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
            if (adminUsers > 0)
            {
                UserProfile userProfile = new DataAccess().getUserProfileById(id);
                ViewData["userProfile"] = userProfile;
                ViewData["UserProfileSession"] = currentUser;
                return View();
            }
            else
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "Invalid access" });
            }
        }

        [HttpPost]
        public ActionResult EditNeuUserDetails(FormCollection formCollection)
        {
            string id = formCollection["Id"];
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
            string userAccesParams = formCollection["userAccess"];
            try
            {
                if (Session["UserProfileSession"] == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                UserProfile currentUser = (Session["UserProfileSession"] as UserProfile);
                if (currentUser == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var userAceess = currentUser.userAccess;
                int adminUsers = userAceess.Where(x => (x.AccessDesc == "Root_Admin" || x.AccessDesc == "Hcm_Admin" || x.AccessDesc == "Hcm_User")).Count();
                if (adminUsers > 0)
                {
                    List<UserAccess> userAccesses = new DataAccess().getUserAccess();
                    UserProfile targetUserProfile = new UserProfile();
                    targetUserProfile.Id = int.Parse(id);
                    targetUserProfile.NTPLID = NTPLID;
                    targetUserProfile.Email = Email;
                    targetUserProfile.FullName = FullName;
                    targetUserProfile.FirstName = FirstName;
                    targetUserProfile.MiddleName = MiddleName;
                    targetUserProfile.LastName = LastName;
                    targetUserProfile.DateofJoining = DateofJoining;
                    targetUserProfile.EmpStatusId = int.Parse(EmpStatusId);
                    targetUserProfile.PracticeId = int.Parse(PracticeId);
                    targetUserProfile.JLId = int.Parse(JLId);
                    targetUserProfile.DSId = int.Parse(DSId);
                    targetUserProfile.Location = Location;
                    targetUserProfile.Active = int.Parse(Active);
                    if (targetUserProfile.isValid())
                    {
                        targetUserProfile.Email = targetUserProfile.Email.ToLower();
                        List<UserAccess> userAccess = new List<UserAccess>();
                        if (userAccesParams != null && userAccesParams.Trim() != "")
                        {
                            var uAccessTemp = userAccesParams.Split(',');
                            foreach (var item in uAccessTemp)
                            {
                                if(item != null && item.Trim() != "")
                                {
                                    var temp = userAccesses.Where(x => x.AccessItemId == int.Parse(item.Trim()));
                                    if(temp != null && temp.Count() > 0)
                                    {
                                        UserAccess temp1 = temp.First();
                                        temp1.MapperId = targetUserProfile.Id;
                                        userAccess.Add(temp1);
                                    }
                                }
                            }
                        }
                        targetUserProfile.userAccess = userAccess;
                        UserProfile userProfile = new DataAccess().updateUserFullProfile(targetUserProfile);
                        if(userProfile != null)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            UserProfile userProfile1 = new DataAccess().getUserProfileById(id);
                            ViewData["userProfile"] = userProfile1;
                            ViewData["UserProfileSession"] = currentUser;
                            TempData["Message"] = "Invalid Opration";
                            return View();
                        }
                    }
                    else
                    {
                        UserProfile userProfile = new DataAccess().getUserProfileById(id);
                        ViewData["userProfile"] = userProfile;
                        ViewData["UserProfileSession"] = currentUser;
                        TempData["Message"] = "Invalid Opration";
                        return View();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    UserProfile userProfile = new DataAccess().getUserProfileById(id);
                    ViewData["userProfile"] = userProfile;
                    ViewData["UserProfileSession"] = currentUser;
                    TempData["Message"] = "You are not authraized";
                    return View();
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("AccessError", "ErrorHandilar", new { message = "An error occerd" });
            }
        }

        // GET: NeuUserManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NeuUserManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NeuUserManagement/Create
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

        // GET: NeuUserManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NeuUserManagement/Edit/5
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

        // GET: NeuUserManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NeuUserManagement/Delete/5
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
