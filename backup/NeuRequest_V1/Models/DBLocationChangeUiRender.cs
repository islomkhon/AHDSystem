using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class DBLocationChangeRequestModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DBLocationChangeRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DBLocationChangeUiRender
    {
        public string Location { get; set; }
        public string Message { get; set; }
        public bool isValid()
        {
            if (this.Location != null && this.Location.Trim() != "")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}