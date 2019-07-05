using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class DepartmentRequest
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestTypeDescription { get; set; }
        public int UserId { get; set; }
        public int Active { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
