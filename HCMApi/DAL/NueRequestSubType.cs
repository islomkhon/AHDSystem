using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestSubType
    {
        public NueRequestSubType()
        {
            NueGoalAccessMapper = new HashSet<NueGoalAccessMapper>();
            NueGoalGlobelRepo = new HashSet<NueGoalGlobelRepo>();
            NueGoalLocalRepo = new HashSet<NueGoalLocalRepo>();
            NueGoalStatusMapper = new HashSet<NueGoalStatusMapper>();
            NueRequestMaster = new HashSet<NueRequestMaster>();
        }

        public int Id { get; set; }
        public string RequestSubType { get; set; }
        public int? RequestType { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual NueRequestType RequestTypeNavigation { get; set; }
        public virtual ICollection<NueGoalAccessMapper> NueGoalAccessMapper { get; set; }
        public virtual ICollection<NueGoalGlobelRepo> NueGoalGlobelRepo { get; set; }
        public virtual ICollection<NueGoalLocalRepo> NueGoalLocalRepo { get; set; }
        public virtual ICollection<NueGoalStatusMapper> NueGoalStatusMapper { get; set; }
        public virtual ICollection<NueRequestMaster> NueRequestMaster { get; set; }
    }
}
