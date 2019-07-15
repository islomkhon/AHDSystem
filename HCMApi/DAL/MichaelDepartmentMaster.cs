using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelDepartmentMaster
    {
        public MichaelDepartmentMaster()
        {
            MichaelDepartmentRequestMaster = new HashSet<MichaelDepartmentRequestMaster>();
            MichaelRequestEscalationMapper = new HashSet<MichaelRequestEscalationMapper>();
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
        }

        public int Id { get; set; }
        public string Departmentname { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public int? Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelDepartmentRequestMaster> MichaelDepartmentRequestMaster { get; set; }
        public virtual ICollection<MichaelRequestEscalationMapper> MichaelRequestEscalationMapper { get; set; }
        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
    }
}
