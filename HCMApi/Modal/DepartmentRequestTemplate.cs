using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class DepartmentRequestTemplate
    {
        public int RequestId { get; set; }
        public int DepartmentId { get; set; }
        public List<object> AvilableField { get; set; }
    }

    public class DepartmentRequestTemplateRender
    {
        public int RequestId { get; set; }
        public int DepartmentId { get; set; }
        public DAL.MichaelDepartmentRequestMaster michaelDepartmentRequestMaster { get; set; }
        public List<object> AvilableField { get; set; }
    }


}
