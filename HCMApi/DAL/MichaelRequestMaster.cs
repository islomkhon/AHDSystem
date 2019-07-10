using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestMaster
    {
        public MichaelRequestMaster()
        {
            MichaelRequestAceessLog = new HashSet<MichaelRequestAceessLog>();
            MichaelRequestPayload = new HashSet<MichaelRequestPayload>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? IsApprovalProcess { get; set; }
        public int? UserId { get; set; }
        public int? RequestStatus { get; set; }
        public int? DepartmentId { get; set; }
        public string Payload { get; set; }
        public int? DepartmentRequestTypeId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelDepartmentRequestTypeMaster DepartmentRequestType { get; set; }
        public virtual NueRequestStatus RequestStatusNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestAceessLog> MichaelRequestAceessLog { get; set; }
        public virtual ICollection<MichaelRequestPayload> MichaelRequestPayload { get; set; }
    }
}
