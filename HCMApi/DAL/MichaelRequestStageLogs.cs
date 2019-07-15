using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestStageLogs
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? RequestStageBaseId { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual MichaelRequestMaster Request { get; set; }
        public virtual MichaelRequestStageBase RequestStageBase { get; set; }
    }
}
