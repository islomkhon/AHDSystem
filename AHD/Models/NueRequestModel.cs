using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace AHD.Models
{
    public class NueRequestModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("RequestId")]
        public string requestId { get; set; }
        [BsonElement("NTPLID")]
        public string ntplId { get; set; }
        [BsonElement("UserProfile")]
        public NueUserProfile nueUserProfile { get; set; }
        [BsonElement("RequestType")]
        public RequestType requestType { get; set; }
        [BsonElement("RequestPayload")]
        public Object requestPayload { get; set; }
        [BsonElement("AccessLists")]
        public LinkedList<string> accessLists { get; set; }
        [BsonElement("RequestLogs")]
        public LinkedList<RequestLog> requestLogs { get; set; }
        [BsonElement("AttachmentLogs")]
        public LinkedList<AttachmentLog> attachmentLogs { get; set; }
        [BsonElement("RequestStatus")]
        public RequestStatus requestStatus { get; set; }
        [BsonElement("DateCreated")]
        public DateTime dateCreated { get; set; }
        [BsonElement("DateModified")]
        public DateTime dateModified { get; set; }
        public DateTime getLocalDateTime { get { return this.dateCreated.ToLocalTime(); } }
        public DateTime getLocalDateTimeModified { get { return this.dateModified.ToLocalTime(); } }
    }
}