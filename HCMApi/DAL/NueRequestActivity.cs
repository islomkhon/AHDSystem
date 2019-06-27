using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestActivity
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public int? PayloadType { get; set; }
        public int? UserId { get; set; }
        public int? RequestId { get; set; }
        public string Request { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual NueRequestActivityMaster PayloadTypeNavigation { get; set; }
        public virtual NueRequestMaster RequestNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
