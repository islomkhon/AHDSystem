using HCMApi.DAL;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        public UiMaterialTableModel DepartmentRequestListUiTableRender(List<DAL.MichaelDepartmentRequestMaster> michaelDepartmentRequestMasters)
        {
            UiMaterialTableModel uiMaterialTableModel = new UiMaterialTableModel();
            //"[{\"Id\":1,\"Name\":\"Leave Cancellation\"},{\"Id\":2,\"Name\":\"Past Leave Apply\"}]"
            List<string> dataCols = new List<string>();
            dataCols.Add("{\"title\":\"Id\",\"field\":\"Id\"}");
            dataCols.Add("{\"title\":\"Name\",\"field\":\"Name\"}");
            dataCols.Add("{\"title\":\"Active/Deactive\",\"field\":\"Active\"}");
            uiMaterialTableModel.DataCols = "[" + string.Join(",", dataCols) + "]";
            var requestTypes = michaelDepartmentRequestMasters.Select(i => new { i.Id, i.RequestTypeName, i.Active });
            List<string> dataRows = new List<string>();
            foreach (var item in requestTypes)
            {
                dataRows.Add("{\"Id\":\"" + item.Id + "\",\"Name\":\"" + item.RequestTypeName + "\",\"Active\":\"" + (item.Active == 1 ? "Active" : "Deactive") + "\"}");
            }
            uiMaterialTableModel.DataRows = "[" + string.Join(",", dataRows) + "]";
            return uiMaterialTableModel;
        }

        /*public UiMaterialTableModel DepartmentRequestListUiTableRender(List<DAL.MichaelDepartmentRequestTypeMaster> departmentRequestList)
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
        }*/

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

        public List<EscalationMapper> getUiMetaEscaltionList(ICollection<MichaelRequestEscalationMapper> michaelRequestEscalationMapper, List<DAL.NueUserProfile> nueUserProfilesMaster)
        {
            List<EscalationMapper> escalationMappers = new List<EscalationMapper>();

            foreach (var item in michaelRequestEscalationMapper)
            {
                EscalationMapper escalationMapper = new EscalationMapper();
                escalationMapper.Level = (int) item.Level;
                escalationMapper.Active = (int)item.Active;
                escalationMapper.MaxSla = (int)item.MaxSla;
                List<UiDropdownItem> Assignees = new List<UiDropdownItem>();

                var slaUsers = item.MichaelRequestEscalationUserBaseMapper;
                foreach (var itemU in slaUsers)
                {
                    if(itemU.Active == 1)
                    {
                        Assignees.Add(new UiDropdownItem(nueUserProfilesMaster.Where(x => x.Id == itemU.UserId && x.Active == 1).SingleOrDefault().Email, itemU.UserId.ToString()));
                    }
                }
                escalationMapper.Assignees = Assignees;
                escalationMappers.Add(escalationMapper);
            }

            return escalationMappers;
        }

        public string getUniqRequestId(string contentRootPath)
        {
            string contentRootPath1 = contentRootPath;
            var path1 = contentRootPath1 + "\\MyStaticFiles\\request-number-tracker.db";
            var data = System.IO.File.ReadAllText(path1);
            string newRequestId = (long.Parse(data, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign) + 1).ToString();
            System.IO.File.WriteAllText(path1, newRequestId);
            return newRequestId;
        }

        public void setUniqRequestId(string contentRootPath, string requestId)
        {
            string contentRootPath1 = contentRootPath;
            var path1 = contentRootPath1 + "\\MyStaticFiles\\request-number-tracker.db";
            System.IO.File.WriteAllText(path1, requestId);
        }

        public static Random getRandom()
        {
            return new Random();
        }

        
    }
}
