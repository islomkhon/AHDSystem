using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuUserPreference
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? IsMailCommunication { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? FirstApprover { get; set; }
        public int? SecondApprover { get; set; }

        public virtual NueUserProfile FirstApproverNavigation { get; set; }
        public virtual NueUserProfile SecondApproverNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
