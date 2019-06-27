using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestActivityMaster
    {
        public NueRequestActivityMaster()
        {
            NueRequestActivity = new HashSet<NueRequestActivity>();
        }

        public int Id { get; set; }
        public string ActivityDesc { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueRequestActivity> NueRequestActivity { get; set; }
    }
}
