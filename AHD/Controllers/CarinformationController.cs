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
    public class CarinformationController : Controller
    {
        MongoContext _dbContext;
        public CarinformationController()
        {
            _dbContext = new MongoContext();
        }


        // GET: Carinformation
        public ActionResult Index()
        {

            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"];
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"];
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];

            /*string mongoDbAuthMechanism = "SCRAM-SHA-1";
            MongoInternalIdentity internalIdentity =
          new MongoInternalIdentity("admin", MongoUsername);
            PasswordEvidence passwordEvidence = new PasswordEvidence(MongoPassword);
            MongoCredential mongoCredential =
                 new MongoCredential(mongoDbAuthMechanism,
                         internalIdentity, passwordEvidence);
            List<MongoCredential> credentials =
                       new List<MongoCredential>() { mongoCredential };

            MongoClientSettings settings = new MongoClientSettings();
            // comment this line below if your mongo doesn't run on secured mode
            settings.Credentials = credentials;
            String mongoHost = MongoHost;
            MongoServerAddress address = new MongoServerAddress(mongoHost);
            settings.Server = address;

            MongoClient client = new MongoClient(settings);

            var mongoServer = client.GetDatabase("CarModel");*/

            var carDetails = _dbContext._database.GetCollection<CarModel>("CarModel").Find(new BsonDocument()).ToList();
            return View(carDetails);
        }

        // GET: Carinformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carinformation/Create
        [HttpPost]
        public ActionResult Create(CarModel carmodel)
        {

            //Gets a MongoCollection instance representing a collection on this database
            var document = _dbContext._database.GetCollection<CarModel>("CarModel");

            //var filter = Builders<CarModel>.Filter.Eq("Carname", carmodel.Carname);

            // Added Filter to Check Duplicate records on Bases of Carname and Color (CarColor)   
            var query = Query.And(Query.EQ("Carname", carmodel.Carname), Query.EQ("Color", carmodel.Color));
            // Will Return Count if same document exists else will Return 0 
            
            //var filter = Builders<CarModel>.Filter.Eq(x => x.Carname, carmodel.Carname);
            //var count = 0;
            //filter = Builders<CarModel>.Filter.Eq("Name", "");

            //document.Find<CarModel>(query).Count();

            var filter = (Builders<CarModel>.Filter.Eq("Carname", carmodel.Carname) & Builders<CarModel>.Filter.Eq("Color", carmodel.Color));
            //filter = filter & (Builders<User>.Filter.Eq(x => x.B, "4") | Builders<User>.Filter.Eq(x => x.B, "5"));

            var count = document.Find<CarModel>(filter).CountDocuments();
            //if it is 0 then only we are going to insert document
            if (count == 0)
            {
                document.InsertOne(carmodel);
            }
            else
            {
                TempData["Message"] = "Carname ALready Exist";
                return View("Create", carmodel);
            }
            return RedirectToAction("Create");

        }

        // GET: Carinformation/Details/5
        public ActionResult Details(string id)
        {
            //var carId = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
            var filter = (Builders<CarModel>.Filter.Eq("Id", new ObjectId(id)));
            var bson = new BsonDocument();
            var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel").Find(filter);
            var car = new CarModel();
            try
            {
                car = carDetail.First();
            }
            catch (Exception e)
            { 

            }
            return View(car);
        }

        // GET: Carinformation/Edit/5
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<CarModel>("CarModel");
            var filter = (Builders<CarModel>.Filter.Eq("Id", new ObjectId(id)));
            var carDetailscount = document.Find<CarModel>(filter).CountDocuments();

            if (carDetailscount > 0)
            {
                //var carObjectid = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));

                var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel").Find(filter).First();

                return View(carDetail);
            }
            return RedirectToAction("Index");
        }
        
        // POST: Carinformation/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CarModel carmodel)
        {
            try
            {
                carmodel.Id = new ObjectId(id);
                //Mongo Query
                var CarObjectId = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
                var filter = (Builders<CarModel>.Filter.Eq("Id", new ObjectId(id)));

                // Document Collections
                var collection = _dbContext._database.GetCollection<CarModel>("CarModel");
                // Document Update which need Id and Data to Update
                //var result = collection.UpdateOne(CarObjectId, Update.Replace(carmodel), UpdateFlags.None);

                collection.ReplaceOne(filter, carmodel);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carinformation/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            //Mongo Query
            var carObjectid = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
            var filter = (Builders<CarModel>.Filter.Eq("Id", new ObjectId(id)));
            // Getting Detials of car by passing ObjectId
            var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel").Find(filter).First();
            //var carDetails = _dbContext._database.GetCollection<CarModel>("CarModel").FindOne(carObjectid);

            return View(carDetail);
        }

        // POST: Carinformation/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, CarModel CarModel)
        {
            try
            {
                //Mongo Query
                var carObjectid = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
                var filter = (Builders<CarModel>.Filter.Eq("Id", new ObjectId(id)));
                // Document Collections
                var collection = _dbContext._database.GetCollection<CarModel>("CarModel");
                // Document Delete which need ObjectId to Delete Data 
                //var result = collection.Remove(carObjectid, RemoveFlags.Single);
                collection.DeleteOne(filter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
