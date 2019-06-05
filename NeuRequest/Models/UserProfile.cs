using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class UserProfile
    {
        public int Id { get; set;}
        public string NTPLID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int EmpStatusId { get; set; }
        public string EmpStatusDesc { get; set; }
        public string DateofJoining { get; set; }
        public int PracticeId { get; set; }
        public string PracticeDesc { get; set; }
        public string Location { get; set; }
        public int JLId { get; set; }
        public string JLDesc { get; set; }
        public int DSId { get; set; }
        public string DSDesc { get; set; }
        public int Active { get; set; }
        public string AddedOn { get; set; }
        public List<UserAccess> userAccess { get; set; }

        public bool isValid()
        {
            if (this.NTPLID != null
                && this.Email != null
                && this.FullName != null
                && this.FirstName != null
                && this.LastName != null
                && this.DateofJoining != null
                && this.EmpStatusId != 0
                && this.PracticeId != 0
                && this.JLId != 0
                && this.DSId != 0
                && this.Location != null
                && this.NTPLID.Trim() != ""
                && this.Email.Trim() != ""
                && this.FullName.Trim() != ""
                && this.FirstName.Trim() != ""
                && this.LastName.Trim() != ""
                && this.DateofJoining.Trim() != ""
                && this.Location.Trim() != "")
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