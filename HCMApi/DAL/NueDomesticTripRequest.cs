using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueDomesticTripRequest
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? UserId { get; set; }
        public int? Accommodation { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile User { get; set; }
    }
}
