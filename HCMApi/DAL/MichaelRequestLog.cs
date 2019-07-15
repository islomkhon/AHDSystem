using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestLog
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? RequestLogTypeId { get; set; }
        public int? RequestStageBaseId { get; set; }
        public int? RequestEscalationMapperId { get; set; }
        public int? RequestEscalationUserId { get; set; }
        public int? RequestPriorityId { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual MichaelRequestMaster Request { get; set; }
        public virtual MichaelRequestEscalationMapper RequestEscalationMapper { get; set; }
        public virtual MichaelRequestEscalationUserBaseMapper RequestEscalationUser { get; set; }
        public virtual MichaelRequestLogTypes RequestLogType { get; set; }
        public virtual MichaelRequestPriority RequestPriority { get; set; }
        public virtual MichaelRequestStageBase RequestStageBase { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
