using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class Department
    {
        public int Id { get; set; }
        public string Departmentname { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
