using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelDepartmentMaster
    {
        public MichaelDepartmentMaster()
        {
            MichaelDepartmentRequestTypeMaster = new HashSet<MichaelDepartmentRequestTypeMaster>();
        }

        public int Id { get; set; }
        public string Departmentname { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public int? Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<MichaelDepartmentRequestTypeMaster> MichaelDepartmentRequestTypeMaster { get; set; }
    }
}
