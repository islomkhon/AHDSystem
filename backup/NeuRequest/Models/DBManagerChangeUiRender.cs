using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{

    public class DBManagerChangeRequestModal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string RequestId { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNTPLID { get; set; }
        public string ManagerEmail { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DBManagerChangeRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }
        public int ManagerId { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DBManagerChangeUiRender
    {
        public string ManagerId { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public bool isValid()
        {
            if (this.ManagerId != null && this.ManagerId.Trim() != "")
            {
                try
                {
                    if(int.Parse(this.ManagerId) <= 0)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}