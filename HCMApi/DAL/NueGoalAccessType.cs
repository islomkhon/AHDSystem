using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalAccessType
    {
        public NueGoalAccessType()
        {
            NueGoalAccessMapper = new HashSet<NueGoalAccessMapper>();
        }

        public int Id { get; set; }
        public string GoalAccessType { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<NueGoalAccessMapper> NueGoalAccessMapper { get; set; }
    }
}
