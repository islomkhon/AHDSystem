using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestAccessTypes
    {
        public MichaelRequestAccessTypes()
        {
            MichaelRequestAccessMapper = new HashSet<MichaelRequestAccessMapper>();
            MichaelRequestApproverStatusMapper = new HashSet<MichaelRequestApproverStatusMapper>();
        }

        public int Id { get; set; }
        public string AccessTypes { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestAccessMapper> MichaelRequestAccessMapper { get; set; }
        public virtual ICollection<MichaelRequestApproverStatusMapper> MichaelRequestApproverStatusMapper { get; set; }
    }
}
