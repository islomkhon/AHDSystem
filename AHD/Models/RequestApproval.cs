using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHD.Models
{
    public class RequestApproval
    {
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("UserProfile")]
        public NueUserProfile nueUserProfile { get; set; }
        [BsonElement("IsApproved")]
        public bool isApproved { get; set; }
        [BsonElement("ApprovalComments")]
        public string approvalComments { get; set; }
        [BsonElement("RequestStatusStage")]
        public RequestStatus requestStatusStage { get; set; }
        [BsonElement("DateApproved")]
        public DateTime dateApproved { get; set; }
    }
}