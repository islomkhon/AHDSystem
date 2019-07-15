using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestAttachmentLog
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string VfileName { get; set; }
        public string Payload { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual MichaelRequestMaster Request { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
