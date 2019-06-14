using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class NueRequestAttchment
    {
        public string requestId { get; set; }
        public string userId { get; set; }
        public HttpPostedFileBase requestAtchmentFile { get; set; }
    }
}