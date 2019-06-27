using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuDesignation
    {
        public NeuDesignation()
        {
            NueUserOrgMapper = new HashSet<NueUserOrgMapper>();
            NueUserProfile = new HashSet<NueUserProfile>();
        }

        public int Id { get; set; }
        public string Desig { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueUserOrgMapper> NueUserOrgMapper { get; set; }
        public virtual ICollection<NueUserProfile> NueUserProfile { get; set; }
    }
}
