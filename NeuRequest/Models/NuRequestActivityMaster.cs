using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class NuRequestActivityModel
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public int PayloadTypeId { get; set; }
        public string PayloadTypeDesc { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string NTPLID { get; set; }
        public int RequestId { get; set; }
        public string Request { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime getLocalAddedOn { get { return this.AddedOn.ToLocalTime(); } }
        public DateTime getLocalModifiedOn { get { return this.ModifiedOn.ToLocalTime(); } }
    }

    public class NuRequestActivityMaster
    {
        public int Id { get; set; }
        public string ActivityDesc { get; set; }
    }

    public class NuRequestActivity
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public int PayloadType { get; set; }
        public string PayloadTypeDesc { get; set; }
        public int UserId { get; set; }
        public int RequestId { get; set; }
        public string Request { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime getLocalAddedOn { get { return this.AddedOn.ToLocalTime(); } }
        public DateTime getLocalModifiedOn { get { return this.ModifiedOn.ToLocalTime(); } }
    }
}