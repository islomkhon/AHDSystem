using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class ListUserId
    {
        public List<int> userIds { get; set; }
        public List<string> emails { get; set; }
    }

    public class ListUserRender
    {
        public string UserId { get; set; }
        public string UserMail { get; set; }
    }
}
