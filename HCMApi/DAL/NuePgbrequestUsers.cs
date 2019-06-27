using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NuePgbrequestUsers
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? UserId { get; set; }
        public int? PgbrequestId { get; set; }
        public string Temp { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NuePgbrequest Pgbrequest { get; set; }
        public virtual NueUserProfile User { get; set; }
    }
}
