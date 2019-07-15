using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestLogTypes
    {
        public MichaelRequestLogTypes()
        {
            MichaelRequestLog = new HashSet<MichaelRequestLog>();
        }

        public int Id { get; set; }
        public string LogType { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestLog> MichaelRequestLog { get; set; }
    }
}
