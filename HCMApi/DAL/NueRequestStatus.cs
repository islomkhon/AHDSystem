using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestStatus
    {
        public NueRequestStatus()
        {
            MichaelRequestMaster = new HashSet<MichaelRequestMaster>();
            NueRequestMaster = new HashSet<NueRequestMaster>();
        }

        public int Id { get; set; }
        public string RequestStatus { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<MichaelRequestMaster> MichaelRequestMaster { get; set; }
        public virtual ICollection<NueRequestMaster> NueRequestMaster { get; set; }
    }
}
