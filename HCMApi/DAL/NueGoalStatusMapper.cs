using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueGoalStatusMapper
    {
        public int Id { get; set; }
        public int? GoalId { get; set; }
        public int? UserId { get; set; }
        public int? OwnerId { get; set; }
        public int? GoalTypeId { get; set; }
        public int? GoalCatType { get; set; }
        public DateTime? GoalStartDate { get; set; }
        public DateTime? GoalEndDate { get; set; }
        public int? GoalStatusId { get; set; }
        public string Temp { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueGoalLocalRepo Goal { get; set; }
        public virtual NueGoalStatusTypeMaster GoalStatus { get; set; }
        public virtual NueRequestSubType GoalType { get; set; }
        public virtual NueUserProfile Owner { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
