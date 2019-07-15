using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestPayloadMaster
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? PayloadDataType { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelPayloadDataType PayloadDataTypeNavigation { get; set; }
        public virtual MichaelRequestMaster Request { get; set; }
    }
}
