using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelAdminUserMaster
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Admin { get; set; }
        public int? AddedBy { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual NueUserProfile AddedByNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
