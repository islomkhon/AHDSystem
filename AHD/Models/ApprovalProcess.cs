using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHD.Models
{
    public class ApprovalProcess
    {
        [BsonElement("RequestApprovals")]
        public LinkedList<RequestApproval> requestApprovals { get; set; }
        [BsonElement("RequestStatusStage")]
        public RequestStatus requestStatusStage { get; set; }
        [BsonElement("DateModified")]
        public DateTime dateApproved { get; set; }
    }
}