using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestEscalationUserBaseMapper
    {
        public MichaelRequestEscalationUserBaseMapper()
        {
            MichaelRequestEscalationAccessLogs = new HashSet<MichaelRequestEscalationAccessLogs>();
            MichaelRequestEscalationDurationLogs = new HashSet<MichaelRequestEscalationDurationLogs>();
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
        }

        public int Id { get; set; }
        public int? Active { get; set; }
        public string Payload { get; set; }
        public int? UserId { get; set; }
        public int? RequestEscalationMapperId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelRequestEscalationMapper RequestEscalationMapper { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestEscalationAccessLogs> MichaelRequestEscalationAccessLogs { get; set; }
        public virtual ICollection<MichaelRequestEscalationDurationLogs> MichaelRequestEscalationDurationLogs { get; set; }
        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
    }
}
