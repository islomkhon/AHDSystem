﻿using AHD.App_Start;
using AHD.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
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
            List<NueUserProfile> userDetails = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile").Find(new BsonDocument()).ToList();
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            ViewData["userDetails"] = userDetails;
            return View();
        }

        // GET: NeuUserManagement/AddNewNeuUser
        public ActionResult AddNewNeuUser()
        {
            ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
            return View();
        }

        // POST: NeuUserManagement/AddNewNeuUser
        [HttpPost]
        public ActionResult AddNewNeuUser(NueUserProfile nueUserProfile)
        {
            try
            {
                var document = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
                var filter = (Builders<NueUserProfile>.Filter.Eq("NTPLID", nueUserProfile.ntplId) 
                    & Builders<NueUserProfile>.Filter.Eq("Email", nueUserProfile.email)
                    & Builders<NueUserProfile>.Filter.Eq("FullName", nueUserProfile.fullName));
                var count = document.Find<NueUserProfile>(filter).CountDocuments();
                if (count == 0)
                {
                    nueUserProfile.email = nueUserProfile.email.ToLower();
                    document.InsertOne(nueUserProfile);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "User details already exist";
                    ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                    return View("AddNewNeuUser", nueUserProfile);
                }
            }
            catch
            {
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                return View("AddNewNeuUser");
            }
        }

        // GET: NeuUserManagement/Edit/5
        public ActionResult EditNeuUserDetails(string id)
        {
            var document = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
            var filter = (Builders<NueUserProfile>.Filter.Eq("Id", new ObjectId(id)));
            var count = document.Find<NueUserProfile>(filter).CountDocuments();
            if (count > 0)
            {
                var userDetail = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile").Find(filter).First();
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                return View(userDetail);
            }
            return RedirectToAction("Index");
        }

        // POST: NeuUserManagement/Edit/5
        [HttpPost]
        public ActionResult EditNeuUserDetails(string id, NueUserProfile nueUserProfile)
        {
            try
            {
                nueUserProfile.Id = new ObjectId(id);

                nueUserProfile.email = nueUserProfile.email.ToLower();

                var document = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
                var filter = Builders<NueUserProfile>.Filter.Eq("Id", new ObjectId(id));
                var count = document.Find<NueUserProfile>(filter).CountDocuments();

                var filterDup = ((Builders<NueUserProfile>.Filter.Eq("NTPLID", nueUserProfile.ntplId)
                    | Builders<NueUserProfile>.Filter.Eq("Email", nueUserProfile.email)
                    | Builders<NueUserProfile>.Filter.Eq("FullName", nueUserProfile.fullName))
                    & Builders<NueUserProfile>.Filter.Ne("Id", new ObjectId(id)));
                var countDup = document.Find<NueUserProfile>(filterDup).CountDocuments();
                if (count > 0 && countDup <= 0)
                {
                    var collection = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
                    collection.ReplaceOne(filter, nueUserProfile);
                    return RedirectToAction("Index");
                }
                else if (countDup > 0)
                {
                    TempData["UiRenderMessage"] = "Information is already in use";
                    return View("EditNeuUserDetails", nueUserProfile);
                }
                else
                {
                    TempData["UiRenderMessage"] = "User details does not exist";
                    return View("EditNeuUserDetails", nueUserProfile);
                }
            }
            catch
            {
                ViewData["NueUserProfile"] = (Session["userProfileSession"] as NueUserProfile);
                return View("EditNeuUserDetails");
            }
        }

        // POST: NeuUserManagement/Delete/5
        [HttpPost]
        public JsonResult DeleteNeuUserDetails(FormCollection formCollection)
        {
            try
            {
                // TODO: Add delete logic here
                string id = formCollection["id"];

                var filter = (Builders<NueUserProfile>.Filter.Eq("Id", new ObjectId(id)));
                var collection = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
                var result = collection.DeleteOne(filter);
                if(result.DeletedCount > 0)
                {
                    return Json(new JsonResponse("Ok", "Data deleted successfully"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new JsonResponse("Failed", "An error occerd. Unable to locate requested information"), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new JsonResponse("Failed", "An error occerd while deleting data"), JsonRequestBehavior.AllowGet);
            }

        }
    }
}
