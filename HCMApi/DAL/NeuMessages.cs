using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuMessages
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public int? Processed { get; set; }
        public int? UserId { get; set; }
        public string Target { get; set; }
        public DateTime? Date { get; set; }

        public virtual NueUserProfile User { get; set; }
    }
}
