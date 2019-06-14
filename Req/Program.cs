using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Req
{
    class Program
    {
        static void Main(string[] args)
        {
            NueRequestEntities nueRequestEntities = new NueRequestEntities();
            var userProfile = nueRequestEntities.NueUserProfile.Where(x => x.Id == 1);
            var su = userProfile.ToList();
            foreach (var item in userProfile)
            {
                var ntpl = item.NTPLID;
                var mail = item.NeuUserPreference2.First().IsMailCommunication;
                var c1 = item.NeuUserPreference2.First().FirstApprover;
                var c2 = item.NueAccessMapper.ToList();
                foreach (var items in c2)
                {
                    var ab = items.NueAccessMaster.AccessDesc;

                }

            }
        }
    }
}
