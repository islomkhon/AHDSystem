using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueAccessMapper
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AccessId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual NueAccessMaster Access { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
