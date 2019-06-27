using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueUserOrgMapper
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OrgUserId { get; set; }
        public int? OrgUserType { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual NueUserProfile OrgUser { get; set; }
        public virtual NeuDesignation OrgUserTypeNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
