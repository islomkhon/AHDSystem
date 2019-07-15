using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestEscalationMapper
    {
        public MichaelRequestEscalationMapper()
        {
            MichaelRequestEscalationAccessLogs = new HashSet<MichaelRequestEscalationAccessLogs>();
            MichaelRequestEscalationDurationLogs = new HashSet<MichaelRequestEscalationDurationLogs>();
            MichaelRequestEscalationUserBaseMapper = new HashSet<MichaelRequestEscalationUserBaseMapper>();
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
        }

        public int Id { get; set; }
        public int? Level { get; set; }
        public int? MaxSla { get; set; }
        public int? Active { get; set; }
        public string Payload { get; set; }
        public int? UserId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DepartmentRequestId { get; set; }
        public int? EscalationBaseId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelDepartmentRequestMaster DepartmentRequest { get; set; }
        public virtual MichaelEscalationBase EscalationBase { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestEscalationAccessLogs> MichaelRequestEscalationAccessLogs { get; set; }
        public virtual ICollection<MichaelRequestEscalationDurationLogs> MichaelRequestEscalationDurationLogs { get; set; }
        public virtual ICollection<MichaelRequestEscalationUserBaseMapper> MichaelRequestEscalationUserBaseMapper { get; set; }
        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
    }
}
