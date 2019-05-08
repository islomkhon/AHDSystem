using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AHD.Models
{
    public class NueUserProfile
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("Email")]
        public string email { get; set; }
        [BsonElement("FullName")]
        public string fullName { get; set; }
        [BsonElement("FirstName")]
        public string firstName { get; set; }
        [BsonElement("MiddleName")]
        public string middleName { get; set; }
        [BsonElement("LastName")]
        public string lastName { get; set; }
        [BsonElement("EmploymentStatus")]
        public EmploymentStatus employmentStatus { get; set; }
        [BsonElement("DateofJoining")]
        public string dateofJoining { get; set; }
        [BsonElement("Practice")]
        public Practice practice { get; set; }
        [BsonElement("Location")]
        public string location { get; set; }
        [BsonElement("JobLevel")]
        public int jobLevel { get; set; }
        [BsonElement("Designation")]
        public string designation { get; set; }
        [BsonElement("Active")]
        public bool active { get; set; }
        [BsonElement("UserAccess")]
        public LinkedList<String> userAccess { get; set; }
    }
}