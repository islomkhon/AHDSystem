using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class NuRequestAceessLog
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public int Completed { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}