using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class MailItem
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Priority { get; set; }

    }
}