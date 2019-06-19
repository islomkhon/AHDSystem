using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class MessagesModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public int Processed { get; set; }
        public int UserId { get; set; }
        public string NTPLID { get; set; }
        public string FullName { get; set; }
        public string Target { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime getLocalAddedOn { get { return this.MessageDate.ToLocalTime(); } }
        public string getRedableTime { get { return new Utils().RelativeDate(this.MessageDate.ToLocalTime()); } }
        
    }

    public class Messages
    {
        public int MessageID { get; set; }

        public string Message { get; set; }

        public string EmptyMessage { get; set; }

        public int Processed { get; set; }

        public DateTime MessageDate { get; set; }

        public DateTime getLocalAddedOn { get { return this.MessageDate.ToLocalTime(); } }

        public string getRedableTime { get { return new Utils().RelativeDate(this.MessageDate.ToLocalTime()); } }

        //public int Id { get; set; }
        //public string Message { get; set; }
        //public string MessageBody { get; set; }
        //public int UserId { get; set; }
        //public string NTPLID { get; set; }
        //public string FullName { get; set; }
        //public int Seen { get; set; }
        //public DateTime AddedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        //public DateTime getLocalAddedOn { get { return this.AddedOn.ToLocalTime(); } }
        //public DateTime getLocalModifiedOn { get { return this.ModifiedOn.ToLocalTime(); } }

        
    }
}