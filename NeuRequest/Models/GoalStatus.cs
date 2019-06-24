using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class GoalStatus
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public int GoalTypeId { get; set; }
        public int GoalCatType { get; set; }
        public string GoalStartDate { get; set; }
        public string GoalEndDate { get; set; }
        public int GoalStatusId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}