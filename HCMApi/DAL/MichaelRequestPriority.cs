using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestPriority
    {
        public MichaelRequestPriority()
        {
            MichaelDepartmentRequestMaster = new HashSet<MichaelDepartmentRequestMaster>();
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
        }

        public int Id { get; set; }
        public string RequestPriority { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelDepartmentRequestMaster> MichaelDepartmentRequestMaster { get; set; }
        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
    }
}
