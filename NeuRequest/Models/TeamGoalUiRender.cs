using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class TeamGoalModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartFinancialQuarter { get; set; }
        public string OpMode { get; set; }
        public int OpportunitiesCount { get; set; }
        public string EstimatedRevenue { get; set; }
        public int NeedVisiaProcessing { get; set; }
        public string Message { get; set; }
        public List<PGBRequestUsers> posibleUsers { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class TeamGoalMapper
    {
        public TeamGoal teamGoal;
        public List<TeamGoalAccess> teamGoalAccesses;
        public GoalStatus goalStatus;
    }

    public class TeamGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InitiOwner { get; set; }
        public int GoalTypeId { get; set; }
        public int GoalCatType { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class TeamGoalAccess
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public int GoalTypeId { get; set; }
        public int GoalAccessTypeId { get; set; }
        public int Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class TeamGoalUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public int TeamGoalId { get; set; }
        public string NTPLID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }


    public class TeamGoalUiRender
    {
        public int GoalCategory { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Active { get; set; }
        public List<string> Users { get; set; }
        public bool isValid()
        {
            if (this.GoalTitle != null
                && this.GoalDesc != null
                && this.StartDate != null
                && this.EndDate != null
                && this.Users != null
                && this.GoalTitle.Trim() != ""
                && this.GoalDesc.Trim() != ""
                && this.StartDate.Trim() != ""
                && this.EndDate.Trim() != ""
                && this.Users.Count > 0)
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