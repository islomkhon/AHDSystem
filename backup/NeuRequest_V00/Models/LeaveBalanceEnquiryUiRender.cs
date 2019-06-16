using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class LeaveBalanceEnquiryModal
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

    public class LeaveBalanceEnquiry
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

    public class LeaveBalanceEnquiryUiRender
    {
        
        public string leaveStartDate { get; set; }
        public string leaveEndDate { get; set; }
        public string message { get; set; }
        public bool isValid()
        {
            if (this.leaveStartDate != null
                && this.leaveEndDate != null
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