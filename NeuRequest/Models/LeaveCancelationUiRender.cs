using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class NeuLeaveCancelation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public string AddedOn { get; set; }
    }

    public class LeaveCancelationUiRender
    {
        
        public string leaveStartDate { get; set; }
        public string leaveEndDate { get; set; }
        public string message { get; set; }
        public string leaveCancelationApprover { get; set; }
        public bool isValid()
        {
            if (this.leaveCancelationApprover != null
                && this.leaveStartDate != null
                && this.leaveEndDate != null
                && this.leaveCancelationApprover.Trim() != ""
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