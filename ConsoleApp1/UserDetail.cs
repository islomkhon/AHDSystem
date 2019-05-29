namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserDetail")]
    public partial class UserDetail
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string MiddleName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string LoginName { get; set; }

        [StringLength(250)]
        public string LoginNameWithDomain { get; set; }

        [StringLength(250)]
        public string StreetAddress { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(250)]
        public string PostalCode { get; set; }

        [StringLength(250)]
        public string Country { get; set; }

        [StringLength(250)]
        public string HomePhone { get; set; }

        [StringLength(250)]
        public string Extension { get; set; }

        [StringLength(250)]
        public string Mobile { get; set; }

        [StringLength(250)]
        public string Fax { get; set; }

        [StringLength(450)]
        public string EmailAddress { get; set; }

        [StringLength(450)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Company { get; set; }

        [StringLength(250)]
        public string Manager { get; set; }

        [StringLength(450)]
        public string ManagerEmail { get; set; }

        [StringLength(450)]
        public string Department { get; set; }

        [Column(TypeName = "date")]
        public DateTime AddedOn { get; set; }
    }
}
