using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestStageBase
    {
        public MichaelRequestStageBase()
        {
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
            MichaelRequestStageLogs = new HashSet<MichaelRequestStageLogs>();
        }

        public int Id { get; set; }
        public string StageType { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
        public virtual ICollection<MichaelRequestStageLogs> MichaelRequestStageLogs { get; set; }
    }
}
