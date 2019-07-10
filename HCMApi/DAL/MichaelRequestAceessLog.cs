using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestAceessLog
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? OwnerId { get; set; }
        public int? Completed { get; set; }
        public int? DepartmentId { get; set; }
        public string Log { get; set; }
        public int? DepartmentRequestTypeId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelDepartmentRequestTypeMaster DepartmentRequestType { get; set; }
        public virtual NueUserProfile Owner { get; set; }
        public virtual MichaelRequestMaster Request { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
