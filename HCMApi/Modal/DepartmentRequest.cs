using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class DepartmentRequestX
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

    public class MichaelDepartmentRequest
    {
        public int Id { get; set; }
        public int RequestPriorityId { get; set; }
        public int Active { get; set; }
        public List<EscalationMapper> EscalationMapper { get; set; }
        public string DepartmentId { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestTypeDescription { get; set; }
        public int UserId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class EscalationMapper
    {
        public int Level { get; set; }
        public int Active { get; set; }
        public int MaxSla { get; set; }
        public List<UiDropdownItem> Assignees { get; set; }
    }

    public class EscalationData1
    {
        public int Active { get; set; }
        public int Level { get; set; }
        public int MaxSla { get; set; }
        public List<UiDropdownItem> Assignees { get; set; }
    }
}
