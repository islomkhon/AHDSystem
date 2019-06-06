using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class AttachmentLogModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string Request { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string NTPLID { get; set; }
        public int OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string VFileName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class AttachmentLog
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string Request { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string VFileName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}