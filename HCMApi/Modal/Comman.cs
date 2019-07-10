using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class UiDropdownItem
    {
        public string label { get; set; }
        public string value { get; set; }
        public UiDropdownItem()
        {

        }
        public UiDropdownItem(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
