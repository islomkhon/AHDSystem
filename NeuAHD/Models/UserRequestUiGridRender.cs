using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class UserRequestUiGridRender
    {
        public int NueRequestMasterId { get; set; }
        public string RequestId { get; set; }
        public int NueRequestSubTypeId { get; set; }
        public string RequestSubType { get; set; }
        public int NueRequestStatusId { get; set; }
        public string RequestStatus { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime getLocalAddedOn { get { return this.AddedOn.ToLocalTime(); } }
        public DateTime getLocalModifiedOn { get { return this.ModifiedOn.ToLocalTime(); } }
    }

    public class UserRequest
    {
        public int NueRequestMasterId { get; set; }
        public string RequestId { get; set; }
        public int ApprovalProcess { get; set; }
        public int PayloadId { get; set; }
        public int NueRequestSubTypeId { get; set; }
        public string RequestSubType { get; set; }
        public int NueRequestStatusId { get; set; }
        public string RequestStatus { get; set; }
        public int OwnerId { get; set; }
        public string FullName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime getLocalAddedOn { get { return this.AddedOn.ToLocalTime(); } }
        public DateTime getLocalModifiedOn { get { return this.ModifiedOn.ToLocalTime(); } }
    }
}