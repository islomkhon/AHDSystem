using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelDepartmentRequestMaster
    {
        public MichaelDepartmentRequestMaster()
        {
            MichaelRequestEscalationMapper = new HashSet<MichaelRequestEscalationMapper>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
        }

        public int Id { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestTypeDescription { get; set; }
        public int? UserId { get; set; }
        public int? DepartmentId { get; set; }
        public int? RequestPriorityId { get; set; }
        public int? Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelRequestPriority RequestPriority { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestEscalationMapper> MichaelRequestEscalationMapper { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
    }
}
