using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class PGBRequestModal
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

    public class PGBRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int CountryId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartFinancialQuarter { get; set; }
        public string OpMode { get; set; }
        public int OpportunitiesCount { get; set; }
        public string EstimatedRevenue { get; set; }
        public int NeedVisiaProcessing { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class PGBRequestUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public int PGBRequestId { get; set; }
        public string ClientName { get; set; }
        public string NTPLID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }


    public class PGBRequestUiRender
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int CountryId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartFinancialQuarter { get; set; }
        public string OpMode { get; set; }
        public int OpportunitiesCount { get; set; }
        public string EstimatedRevenue { get; set; }
        public int NeedVisiaProcessing { get; set; }
        public List<string> Users { get; set; }
        public string Message { get; set; }
        public bool isValid()
        {
            if (this.ProjectName != null
                && this.ClientName != null
                && this.ProjectName.Trim() != ""
                && this.ClientName.Trim() != "")
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