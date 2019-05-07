using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;

namespace AHD.App_Start
{
    public class MongoContext
    {
        MongoClient _client;
        public IMongoDatabase _database;

        public MongoContext()        //constructor   
        {
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; 
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"];
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; 
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"]; 
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];
            

            var credential = MongoCredential.CreateCredential(MongoDatabaseName, MongoUsername, MongoPassword);
            MongoClientSettings settings = new MongoClientSettings();
            settings.Credential = credential;
            
            MongoServerAddress address = new MongoServerAddress(MongoHost, int.Parse(MongoPort));
            settings.Server = address;
            _client = new MongoClient(settings);
            _database = _client.GetDatabase(MongoDatabaseName.ToString());
        }

    }
}