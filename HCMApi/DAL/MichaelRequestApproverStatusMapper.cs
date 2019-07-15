using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestApproverStatusMapper
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? RequestAccessId { get; set; }
        public int? RequestAccessTypesId { get; set; }
        public int? ApproverStatusId { get; set; }
        public int? Active { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual MichaelApproverStatusTypes ApproverStatus { get; set; }
        public virtual MichaelRequestMaster Request { get; set; }
        public virtual MichaelRequestAccessMapper RequestAccess { get; set; }
        public virtual MichaelRequestAccessTypes RequestAccessTypes { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
