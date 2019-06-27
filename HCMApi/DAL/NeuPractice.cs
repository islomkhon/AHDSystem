using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuPractice
    {
        public NeuPractice()
        {
            NueUserProfile = new HashSet<NueUserProfile>();
        }

        public int Id { get; set; }
        public string Practice { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueUserProfile> NueUserProfile { get; set; }
    }
}
