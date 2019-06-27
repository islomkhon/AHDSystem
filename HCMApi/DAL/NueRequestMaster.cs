using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestMaster
    {
        public NueRequestMaster()
        {
            NueRequestAceessLog = new HashSet<NueRequestAceessLog>();
            NueRequestActivity = new HashSet<NueRequestActivity>();
            NueRequestAttachmentLog = new HashSet<NueRequestAttachmentLog>();
        }

        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? IsApprovalProcess { get; set; }
        public int? CreatedBy { get; set; }
        public int? RequestStatus { get; set; }
        public int? PayloadId { get; set; }
        public int? RequestCatType { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile CreatedByNavigation { get; set; }
        public virtual NueRequestSubType RequestCatTypeNavigation { get; set; }
        public virtual NueRequestStatus RequestStatusNavigation { get; set; }
        public virtual ICollection<NueRequestAceessLog> NueRequestAceessLog { get; set; }
        public virtual ICollection<NueRequestActivity> NueRequestActivity { get; set; }
        public virtual ICollection<NueRequestAttachmentLog> NueRequestAttachmentLog { get; set; }
    }
}
