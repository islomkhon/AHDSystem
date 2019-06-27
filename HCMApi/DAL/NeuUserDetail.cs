using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NeuUserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string LoginNameWithDomain { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Manager { get; set; }
        public string ManagerEmail { get; set; }
        public string Department { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
