using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuCountry
    {
        public NeuCountry()
        {
            NuePgbrequest = new HashSet<NuePgbrequest>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string TwoCharCountryCode { get; set; }
        public string ThreeCharCountryCode { get; set; }

        public virtual ICollection<NuePgbrequest> NuePgbrequest { get; set; }
    }
}
