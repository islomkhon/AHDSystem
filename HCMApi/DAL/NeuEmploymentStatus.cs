using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuEmploymentStatus
    {
        public NeuEmploymentStatus()
        {
            NueUserProfile = new HashSet<NueUserProfile>();
        }

        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueUserProfile> NueUserProfile { get; set; }
    }
}
