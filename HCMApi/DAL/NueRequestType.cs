using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueRequestType
    {
        public NueRequestType()
        {
            NueRequestSubType = new HashSet<NueRequestSubType>();
        }

        public int Id { get; set; }
        public string RequestType { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ICollection<NueRequestSubType> NueRequestSubType { get; set; }
    }
}
