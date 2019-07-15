using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelApproverStatusTypes
    {
        public MichaelApproverStatusTypes()
        {
            MichaelRequestApproverStatusMapper = new HashSet<MichaelRequestApproverStatusMapper>();
        }

        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestApproverStatusMapper> MichaelRequestApproverStatusMapper { get; set; }
    }
}
