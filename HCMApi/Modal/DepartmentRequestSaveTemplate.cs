using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class DepartmentRequestSaveFormItem
    {
        public string Name { get; set; }
        public string Fieldvalue { get; set; }
        public string Fieldtype { get; set; }
        public bool IsApproverField { get; set; }
    }

    public class DepartmentRequestSaveTemplate
    {
        public DAL.MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster { get; set; }
        public List<DepartmentRequestSaveFormItem> DataFields { get; set; }
    }

    
}
