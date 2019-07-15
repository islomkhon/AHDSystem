using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class UserAdModelClass
    {
        public string empId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string adObjectType { get; set; }
        public Guid guid { get; set; }

        public UserAdModelClass()
        {

        }
        public UserAdModelClass(string email, string userName)
        {
            this.userName = userName;
            this.email = email;
        }
    }
}
