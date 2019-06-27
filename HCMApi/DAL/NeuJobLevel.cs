using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuJobLevel
    {
        public NeuJobLevel()
        {
            NueUserProfile = new HashSet<NueUserProfile>();
        }

        public int Id { get; set; }
        public string Level { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueUserProfile> NueUserProfile { get; set; }
    }
}
