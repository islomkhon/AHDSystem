using AHD.App_Start;
using AHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHD.Controllers
{
    public class HcmAHDDashboardController : Controller
    {
        MongoContext _dbContext;
        public HcmAHDDashboardController()
        {
            _dbContext = new MongoContext();
        }

        // GET: HcmAHDDashboard
        public ActionResult Index()
        {
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            return View();
        }

        // GET: HcmAHDDashboard/Create
        public ActionResult LeaveCancelationCreate()
        {
            List<NueUserProfile> returnUserList = new MongoCommunicator().getUserList();
            returnUserList.Remove(returnUserList.Where(x => x.email == (Session["userProfileSession"] as NueUserProfile).email).First());
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["UserMasterList"] = returnUserList;
            return View(returnUserList);
        }

        
        [HttpPost]
        public ActionResult LeaveCancelationCreate(FormCollection formCollection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HcmAHDDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HcmAHDDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HcmAHDDashboard/Create
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

        // GET: HcmAHDDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HcmAHDDashboard/Edit/5
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

        // GET: HcmAHDDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HcmAHDDashboard/Delete/5
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
