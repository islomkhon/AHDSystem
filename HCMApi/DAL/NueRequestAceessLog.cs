using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestAceessLog
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? Completed { get; set; }
        public int? OwnerId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile Owner { get; set; }
        public virtual NueRequestMaster Request { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
