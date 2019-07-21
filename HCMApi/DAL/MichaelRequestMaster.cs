using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestMaster
    {
        public MichaelRequestMaster()
        {
            MichaelRequestAccessMapper = new HashSet<MichaelRequestAccessMapper>();
            MichaelRequestApproverStatusMapper = new HashSet<MichaelRequestApproverStatusMapper>();
            MichaelRequestAttachmentLog = new HashSet<MichaelRequestAttachmentLog>();
            MichaelRequestEscalationAccessLogs = new HashSet<MichaelRequestEscalationAccessLogs>();
            MichaelRequestEscalationDurationLogs = new HashSet<MichaelRequestEscalationDurationLogs>();
            MichaelRequestFeedbackMaster = new HashSet<MichaelRequestFeedbackMaster>();
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
            MichaelRequestPayloadMaster = new HashSet<MichaelRequestPayloadMaster>();
            MichaelRequestStageLogs = new HashSet<MichaelRequestStageLogs>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DepartmentRequestId { get; set; }
        public int? UserId { get; set; }
        public int? RequestStageBaseId { get; set; }
        public int? RequestEscalationMapperId { get; set; }
        public int? RequestEscalationUserId { get; set; }
        public int? RequestPriorityId { get; set; }
        public int? IsApprovalProcess { get; set; }
        public int? IsApprovalProcessComplted { get; set; }
        public int? CurrentSla { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelDepartmentRequestMaster DepartmentRequest { get; set; }
        public virtual MichaelRequestEscalationMapper RequestEscalationMapper { get; set; }
        public virtual MichaelRequestEscalationUserBaseMapper RequestEscalationUser { get; set; }
        public virtual MichaelRequestPriority RequestPriority { get; set; }
        public virtual MichaelRequestStageBase RequestStageBase { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestAccessMapper> MichaelRequestAccessMapper { get; set; }
        public virtual ICollection<MichaelRequestApproverStatusMapper> MichaelRequestApproverStatusMapper { get; set; }
        public virtual ICollection<MichaelRequestAttachmentLog> MichaelRequestAttachmentLog { get; set; }
        public virtual ICollection<MichaelRequestEscalationAccessLogs> MichaelRequestEscalationAccessLogs { get; set; }
        public virtual ICollection<MichaelRequestEscalationDurationLogs> MichaelRequestEscalationDurationLogs { get; set; }
        public virtual ICollection<MichaelRequestFeedbackMaster> MichaelRequestFeedbackMaster { get; set; }
        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual ICollection<MichaelRequestPayloadMaster> MichaelRequestPayloadMaster { get; set; }
        public virtual ICollection<MichaelRequestStageLogs> MichaelRequestStageLogs { get; set; }
    }
}
