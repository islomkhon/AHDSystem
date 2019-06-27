using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalGlobelRepo
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GoalTypeId { get; set; }
        public int? GoalCatType { get; set; }
        public int? Active { get; set; }
        public int? LocalRepoId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueRequestSubType GoalType { get; set; }
        public virtual NueGoalLocalRepo LocalRepo { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
