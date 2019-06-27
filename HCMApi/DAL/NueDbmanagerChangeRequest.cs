using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueDbmanagerChangeRequest
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? UserId { get; set; }
        public int? ManagerId { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile Manager { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
