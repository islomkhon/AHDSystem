using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace AHD.Models
{
    [BsonDiscriminator("AttachmentLog")]
    public class AttachmentLog
    {
        [BsonElement("RequestId")]
        public string requestId { get; set; }
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("UserProfile")]
        public NueUserProfile nueUserProfile { get; set; }
        [BsonElement("FileName")]
        public string fileName { get; set; }
        [BsonElement("FileExt")]
        public string fileExt { get; set; }
        [BsonElement("DateCreated")]
        public DateTime dateCreated { get; set; }
    }
}