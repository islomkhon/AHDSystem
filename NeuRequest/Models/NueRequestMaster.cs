using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class NueRequestMaster
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int CreatedBy { get; set; }
        public int IsApprovalProcess { get; set; }
        public int RequestStatus { get; set; }
        public int PayloadId { get; set; }
        public int RequestCatType { get; set; }
        public string AddedOn { get; set; }
    }
}