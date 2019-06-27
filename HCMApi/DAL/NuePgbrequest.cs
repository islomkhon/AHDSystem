using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NuePgbrequest
    {
        public NuePgbrequest()
        {
            NuePgbrequestUsers = new HashSet<NuePgbrequestUsers>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? UserId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int? CountryId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartFinancialQuarter { get; set; }
        public string OpMode { get; set; }
        public int? OpportunitiesCount { get; set; }
        public string EstimatedRevenue { get; set; }
        public int? NeedVisiaProcessing { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NeuCountry Country { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<NuePgbrequestUsers> NuePgbrequestUsers { get; set; }
    }
}
