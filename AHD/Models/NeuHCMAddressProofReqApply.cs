using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AHD.Models
{
    [BsonDiscriminator("NeuHCMAddressProofReqApply")]
    public class NeuHCMAddressProofReqApply
    {
        /*[BsonId]
        public ObjectId Id { get; set; }*/
        [BsonElement("RequestId")]
        public string requestId { get; set; }
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("UserProfile")]
        public NueUserProfile nueUserProfile { get; set; }
        [BsonElement("LeaveStartDate")]
        public string leaveStartDate { get; set; }
        [BsonElement("LeaveEndDate")]
        public string leaveEndDate { get; set; }
        [BsonElement("Message")]
        public string message { get; set; }
        [BsonElement("IsApprovalProcess")]
        public bool isApprovalProcess { get; set; }
        [BsonElement("ApprovalProcess")]
        public ApprovalProcess approvalProcess { get; set; }
        [BsonElement("RequestStatus")]
        public RequestStatus requestStatus { get; set; }
        [BsonElement("DateCreated")]
        public DateTime dateCreated { get; set; }
        [BsonElement("DateApproved")]
        public DateTime dateApproved { get; set; }
        [BsonElement("NeuHCMAddressProofReqUiRender")]
        public NeuHCMAddressProofReqUiRender leaveCancelationUiRender { get; set; }

    }

    public class NeuHCMAddressProofReqUiRender
    {
        [BsonElement("LeaveStartDate")]
        public string leaveStartDate { get; set; }
        [BsonElement("LeaveEndDate")]
        public string leaveEndDate { get; set; }
        [BsonElement("Message")]
        public string message { get; set; }
        [BsonElement("LeaveCancelationApprover")]
        public string leaveCancelationApprover { get; set; }
        public bool isValid()
        {
            if (this.message != null
                && this.message.Trim() != "")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}