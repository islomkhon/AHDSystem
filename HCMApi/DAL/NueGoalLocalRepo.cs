using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalLocalRepo
    {
        public NueGoalLocalRepo()
        {
            InverseParent = new HashSet<NueGoalLocalRepo>();
            NueGoalAccessMapper = new HashSet<NueGoalAccessMapper>();
            NueGoalGlobelRepo = new HashSet<NueGoalGlobelRepo>();
            NueGoalStatusMapper = new HashSet<NueGoalStatusMapper>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? InitiOwner { get; set; }
        public int? ParentId { get; set; }
        public int? GoalTypeId { get; set; }
        public int? GoalCatType { get; set; }
        public int? Active { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDesc { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueGoalCatgoryTypeMaster GoalCatTypeNavigation { get; set; }
        public virtual NueRequestSubType GoalType { get; set; }
        public virtual NueUserProfile InitiOwnerNavigation { get; set; }
        public virtual NueGoalLocalRepo Parent { get; set; }
        public virtual NueUserProfile User { get; set; }
        public virtual ICollection<NueGoalLocalRepo> InverseParent { get; set; }
        public virtual ICollection<NueGoalAccessMapper> NueGoalAccessMapper { get; set; }
        public virtual ICollection<NueGoalGlobelRepo> NueGoalGlobelRepo { get; set; }
        public virtual ICollection<NueGoalStatusMapper> NueGoalStatusMapper { get; set; }
    }
}
