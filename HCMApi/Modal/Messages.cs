using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAgo;

namespace HCMApi.Modal
{
    public class MessagesModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public int Processed { get; set; }
        public int UserId { get; set; }
        public string NTPLID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Target { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime getLocalAddedOn { get { return this.MessageDate.ToLocalTime(); } }
        public string getRedableTime { get { return this.MessageDate.ToLocalTime().TimeAgo(); } }

    }

    public class MailItem
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Priority { get; set; }

    }

    public class Messages
    {
        public int MessageID { get; set; }

        public string Message { get; set; }

        public string EmptyMessage { get; set; }

        public int Processed { get; set; }

        public DateTime MessageDate { get; set; }

        public DateTime getLocalAddedOn { get { return this.MessageDate.ToLocalTime(); } }

        public string getRedableTime { get { return this.MessageDate.ToLocalTime().TimeAgo(); } }
        
    }
}
