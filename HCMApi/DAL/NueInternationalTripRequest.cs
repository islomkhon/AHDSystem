using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueInternationalTripRequest
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int? UserId { get; set; }
        public int? NeedVisiaProcessing { get; set; }
        public string PlaceToVisit { get; set; }
        public string StartDate { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual NueUserProfile User { get; set; }
    }
}
