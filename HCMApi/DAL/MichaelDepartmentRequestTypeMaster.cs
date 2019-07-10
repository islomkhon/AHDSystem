﻿using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelDepartmentRequestTypeMaster
    {
        public MichaelDepartmentRequestTypeMaster()
        {
            MichaelRequestAceessLog = new HashSet<MichaelRequestAceessLog>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
            MichaelRequestPayload = new HashSet<MichaelRequestPayload>();
        }

        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestTypeDescription { get; set; }
        public int? UserId { get; set; }
        public int? Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelDepartmentMaster Department { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelRequestAceessLog> MichaelRequestAceessLog { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
        public virtual ICollection<MichaelRequestPayload> MichaelRequestPayload { get; set; }
    }
}
