using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelPayloadDataType
    {
        public MichaelPayloadDataType()
        {
            MichaelRequestPayloadMaster = new HashSet<MichaelRequestPayloadMaster>();
        }

        public int Id { get; set; }
        public string DataType { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<MichaelRequestPayloadMaster> MichaelRequestPayloadMaster { get; set; }
    }
}
