using AHD.App_Start;
using AHD.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHD.Controllers
{
    public class NeuUserManagementController : Controller
    {
        MongoContext _dbContext;
        public NeuUserManagementController()
        {
            _dbContext = new MongoContext();
        }

        // GET: NeuUserManagement
        public ActionResult Index()
        {
            var userDetails = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile").Find(new BsonDocument()).ToList();
            return View(userDetails);
        }

        // GET: NeuUserManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NeuUserManagement/AddNewNeuUser
        public ActionResult AddNewNeuUser()
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
