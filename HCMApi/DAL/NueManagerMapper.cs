using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueManagerMapper
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ManagerId { get; set; }
        public int? IsConsultentManager { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile Manager { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
