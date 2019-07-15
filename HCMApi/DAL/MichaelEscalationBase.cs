using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelEscalationBase
    {
        public MichaelEscalationBase()
        {
            MichaelRequestEscalationMapper = new HashSet<MichaelRequestEscalationMapper>();
        }

        public int Id { get; set; }
        public string EscalationLevel { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestEscalationMapper> MichaelRequestEscalationMapper { get; set; }
    }
}
