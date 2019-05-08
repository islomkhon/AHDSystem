using AHD.App_Start;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHD.Models
{
    public class MongoCommunicator
    {
        MongoContext _dbContext;
        public MongoCommunicator()
        {
            _dbContext = new MongoContext();
        }

        public NueUserProfile getActiveUserData(string userEmail)
        {
            NueUserProfile nueUserProfile = null;
            try
            {
                userEmail = userEmail.ToLower();
                var document = _dbContext._database.GetCollection<NueUserProfile>("NueUserProfile");
                var filter = Builders<NueUserProfile>.Filter.Eq("Email", userEmail) & Builders<NueUserProfile>.Filter.Eq("Active", true);
                var userDatas = document.Find<NueUserProfile>(filter);
                if(userDatas != null && userDatas.CountDocuments() > 0)
                {
                    nueUserProfile = userDatas.First();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return nueUserProfile;
        }
    }
}