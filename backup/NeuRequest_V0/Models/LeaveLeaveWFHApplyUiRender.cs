using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class NeuLeaveWFHApplyModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class LeaveWFHApply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class LeaveWFHApplyUiRender
    {
        
        public string leaveStartDate { get; set; }
        public string leaveEndDate { get; set; }
        public string message { get; set; }
        public string leaveWFHApplyApprover { get; set; }
        public bool isValid()
        {
            if (this.leaveWFHApplyApprover != null
                && this.leaveStartDate != null
                && this.leaveEndDate != null
                && this.leaveWFHApplyApprover.Trim() != ""
                && this.leaveStartDate.Trim() != ""
                && this.leaveEndDate.Trim() != "")
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