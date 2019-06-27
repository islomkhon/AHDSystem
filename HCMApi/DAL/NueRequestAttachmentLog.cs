using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestAttachmentLog
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public string Request { get; set; }
        public int? UserId { get; set; }
        public int? OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string VfileName { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual NueUserProfile Owner { get; set; }
        public virtual NueRequestMaster RequestNavigation { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
