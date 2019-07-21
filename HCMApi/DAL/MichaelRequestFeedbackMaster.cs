using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class MichaelRequestFeedbackMaster
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? Ratting { get; set; }
        public string FeedbackComment { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual MichaelRequestMaster Request { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
