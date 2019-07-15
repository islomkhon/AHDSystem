using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestAccessMapper
    {
        public MichaelRequestAccessMapper()
        {
            MichaelRequestApproverStatusMapper = new HashSet<MichaelRequestApproverStatusMapper>();
        }

        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? RequestAccessTypesId { get; set; }
        public int? Active { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual MichaelRequestMaster Request { get; set; }
        public virtual MichaelRequestAccessTypes RequestAccessTypes { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestApproverStatusMapper> MichaelRequestApproverStatusMapper { get; set; }
    }
}
