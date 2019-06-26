using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCMApi.Modal
{
    public class UserPreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsMailCommunication { get; set; }
        public int FirstApprover { get; set; }
        public int SecondApprover { get; set; }
    }
}