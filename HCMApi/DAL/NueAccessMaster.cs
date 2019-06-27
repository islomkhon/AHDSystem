using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueAccessMaster
    {
        public NueAccessMaster()
        {
            NueAccessMapper = new HashSet<NueAccessMapper>();
        }

        public int Id { get; set; }
        public string AccessDesc { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueAccessMapper> NueAccessMapper { get; set; }
    }
}
