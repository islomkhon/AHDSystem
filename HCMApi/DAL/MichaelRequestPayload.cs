using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestPayload
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? RequestMasterId { get; set; }
        public string Name { get; set; }
        public string Fieldvalue { get; set; }
        public string Fieldtype { get; set; }
        public int? UserId { get; set; }
        public string Payload { get; set; }
        public int? DepartmentId { get; set; }
        public int? DepartmentRequestTypeId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual MichaelDepartmentRequestTypeMaster DepartmentRequestType { get; set; }
        public virtual MichaelRequestMaster RequestMaster { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
