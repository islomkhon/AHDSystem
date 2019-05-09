using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AHD.Models
{
    public class NeuLeaveCancelation
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("UserProfile")]
        public NueUserProfile nueUserProfile { get; set; }
        [BsonElement("LeaveDate")]
        public string leaveDate { get; set; }
        [BsonElement("Message")]
        public string message { get; set; }
        [BsonElement("IsApprovalProcess")]
        public bool isApprovalProcess { get; set; }
        [BsonElement("ApprovalProcess")]
        public ApprovalProcess ApprovalProcess { get; set; }
        [BsonElement("RequestStatus")]
        public RequestStatus requestStatus { get; set; }
        [BsonElement("DateCreated")]
        public DateTime dateCreated { get; set; }
        [BsonElement("DateApproved")]
        public DateTime dateApproved { get; set; }

    }

    public class ApprovalProcess
    {

        [BsonElement("RequestApprovals")]
        public LinkedList<RequestApproval> requestApprovals { get; set; }
        [BsonElement("RequestStatusStage")]
        public RequestStatus requestStatusStage { get; set; }
        [BsonElement("DateModified")]
        public DateTime dateApproved { get; set; }

    }

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