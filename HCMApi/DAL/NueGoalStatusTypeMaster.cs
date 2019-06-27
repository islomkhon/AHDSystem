using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalStatusTypeMaster
    {
        public NueGoalStatusTypeMaster()
        {
            NueGoalStatusMapper = new HashSet<NueGoalStatusMapper>();
        }

        public int Id { get; set; }
        public string StausDesc { get; set; }
        public string Temp { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<NueGoalStatusMapper> NueGoalStatusMapper { get; set; }
    }
}
