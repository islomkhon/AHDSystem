using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{

    public class MaterialTableCol
    {
        public string title { get; set; }
        public string field { get; set; }

        public MaterialTableCol() { }

        public MaterialTableCol(string title, string field)
        {
            this.title = title;
            this.field = field;
        }
    }

    public class Utils
    {

        public UiMaterialTableModel DepartmentRequestListUiTableRender(List<DAL.MichaelDepartmentRequestTypeMaster> departmentRequestList)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            //"[{\"Id\":1,\"Name\":\"Leave Cancellation\"},{\"Id\":2,\"Name\":\"Past Leave Apply\"}]"
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"Name\",\"field\":\"Name\"}");
            dataCols.Add("{\"title\":\"Active/Deactive\",\"field\":\"Active\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            var requestTypes = departmentRequestList.Select(i => new { i.Id, i.RequestTypeName, i.Active });
            List<string> dataRows = new List<string>();
            foreach (var item in requestTypes)
            {
                dataRows.Add("{\"Id\":\"" + item.Id + "\",\"Name\":\"" + item.RequestTypeName + "\",\"Active\":\"" + (item.Active == 1 ? "Active":"Deactive") + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        public ListUserId generateUserDropdownList(List<DAL.NueUserProfile> userProfiles)
        {
            ListUserId listUserId = new ListUserId();
            List<int> userIds = new List<int>();
            List<string> emails = new List<string>();

            if(userProfiles != null && userProfiles.Count > 0)
            {
                for (int i = 0; i < userProfiles.Count; i++)
                {
                    try
                    {
                        userIds.Add(userProfiles[i].Id);
                        emails.Add(userProfiles[i].Email);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            listUserId.userIds = userIds;
            listUserId.emails = emails;
            return listUserId;
        }

    }
}
