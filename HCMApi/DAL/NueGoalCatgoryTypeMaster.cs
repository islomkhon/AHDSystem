using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalCatgoryTypeMaster
    {
        public NueGoalCatgoryTypeMaster()
        {
            NueGoalLocalRepo = new HashSet<NueGoalLocalRepo>();
        }

        public int Id { get; set; }
        public string GoalDesc { get; set; }
        public string Temp { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<NueGoalLocalRepo> NueGoalLocalRepo { get; set; }
    }
}
