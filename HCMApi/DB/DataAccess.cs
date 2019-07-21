using HCMApi.DAL;
using HCMApi.Extensions;
using HCMApi.Modal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeAgo;

namespace HCMApi.DB
{
    public class DataAccess
    {
        
        string connectionString = String.Empty;

        public DataAccess(AzureAd AzureAdSettings)
        {
            this.connectionString = AzureAdSettings.Db;
        }

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<NueUserProfile> getAllUserProfilesDinamic()
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            return nueRequestContext.NueUserProfile.ToList<NueUserProfile>();
        }

        public NueUserProfile getSpecificUserProfilesByEmail(string email)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var users = nueRequestContext.NueUserProfile.Where(x => x.Email == email);
            if(users != null && users.Count() > 0)
            {
                return users.First<NueUserProfile>();
            }
            else
            {
                return null;
            }
        }

        public List<UserProfile> getAllUserProfileExcept(string email)
        {
            List<UserProfile> userProfiles = new List<UserProfile>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT NP.Id, NTPLID, Email, FullName, FirstName, MiddleName, LastName
	                           ,ems.Id as EmpStatusId, ems.Status as EmpStatusDesc, DateofJoining, pc.Id as PracticeId, 
                                pc.Practice as PracticeDesc, Location, jl.Id as JLId, jl.[LEVEL] as JLDesc, 
                                ds.Id as DSId, ds.Desig as DSDesc, Active, NP.AddedOn
                                FROM NueUserProfile as NP join NeuEmploymentStatus as ems on NP.EmploymentStatus = ems.Id 
                                join NeuPractice as pc on NP.Practice = pc.Id 
                                join NeuJobLevel as jl on NP.JobLevel = jl.Id 
                                join NeuDesignation as ds on NP.Designation = ds.Id 
                                where Email != @Email
                                AND Active = 1";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Email";
                param.Value = email;
                command.Parameters.Add(param);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        UserProfile userProfile = new UserProfile();
                        //var nt = dataReader["NTPLID"];
                        userProfile.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                        userProfile.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
                        userProfile.Email = ConvertFromDBVal<string>(dataReader["Email"]);
                        userProfile.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                        userProfile.FirstName = ConvertFromDBVal<string>(dataReader["FirstName"]);
                        userProfile.MiddleName = ConvertFromDBVal<string>(dataReader["MiddleName"]);
                        userProfile.LastName = ConvertFromDBVal<string>(dataReader["LastName"]);
                        userProfile.EmpStatusId = ConvertFromDBVal<int>(dataReader["EmpStatusId"]);
                        userProfile.EmpStatusDesc = ConvertFromDBVal<string>(dataReader["EmpStatusDesc"]);
                        userProfile.DateofJoining = ConvertFromDBVal<string>(dataReader["DateofJoining"]);
                        userProfile.PracticeId = ConvertFromDBVal<int>(dataReader["PracticeId"]);
                        userProfile.PracticeDesc = ConvertFromDBVal<string>(dataReader["PracticeDesc"]);
                        userProfile.Location = ConvertFromDBVal<string>(dataReader["Location"]);
                        userProfile.JLId = ConvertFromDBVal<int>(dataReader["JLId"]);
                        userProfile.JLDesc = ConvertFromDBVal<string>(dataReader["JLDesc"]);
                        userProfile.DSId = ConvertFromDBVal<int>(dataReader["DSId"]);
                        userProfile.DSDesc = ConvertFromDBVal<string>(dataReader["DSDesc"]);
                        userProfile.Active = ConvertFromDBVal<int>(dataReader["Active"]);
                        userProfile.AddedOn = ConvertFromDBVal<string>(dataReader["AddedOn"].ToString());
                        userProfiles.Add(userProfile);
                    }
                }

                connection.Close();
            }
            return userProfiles;
        }

        public List<MichaelDepartmentMaster> getDepartments()
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            return nueRequestContext.MichaelDepartmentMaster.ToList<MichaelDepartmentMaster>();
        }

        public MichaelDepartmentMaster GetDepartmentDetails(int departmentId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var department = nueRequestContext.MichaelDepartmentMaster.Where(x => x.Id == departmentId);
            if(department != null && department.Count() > 0)
            {
                return department.First<MichaelDepartmentMaster>();
            }
            else
            {
                return null;
            }
        }

        public MichaelDepartmentRequestMaster GetDepartmentRequestDetails(int departmentId, int requestId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var departmentRequestItem = nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.Id == requestId && x.DepartmentId == departmentId);
            if (departmentRequestItem != null && departmentRequestItem.Count() > 0)
            {
                return departmentRequestItem.First<MichaelDepartmentRequestMaster>();
            }
            else
            {
                return null;
            }
        }

        public List<MichaelDepartmentRequestMaster> GetDepartmentActiveRequestTypes()
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var departmentRequestItem = nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.Active == 1);
            if (departmentRequestItem != null && departmentRequestItem.Count() > 0)
            {
                return departmentRequestItem.ToList<MichaelDepartmentRequestMaster>();
            }
            else
            {
                return null;
            }
        }

        public MichaelDepartmentRequestMaster GetDepartmentRequestMetaDetails(int departmentId, int requestId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var departmentRequestItem = nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.Id == requestId && x.DepartmentId == departmentId);
            if (departmentRequestItem != null && departmentRequestItem.Count() > 0)
            {
                MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = departmentRequestItem.First<MichaelDepartmentRequestMaster>();
                michaelDepartmentRequestMaster.MichaelRequestEscalationMapper = nueRequestContext.MichaelRequestEscalationMapper.Where(x => x.DepartmentRequestId == michaelDepartmentRequestMaster.Id && x.DepartmentId == michaelDepartmentRequestMaster.DepartmentId).ToList<MichaelRequestEscalationMapper>();
                foreach (var item in michaelDepartmentRequestMaster.MichaelRequestEscalationMapper)
                {
                    item.MichaelRequestEscalationUserBaseMapper = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == item.Id).ToList<MichaelRequestEscalationUserBaseMapper>();    
                }
                return michaelDepartmentRequestMaster;
            }
            else
            {
                return null;
            }
        }

        

        /*public MichaelDepartmentRequestTypeMaster GetDepartmentRequestDetails(int departmentId, int requestId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var departmentRequestItem = nueRequestContext.MichaelDepartmentRequestTypeMaster.Where(x => x.Id == requestId && x.DepartmentId == departmentId);
            if (departmentRequestItem != null && departmentRequestItem.Count() > 0)
            {
                return departmentRequestItem.First<MichaelDepartmentRequestTypeMaster>();
            }
            else
            {
                return null;
            }
        }*/

        /*public List<MichaelDepartmentRequestTypeMaster> GetDepartmentRequestList(Department department)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            return nueRequestContext.MichaelDepartmentRequestTypeMaster.Where(x => x.DepartmentId == department.Id && x.UserId == department.UserId).ToList<MichaelDepartmentRequestTypeMaster>();
        }*/

        public List<MichaelDepartmentMaster> DepartmentStatusToggle(int departmentId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var department = nueRequestContext.MichaelDepartmentMaster.Where(x => x.Id == departmentId);
            var result = nueRequestContext.MichaelDepartmentMaster.SingleOrDefault(b => b.Id == departmentId);
            if (result != null)
            {
                if(result.Active == 1)
                {
                    result.Active = 0;
                }
                else
                {
                    result.Active = 1;
                }
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    return nueRequestContext.MichaelDepartmentMaster.ToList<MichaelDepartmentMaster>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /*public List<MichaelDepartmentMaster> GetDepartmentRequestList(int departmentId)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var department = nueRequestContext.MichaelDepartmentMaster.Where(x => x.Id == departmentId);
            var result = nueRequestContext.MichaelDepartmentMaster.SingleOrDefault(b => b.Id == departmentId);
            if (result != null)
            {
                if (result.Active == 1)
                {
                    result.Active = 0;
                }
                else
                {
                    result.Active = 1;
                }
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    return nueRequestContext.MichaelDepartmentMaster.ToList<MichaelDepartmentMaster>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }*/


        public JsonResponse updateDepartMentDetails(Department department)
        {
            JsonResponse jsonResponse = new JsonResponse();

            NueRequestContext nueRequestContext = new NueRequestContext();
            if (nueRequestContext.MichaelDepartmentMaster.Where(x => x.Departmentname == department.Departmentname && x.Id != department.Id).Count() <= 0)
            {

                var result = nueRequestContext.MichaelDepartmentMaster.SingleOrDefault(b => b.Id == department.Id);
                if (result != null)
                {
                    var dateCreated = DateTime.UtcNow;
                    result.Departmentname = department.Departmentname;
                    result.Description = department.Description;
                    result.Active = department.Active;
                    result.ModifiedOn = dateCreated;
                    int returnValue = nueRequestContext.SaveChanges();
                    if (returnValue > 0)
                    {
                        jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                    }
                    else
                    {
                        jsonResponse = new JsonResponse("Failed", "An error occerd.");
                    }
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "Unable to locate requested information");
                }

                /*var dateCreated = DateTime.UtcNow;
                MichaelDepartmentMaster michaelDepartmentMaster = new MichaelDepartmentMaster();
                michaelDepartmentMaster.Departmentname = department.Departmentname;
                michaelDepartmentMaster.Description = department.Description;
                michaelDepartmentMaster.UserId = department.UserId;
                michaelDepartmentMaster.Active = 1;
                michaelDepartmentMaster.AddedOn = dateCreated;
                michaelDepartmentMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelDepartmentMaster.Add(michaelDepartmentMaster);
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "An error occerd.");
                }*/
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department name already in use.");

            }
            return jsonResponse;
        }

        public JsonResponse addNewDepartment(Department department)
        {
            JsonResponse jsonResponse = new JsonResponse();

            NueRequestContext nueRequestContext = new NueRequestContext();
            if(nueRequestContext.MichaelDepartmentMaster.Where(x=>x.Departmentname == department.Departmentname).Count() <= 0)
            {
                var dateCreated = DateTime.UtcNow;
                MichaelDepartmentMaster michaelDepartmentMaster = new MichaelDepartmentMaster();
                michaelDepartmentMaster.Departmentname = department.Departmentname;
                michaelDepartmentMaster.Description = department.Description;
                michaelDepartmentMaster.UserId = department.UserId;
                michaelDepartmentMaster.Active = 1;
                michaelDepartmentMaster.AddedOn = dateCreated;
                michaelDepartmentMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelDepartmentMaster.Add(michaelDepartmentMaster);
                int returnValue = nueRequestContext.SaveChanges();
                if(returnValue > 0)
                {
                    jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "An error occerd.");
                }
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department name already exist.");

            }
            return jsonResponse;
        }

        public JsonResponse sincUsersAd(List<UserAdModelClass> toBeAdded, List<UserAdModelClass> toBeRemoved)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            if(toBeRemoved != null && toBeRemoved.Count > 0)
            {
                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    try
                    {
                        var result = nueRequestContext.NueUserProfile.SingleOrDefault(b => b.Email == toBeRemoved[i].email);
                        if (result != null)
                        {
                            result.Active = 0;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            if (toBeAdded != null && toBeAdded.Count > 0)
            {
                for (int i = 0; i < toBeAdded.Count; i++)
                {
                    try
                    {
                        var result = nueRequestContext.NueUserProfile.SingleOrDefault(b => b.Email == toBeAdded[i].email);
                        if (result == null)
                        {
                            NueUserProfile nueUserProfile = new NueUserProfile();
                            nueUserProfile.Email = toBeAdded[i].email;
                            nueUserProfile.FullName = toBeAdded[i].userName;
                            nueUserProfile.FirstName = toBeAdded[i].firstName;
                            nueUserProfile.LastName = toBeAdded[i].lastName;
                            nueUserProfile.EmploymentStatus = 5;
                            nueUserProfile.Active = 1;
                            nueUserProfile.AddedOn = DateTime.UtcNow;
                            nueRequestContext.NueUserProfile.Add(nueUserProfile);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            int returnValue = nueRequestContext.SaveChanges();
            jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            return jsonResponse;
        }

        /*public JsonResponse editDepartmentRequestType(DepartmentRequest departmentRequest)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            if (nueRequestContext.MichaelDepartmentRequestTypeMaster.Where(x => x.RequestTypeName.ToString() == departmentRequest.RequestTypeName.ToString() && x.DepartmentId == departmentRequest.DepartmentId && x.Id != departmentRequest.Id).Count() <= 0)
            {

                var result = nueRequestContext.MichaelDepartmentRequestTypeMaster.SingleOrDefault(b => b.Id == departmentRequest.Id);
                if (result != null)
                {
                    var dateCreated = DateTime.UtcNow;
                    result.RequestTypeName = departmentRequest.RequestTypeName;
                    result.RequestTypeDescription = departmentRequest.RequestTypeDescription;
                    result.Active = departmentRequest.Active;
                    result.ModifiedOn = dateCreated;
                    int returnValue = nueRequestContext.SaveChanges();
                    if (returnValue > 0)
                    {
                        jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                    }
                    else
                    {
                        jsonResponse = new JsonResponse("Failed", "An error occerd.");
                    }
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "Unable to locate requested information");
                }
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department name already in use.");

            }
            return jsonResponse;
        }*/

        /*public JsonResponse addNewDepartmentRequestType(DepartmentRequest departmentRequest)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            if (nueRequestContext.MichaelDepartmentRequestTypeMaster.Where(x => x.RequestTypeName.ToString() == departmentRequest.RequestTypeName.ToString() && x.DepartmentId == departmentRequest.DepartmentId).Count() <= 0)
            {
                var dateCreated = DateTime.UtcNow;
                MichaelDepartmentRequestTypeMaster michaelDepartmentRequestTypeMaster = new MichaelDepartmentRequestTypeMaster();
                michaelDepartmentRequestTypeMaster.DepartmentId = departmentRequest.DepartmentId;
                michaelDepartmentRequestTypeMaster.RequestTypeName = departmentRequest.RequestTypeName;
                michaelDepartmentRequestTypeMaster.RequestTypeDescription = departmentRequest.RequestTypeDescription;
                michaelDepartmentRequestTypeMaster.UserId = departmentRequest.UserId;
                michaelDepartmentRequestTypeMaster.Active = 1;
                michaelDepartmentRequestTypeMaster.AddedOn = dateCreated;
                michaelDepartmentRequestTypeMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelDepartmentRequestTypeMaster.Add(michaelDepartmentRequestTypeMaster);
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "An error occerd.");
                }
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department request type already exist.");

            }
            return jsonResponse;
        }*/

        public List<MichaelDepartmentRequestMaster> GetDepartmentRequestList(MichaelDepartmentRequestMaster michaelDepartmentRequestMaster)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            var department = nueRequestContext.MichaelDepartmentMaster.Where(x => x.Id == michaelDepartmentRequestMaster.DepartmentId);
            if (department != null)
            {
                if (department.SingleOrDefault().Active == 1)
                {
                    return nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.DepartmentId == michaelDepartmentRequestMaster.DepartmentId).ToList<MichaelDepartmentRequestMaster>();
                }
                else
                {
                    return null;
                }

                /*var result = nueRequestContext.MichaelDepartmentRequestMaster.SingleOrDefault(b => b.Id == michaelDepartmentRequestMaster.DepartmentId);
                if (result.Active == 1)
                {
                    result.Active = 0;
                }
                else
                {
                    result.Active = 1;
                }
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    return nueRequestContext.MichaelDepartmentMaster.ToList<MichaelDepartmentMaster>();
                }
                else
                {
                    return null;
                }*/
            }
            else
            {
                return null;
            }

        }

        public JsonResponse updateDepartmentRequestType(MichaelDepartmentRequest michaelDepartmentRequest)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            if (nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.Id != michaelDepartmentRequest.Id && x.RequestTypeName.ToString() == michaelDepartmentRequest.RequestTypeName.ToString() && x.DepartmentId == int.Parse(michaelDepartmentRequest.DepartmentId)).Count() <= 0)
            {

                var result = nueRequestContext.MichaelDepartmentRequestMaster.SingleOrDefault(x => x.Id == michaelDepartmentRequest.Id);
                if (result != null)
                {

                    var baseStage = nueRequestContext.MichaelRequestStageBase.Where(x => x.StageType == "Feedback Given").SingleOrDefault();

                    var dateUpdated = DateTime.UtcNow;

                    result.RequestTypeName = michaelDepartmentRequest.RequestTypeName;
                    result.RequestTypeDescription = michaelDepartmentRequest.RequestTypeDescription;
                    result.RequestPriorityId = michaelDepartmentRequest.RequestPriorityId;
                    result.Active = michaelDepartmentRequest.Active;
                    result.ModifiedOn = dateUpdated;
                    int updatedValue = nueRequestContext.SaveChanges();
                    if (updatedValue > 0)
                    {
                        foreach (var item in michaelDepartmentRequest.EscalationMapper)
                        {
                            var slaBaseObject = nueRequestContext.MichaelEscalationBase.Where(x => x.EscalationLevel == "EL" + item.Level).First();
                            var requestL1SLa = nueRequestContext.MichaelRequestEscalationMapper.Where(x => x.DepartmentId == result.DepartmentId && x.DepartmentRequestId == result.Id && x.EscalationBaseId == slaBaseObject.Id).SingleOrDefault();
                            requestL1SLa.MaxSla = item.MaxSla;
                            nueRequestContext.SaveChanges();

                            var salSloatUserList = item.Assignees;

                            //find new people for sla slot
                            var asigneeList = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id);
                            foreach (var itemAssignee in salSloatUserList)
                            {
                                var matched = asigneeList.Where(x => x.UserId == int.Parse(itemAssignee.value));
                                if(matched == null || matched.FirstOrDefault() == null)
                                {
                                    MichaelRequestEscalationUserBaseMapper michaelRequestEscalationUserBaseMapper = new MichaelRequestEscalationUserBaseMapper();
                                    michaelRequestEscalationUserBaseMapper.RequestEscalationMapperId = requestL1SLa.Id;
                                    michaelRequestEscalationUserBaseMapper.UserId = int.Parse(itemAssignee.value);
                                    michaelRequestEscalationUserBaseMapper.Active = 1;
                                    michaelRequestEscalationUserBaseMapper.AddedOn = dateUpdated;
                                    michaelRequestEscalationUserBaseMapper.ModifiedOn = dateUpdated;
                                    nueRequestContext.MichaelRequestEscalationUserBaseMapper.Add(michaelRequestEscalationUserBaseMapper);
                                    nueRequestContext.SaveChanges();
                                }
                                else
                                {
                                    if(matched.FirstOrDefault().Active == 0)
                                    {
                                        var asigneeReactivate = nueRequestContext.MichaelRequestEscalationUserBaseMapper.SingleOrDefault(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.UserId == int.Parse(itemAssignee.value));
                                        asigneeReactivate.Active = 1;
                                        nueRequestContext.SaveChanges();
                                    }
                                }
                            }

                            /*List<int> deactivateUserIds = new List<int>();
                            var asigneePreRmList = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.Active == 1);
                            foreach (var itemAssignee in asigneePreRmList)
                            {
                                if (salSloatUserList.Where(x => int.Parse(x.value) == itemAssignee.UserId).Count() <= 0)
                                {
                                    deactivateUserIds.Add((int)itemAssignee.UserId);
                                }
                            }

                            if(deactivateUserIds.Count > 0)
                            {
                                foreach (var revabaleUserId in deactivateUserIds)
                                {
                                    var asigneeReactivate = nueRequestContext.MichaelRequestEscalationUserBaseMapper.SingleOrDefault(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.UserId == revabaleUserId);
                                    asigneeReactivate.Active = 0;
                                    nueRequestContext.SaveChanges();

                                    var slaAsigneesQ = nueRequestContext.MichaelRequestEscalationAccessLogs.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.UserId == revabaleUserId);
                                    if(slaAsigneesQ != null && slaAsigneesQ.Count() > 0)
                                    {
                                        var slaAsignees = slaAsigneesQ.ToList();
                                        for (int i = 0; i < slaAsignees.Count(); i++)
                                        {
                                            var slaAsignee = slaAsignees[i];
                                            slaAsignee.Active = 0;
                                            nueRequestContext.SaveChanges();

                                            var requestIdTemp = slaAsignee.RequestId;

                                            try
                                            {
                                                var slaAsigneeDurationLog = nueRequestContext.MichaelRequestEscalationDurationLogs.FirstOrDefault(x => x.RequestId == requestIdTemp && x.RequestEscalationMapperId == requestL1SLa.Id && x.UserId == revabaleUserId && x.Duration == -1);
                                                if (slaAsigneeDurationLog != null)
                                                {
                                                    var duration = slaAsigneeDurationLog.Duration;
                                                    TimeSpan diff = dateUpdated - slaAsigneeDurationLog.AddedOn;
                                                    slaAsigneeDurationLog.Duration = Convert.ToInt32(diff.TotalHours);
                                                    slaAsigneeDurationLog.AddedOn = dateUpdated;
                                                    nueRequestContext.SaveChanges();
                                                }
                                            }
                                            catch (Exception e1)
                                            {
                                                
                                            }
                                            
                                            

                                            //find new user id for replcement
                                            var activeUsers = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.Active == 1);
                                            var replacementUserid = activeUsers.Random();

                                            //update to new user
                                            MichaelRequestEscalationAccessLogs michaelRequestEscalationAccessLogs = new MichaelRequestEscalationAccessLogs();
                                            michaelRequestEscalationAccessLogs.RequestId = requestIdTemp;
                                            michaelRequestEscalationAccessLogs.UserId = slaAsignee.UserId;
                                            michaelRequestEscalationAccessLogs.Active = 1;
                                            michaelRequestEscalationAccessLogs.RequestEscalationMapperId = requestL1SLa.Id;
                                            michaelRequestEscalationAccessLogs.RequestEscalationUserId = replacementUserid.Id;
                                            michaelRequestEscalationAccessLogs.AddedOn = dateUpdated;
                                            nueRequestContext.MichaelRequestEscalationAccessLogs.Add(michaelRequestEscalationAccessLogs);


                                            MichaelRequestEscalationDurationLogs michaelRequestEscalationDurationLogs = new MichaelRequestEscalationDurationLogs();
                                            michaelRequestEscalationDurationLogs.RequestId = requestIdTemp;
                                            michaelRequestEscalationDurationLogs.Duration = -1;
                                            michaelRequestEscalationDurationLogs.UserId = slaAsignee.UserId;
                                            michaelRequestEscalationDurationLogs.RequestEscalationMapperId = requestL1SLa.Id;
                                            michaelRequestEscalationDurationLogs.RequestEscalationUserId = replacementUserid.Id;
                                            michaelRequestEscalationDurationLogs.AddedOn = dateUpdated;
                                            nueRequestContext.MichaelRequestEscalationDurationLogs.Add(michaelRequestEscalationDurationLogs);

                                            nueRequestContext.SaveChanges();

                                            //update request master if needed
                                            var requestMaster = nueRequestContext.MichaelRequestMaster.FirstOrDefault(x => x.Id == requestIdTemp && x.RequestEscalationMapperId == requestL1SLa.Id && x.RequestStageBaseId != baseStage.Id);
                                            if (requestMaster != null)
                                            {
                                                requestMaster.RequestEscalationUserId = replacementUserid.Id;
                                                nueRequestContext.SaveChanges();
                                            }


                                        }
                                    }
                                    
                                }
                            }*/
                        }

                        jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                    }
                    else
                    {
                        jsonResponse = new JsonResponse("Failed", "An error occerd.");
                    }
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "An error occerd.");
                }
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department request type already in use.");

            }
            return jsonResponse;
        }
        
        public JsonResponse addNewDepartmentRequestType(MichaelDepartmentRequest michaelDepartmentRequest)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            if (nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.RequestTypeName.ToString() == michaelDepartmentRequest.RequestTypeName.ToString() && x.DepartmentId == int.Parse(michaelDepartmentRequest.DepartmentId)).Count() <= 0)
            {
                var dateCreated = DateTime.UtcNow;
                MichaelDepartmentRequestMaster michaelDepartmentRequestMaster = new MichaelDepartmentRequestMaster();
                michaelDepartmentRequestMaster.DepartmentId = int.Parse(michaelDepartmentRequest.DepartmentId);
                michaelDepartmentRequestMaster.RequestTypeName = michaelDepartmentRequest.RequestTypeName;
                michaelDepartmentRequestMaster.RequestTypeDescription = michaelDepartmentRequest.RequestTypeDescription;
                michaelDepartmentRequestMaster.RequestPriorityId = michaelDepartmentRequest.RequestPriorityId;
                michaelDepartmentRequestMaster.UserId = michaelDepartmentRequest.UserId;
                michaelDepartmentRequestMaster.Active = michaelDepartmentRequest.Active;
                michaelDepartmentRequestMaster.AddedOn = dateCreated;
                michaelDepartmentRequestMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelDepartmentRequestMaster.Add(michaelDepartmentRequestMaster);
                int returnValue = nueRequestContext.SaveChanges();
                if (returnValue > 0)
                {
                    int departmentRequestId = michaelDepartmentRequestMaster.Id;
                    foreach (var item in michaelDepartmentRequest.EscalationMapper)
                    {
                        MichaelRequestEscalationMapper michaelRequestEscalationMapper = new MichaelRequestEscalationMapper();
                        michaelRequestEscalationMapper.Level = item.Level;
                        michaelRequestEscalationMapper.Active = item.Active;
                        michaelRequestEscalationMapper.MaxSla = item.MaxSla;
                        michaelRequestEscalationMapper.DepartmentId = michaelDepartmentRequestMaster.DepartmentId;
                        michaelRequestEscalationMapper.DepartmentRequestId = departmentRequestId;
                        michaelRequestEscalationMapper.EscalationBaseId = nueRequestContext.MichaelEscalationBase.Where(x => x.EscalationLevel == "EL" + item.Level).First().Id;
                        michaelRequestEscalationMapper.AddedOn = dateCreated;
                        michaelRequestEscalationMapper.ModifiedOn = dateCreated;
                        nueRequestContext.MichaelRequestEscalationMapper.Add(michaelRequestEscalationMapper);
                        nueRequestContext.SaveChanges();
                        int requestEscalationMapperId  = michaelRequestEscalationMapper.Id;
                        foreach (var itemAssignee in item.Assignees)
                        {
                            MichaelRequestEscalationUserBaseMapper michaelRequestEscalationUserBaseMapper = new MichaelRequestEscalationUserBaseMapper();
                            michaelRequestEscalationUserBaseMapper.RequestEscalationMapperId = requestEscalationMapperId;
                            michaelRequestEscalationUserBaseMapper.UserId = int.Parse(itemAssignee.value);
                            michaelRequestEscalationUserBaseMapper.Active = 1;
                            michaelRequestEscalationUserBaseMapper.AddedOn = dateCreated;
                            michaelRequestEscalationUserBaseMapper.ModifiedOn = dateCreated;
                            nueRequestContext.MichaelRequestEscalationUserBaseMapper.Add(michaelRequestEscalationUserBaseMapper);
                        }
                        nueRequestContext.SaveChanges();
                    }
                    jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "An error occerd.");
                }
            }
            else
            {
                jsonResponse = new JsonResponse("Failed", "Department request type already exist.");

            }
            return jsonResponse;
        }

        public JsonResponse GetMichaelRequestBotPrevData(MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var requestObj = nueRequestContext.MichaelRequestMaster.Where(x => x.RequestId == michaelRequestViewerData.RequestId);
                if (requestObj != null && requestObj.FirstOrDefault() != null)
                {
                    var request = requestObj.FirstOrDefault();

                    michaelRequestViewerData.IsPermitted = 0;
                    michaelRequestViewerData.Id = request.Id;
                    michaelRequestViewerData.RequestId = request.RequestId;
                    michaelRequestViewerData.RequestType = nueRequestContext.MichaelDepartmentRequestMaster.SingleOrDefault(x => x.Id == request.DepartmentRequestId && x.DepartmentId == request.DepartmentId).RequestTypeName;
                    michaelRequestViewerData.IsApprovalProcess = (int)request.IsApprovalProcess;
                    michaelRequestViewerData.IsApprovalProcessComplated = (int)request.IsApprovalProcessComplted;

                    int isUserHasAccess = 0;

                    MichaelRequestUserAccess michaelRequestUserAccess = new MichaelRequestUserAccess();

                    //check user is assignee
                    var MichaelRequestEscalationUserBaseMapper = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.Id == request.RequestEscalationUserId && x.UserId == michaelRequestViewerData.UserId && x.RequestEscalationMapperId == request.RequestEscalationMapperId);
                    if (MichaelRequestEscalationUserBaseMapper != null && MichaelRequestEscalationUserBaseMapper.FirstOrDefault() != null)
                    {
                        var requestSLAUser = MichaelRequestEscalationUserBaseMapper.FirstOrDefault();
                        var userAccess = nueRequestContext.MichaelRequestEscalationAccessLogs.Where(x => x.RequestEscalationUserId == requestSLAUser.Id && x.Active == 1 && x.RequestEscalationMapperId == request.RequestEscalationMapperId && x.RequestId == request.Id);
                        if (userAccess != null && userAccess.FirstOrDefault() != null)
                        {
                            michaelRequestUserAccess.IsAssignee = 1;
                            isUserHasAccess = 1;
                        }
                    }

                    //check user is owner and approver
                    var ownerAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "owner");
                    var approverAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "approver");

                    var ownerAccess = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == ownerAccessType.Id && x.UserId == michaelRequestViewerData.UserId);
                    var approverAccess = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id && x.UserId == michaelRequestViewerData.UserId);

                    if (ownerAccess != null && ownerAccess.FirstOrDefault() != null)
                    {
                        michaelRequestUserAccess.IsOwner = 1;
                        isUserHasAccess = 1;
                    }

                    if (approverAccess != null && approverAccess.FirstOrDefault() != null)
                    {
                        michaelRequestUserAccess.IsApprover = 1;
                        isUserHasAccess = 1;
                    }

                    if (isUserHasAccess == 1)
                    {
                        michaelRequestViewerData.IsPermitted = 1;
                        michaelRequestViewerData.MichaelRequestUserAcces = michaelRequestUserAccess;

                        var ownerUserBase = nueRequestContext.MichaelRequestAccessMapper.SingleOrDefault(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == ownerAccessType.Id);
                        var owner = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == ownerUserBase.UserId);

                        var requestStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.Id == request.RequestStageBaseId);

                        //load sidebar data
                        List<SideBarItem> sideBarItems = new List<SideBarItem>();
                        //var requestPayloads = nueRequestContext.MichaelRequestPayloadMaster.Where(x => x.RequestId == request.Id);
                        //foreach (var item in requestPayloads)
                        //{
                        //    sideBarItems.Add(new SideBarItem(item.FieldName, item.Payload));
                        //}
                        michaelRequestViewerData.RequestStatus = Utils.FirstCharToUpper(requestStage.StageType);
                        //sideBarItems.Add(new SideBarItem("Status", requestStage.StageType));
                        sideBarItems.Add(new SideBarItem("Created On", request.AddedOn.ToLocalTime().TimeAgo().ToString()));
                        sideBarItems.Add(new SideBarItem("Ticket Creator", owner.FullName));
                        if (request.IsApprovalProcess == 1)
                        {
                            var approverUsersBase = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id);
                            if (approverUsersBase != null && approverUsersBase.FirstOrDefault() != null)
                            {
                                int li = 1;
                                foreach (var item in approverUsersBase)
                                {
                                    var approverUserName = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId).FullName;
                                    var assessMapper = nueRequestContext.MichaelRequestApproverStatusMapper.FirstOrDefault(x => x.RequestId == request.Id && x.RequestAccessId == item.Id && x.UserId == item.UserId);
                                    var approverstage = nueRequestContext.MichaelApproverStatusTypes.FirstOrDefault(x => x.Id == assessMapper.ApproverStatusId);
                                    sideBarItems.Add(new SideBarItem("Ticket Approver(L" + li + ")", approverUserName + "- " + approverstage.Status));
                                    li++;
                                }
                            }
                        }
                        michaelRequestViewerData.SidebarData = sideBarItems;

                        MichaelRequestUserOps MichaelRequestUserOp = new MichaelRequestUserOps();

                        if (requestStage.StageType.ToLower() == "created")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                MichaelRequestUserOp.Approve = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }

                            //MichaelRequestUserOp.Close = 0;
                            //MichaelRequestUserOp.Withdraw = 0;
                            //MichaelRequestUserOp.Approve = 0;
                            //MichaelRequestUserOp.AdminApprove = 0;
                            //MichaelRequestUserOp.AdminOverideApprove = 0;
                        }
                        else if (requestStage.StageType.ToLower() == "assignee accepted")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Close = 1;
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "assignee rejected")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Close = 1;
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "approver accepted")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                //MichaelRequestUserOp.Approve = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    //MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "approver rejected")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                //MichaelRequestUserOp.Approve = 1;
                                MichaelRequestUserOp.ApproveReject = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "withdraw")
                        {

                        }
                        else if (requestStage.StageType.ToLower() == "completed")
                        {

                        }

                        michaelRequestViewerData.MichaelRequestUserOp = MichaelRequestUserOp;

                        jsonResponse = new JsonResponse("Ok", "Data loaded successfully.", michaelRequestViewerData);
                    }
                    else
                    {
                        michaelRequestViewerData.IsPermitted = 0;
                        jsonResponse = new JsonResponse("Failed", "Invalid request.");
                    }

                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "Invalid request");
                }
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse GetMichaelRequestViewerData(MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var requestObj = nueRequestContext.MichaelRequestMaster.Where(x => x.RequestId == michaelRequestViewerData.RequestId);
                if(requestObj != null && requestObj.FirstOrDefault() != null)
                {
                    var request = requestObj.FirstOrDefault();

                    michaelRequestViewerData.IsPermitted = 0;
                    michaelRequestViewerData.Id = request.Id;
                    michaelRequestViewerData.RequestId = request.RequestId;
                    michaelRequestViewerData.RequestType = nueRequestContext.MichaelDepartmentRequestMaster.SingleOrDefault(x => x.Id == request.DepartmentRequestId && x.DepartmentId == request.DepartmentId).RequestTypeName;
                    michaelRequestViewerData.IsApprovalProcess = (int) request.IsApprovalProcess;
                    michaelRequestViewerData.IsApprovalProcessComplated = (int)request.IsApprovalProcessComplted;

                    int isUserHasAccess = 0;

                    MichaelRequestUserAccess michaelRequestUserAccess = new MichaelRequestUserAccess();

                    //check user is assignee
                    var MichaelRequestEscalationUserBaseMapper = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.Id == request.RequestEscalationUserId && x.UserId == michaelRequestViewerData.UserId && x.RequestEscalationMapperId == request.RequestEscalationMapperId);
                    if(MichaelRequestEscalationUserBaseMapper != null && MichaelRequestEscalationUserBaseMapper.FirstOrDefault() != null)
                    {
                        var requestSLAUser = MichaelRequestEscalationUserBaseMapper.FirstOrDefault();
                        var userAccess = nueRequestContext.MichaelRequestEscalationAccessLogs.Where(x => x.RequestEscalationUserId == requestSLAUser.Id && x.Active == 1 && x.RequestEscalationMapperId == request.RequestEscalationMapperId && x.RequestId == request.Id);
                        if(userAccess != null && userAccess.FirstOrDefault() != null)
                        {
                            michaelRequestUserAccess.IsAssignee = 1;
                            isUserHasAccess = 1;
                        }
                    }

                    //check user is owner and approver
                    var ownerAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "owner");
                    var approverAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "approver");

                    var ownerAccess = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == ownerAccessType.Id && x.UserId == michaelRequestViewerData.UserId);
                    var approverAccess = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id && x.UserId == michaelRequestViewerData.UserId);

                    if(ownerAccess != null && ownerAccess.FirstOrDefault() != null)
                    {
                        michaelRequestUserAccess.IsOwner = 1;
                        isUserHasAccess = 1;
                    }

                    if (approverAccess != null && approverAccess.FirstOrDefault() != null)
                    {
                        michaelRequestUserAccess.IsApprover = 1;
                        isUserHasAccess = 1;
                    }

                    if(isUserHasAccess == 1)
                    {
                        michaelRequestViewerData.IsPermitted = 1;
                        michaelRequestViewerData.MichaelRequestUserAcces = michaelRequestUserAccess;

                        var ownerUserBase = nueRequestContext.MichaelRequestAccessMapper.SingleOrDefault(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == ownerAccessType.Id);
                        var owner = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == ownerUserBase.UserId);

                        var requestStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.Id == request.RequestStageBaseId);

                        //load sidebar data
                        List<SideBarItem> sideBarItems = new List<SideBarItem>();
                        var requestPayloads = nueRequestContext.MichaelRequestPayloadMaster.Where(x =>x.RequestId == request.Id);
                        foreach (var item in requestPayloads)
                        {
                            sideBarItems.Add(new SideBarItem(Utils.FirstCharToUpper(item.FieldName), Utils.FirstCharToUpper(item.Payload)));
                        }

                        sideBarItems.Add(new SideBarItem("Status", Utils.FirstCharToUpper(requestStage.StageType)));
                        sideBarItems.Add(new SideBarItem("Created On", request.AddedOn.ToLocalTime().TimeAgo().ToString()));
                        sideBarItems.Add(new SideBarItem("Ticket Creator", owner.FullName));
                        if (request.IsApprovalProcess == 1)
                        {
                            var approverUsersBase = nueRequestContext.MichaelRequestAccessMapper.Where(x => x.RequestId == request.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id);
                            if (approverUsersBase != null && approverUsersBase.FirstOrDefault() != null)
                            {
                                int li = 1;
                                foreach (var item in approverUsersBase)
                                {
                                    var approverUserName = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId).FullName;
                                    var assessMapper = nueRequestContext.MichaelRequestApproverStatusMapper.FirstOrDefault(x => x.RequestId == request.Id && x.RequestAccessId == item.Id && x.UserId == item.UserId);
                                    var approverstage = nueRequestContext.MichaelApproverStatusTypes.FirstOrDefault(x => x.Id == assessMapper.ApproverStatusId);
                                    sideBarItems.Add(new SideBarItem("Ticket Approver(L" + li + ")", approverUserName + "- " + approverstage.Status));
                                    li++;
                                }
                            }
                        }
                        michaelRequestViewerData.SidebarData = sideBarItems;

                        MichaelRequestUserOps MichaelRequestUserOp = new MichaelRequestUserOps();

                        if (requestStage.StageType.ToLower() == "created")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                MichaelRequestUserOp.Approve = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }

                            //MichaelRequestUserOp.Close = 0;
                            //MichaelRequestUserOp.Withdraw = 0;
                            //MichaelRequestUserOp.Approve = 0;
                            //MichaelRequestUserOp.AdminApprove = 0;
                            //MichaelRequestUserOp.AdminOverideApprove = 0;
                        }
                        else if (requestStage.StageType.ToLower() == "assignee accepted")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Close = 1;
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "assignee rejected")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Close = 1;
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "approver accepted")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                //MichaelRequestUserOp.Approve = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    //MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "approver rejected")
                        {
                            MichaelRequestUserOp.Comment = 1;
                            MichaelRequestUserOp.Attach = 1;
                            if (michaelRequestUserAccess.IsOwner == 1)
                            {
                                MichaelRequestUserOp.Withdraw = 1;
                            }
                            if (michaelRequestUserAccess.IsApprover == 1)
                            {
                                //MichaelRequestUserOp.Approve = 1;
                                MichaelRequestUserOp.ApproveReject = 1;
                            }

                            if (michaelRequestUserAccess.IsAssignee == 1)
                            {
                                MichaelRequestUserOp.AdminApprove = 1;

                                if (request.IsApprovalProcessComplted == 0)
                                {
                                    MichaelRequestUserOp.AdminOverideApprove = 1;
                                }
                            }
                        }
                        else if (requestStage.StageType.ToLower() == "withdraw")
                        {

                        }
                        else if (requestStage.StageType.ToLower() == "completed")
                        {

                        }

                        michaelRequestViewerData.MichaelRequestUserOp = MichaelRequestUserOp;

                        jsonResponse = new JsonResponse("Ok", "Data loaded successfully.", michaelRequestViewerData);
                    }
                    else
                    {
                        michaelRequestViewerData.IsPermitted = 0;
                        jsonResponse = new JsonResponse("Failed", "Invalid request.");
                    }
                    
                }
                else
                {
                    jsonResponse = new JsonResponse("Failed", "Invalid request");
                }
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse getMichaelRequestLogs(MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                List<MichaelRequestLogItem> michaelRequestLogItems = new List<MichaelRequestLogItem>();

                var requestLog = nueRequestContext.MichaelRequestLog.Where(x => x.RequestId == michaelRequestViewerData.Id);
                foreach (var item in requestLog)
                {
                    var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.Id == item.RequestLogTypeId);
                    if(attachFileActivity.LogType.ToLower() == "commented")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "commented";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "withdraw")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "withdraw";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "attachedfile")
                    {
                        var attachment = nueRequestContext.MichaelRequestAttachmentLog.FirstOrDefault(x => x.Id == int.Parse(item.Payload));
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "WorkIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = attachment.FileName+"."+attachment.FileExt;
                        michaelRequestLogItem.PayloadType = "attachedfile";
                        michaelRequestLogItem.AttachmentId = item.Id;
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "approved")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "approved";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "rejected")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "rejected";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "approverapproved")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "approverapproved";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "approverrejected")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "approverrejected";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }
                    else if (attachFileActivity.LogType.ToLower() == "completed")
                    {
                        var user = nueRequestContext.NueUserProfile.FirstOrDefault(x => x.Id == item.UserId);
                        MichaelRequestLogItem michaelRequestLogItem = new MichaelRequestLogItem();
                        michaelRequestLogItem.DateOn = item.AddedOn.ToLocalTime().TimeAgo();
                        michaelRequestLogItem.Icon = "SchoolIcon";
                        michaelRequestLogItem.UserName = user.FullName;
                        michaelRequestLogItem.UserEmail = user.Email;
                        michaelRequestLogItem.UserId = user.Id;
                        michaelRequestLogItem.Payload = Utils.FirstCharToUpper(item.Payload);
                        michaelRequestLogItem.PayloadType = "completed";
                        michaelRequestLogItems.Add(michaelRequestLogItem);
                    }

                }

                jsonResponse = new JsonResponse("Ok", "Data loaded successfully.", michaelRequestLogItems);
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse addMichaelRequestFeedback(MichaelRequestLog michaelRequestLog, MichaelRequestViewerData michaelRequestViewerData, MichaelRequestFeedbackRequest michaelRequestFeedbackRequest)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Completed");
                var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                request.RequestStageBaseId = requestCompltedStage.Id;
                request.ModifiedOn = dateCreated;

                MichaelRequestFeedbackMaster michaelRequestFeedbackMaster = new MichaelRequestFeedbackMaster();
                michaelRequestFeedbackMaster.RequestId = michaelRequestViewerData.Id;
                michaelRequestFeedbackMaster.Ratting = michaelRequestFeedbackRequest.Ratting;
                michaelRequestFeedbackMaster.FeedbackComment = michaelRequestLog.Payload;
                michaelRequestFeedbackMaster.UserId = michaelRequestLog.UserId;
                michaelRequestFeedbackMaster.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestFeedbackMaster.Add(michaelRequestFeedbackMaster);

                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "completed");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse adminRejectMichaelRequest(MichaelRequestLog michaelRequestLog, MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Assignee Rejected");
                var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                request.RequestStageBaseId = requestCompltedStage.Id;
                request.ModifiedOn = dateCreated;

                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "rejected");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse adminApproveMichaelRequest(MichaelRequestLog michaelRequestLog, MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Assignee Accepted");
                var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                request.RequestStageBaseId = requestCompltedStage.Id;
                request.ModifiedOn = dateCreated;

                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "approved");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse rejectMichaelRequest(MichaelRequestLog michaelRequestLog, MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var approverAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "approver");
                var userAccess = nueRequestContext.MichaelRequestAccessMapper.FirstOrDefault(x => x.RequestId == michaelRequestViewerData.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id && x.UserId == michaelRequestViewerData.UserId);
                var approvedPendingStatusBase = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "pending").SingleOrDefault();
                var rejectStatusBase = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "rejected").SingleOrDefault();


                var approverAccess = nueRequestContext.MichaelRequestApproverStatusMapper.FirstOrDefault(x => x.RequestId == michaelRequestViewerData.Id
                                                                                    && x.RequestAccessTypesId == approverAccessType.Id
                                                                                    && x.UserId == michaelRequestViewerData.UserId
                                                                                    && x.RequestAccessId == userAccess.Id
                                                                                    && x.ApproverStatusId == approvedPendingStatusBase.Id);

                if (approverAccess != null)
                {
                    approverAccess.ApproverStatusId = rejectStatusBase.Id;
                    approverAccess.AddedOn = dateCreated;
                    nueRequestContext.SaveChanges();
                }
                

                //change request status to approver rejected
                var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Approver Rejected");
                var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                request.RequestStageBaseId = requestCompltedStage.Id;
                request.ModifiedOn = dateCreated;

                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "approverrejected");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse approveMichaelRequest(MichaelRequestLog michaelRequestLog, MichaelRequestViewerData michaelRequestViewerData)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var approverAccessType = nueRequestContext.MichaelRequestAccessTypes.SingleOrDefault(x => x.AccessTypes == "approver");
                var userAccess = nueRequestContext.MichaelRequestAccessMapper.FirstOrDefault(x => x.RequestId == michaelRequestViewerData.Id && x.Active == 1 && x.RequestAccessTypesId == approverAccessType.Id && x.UserId == michaelRequestViewerData.UserId);
                var approvedPendingStatusBase = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "pending").SingleOrDefault();
                var approvedStatusBase = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "approved").SingleOrDefault();


                var approverAccess = nueRequestContext.MichaelRequestApproverStatusMapper.FirstOrDefault(x => x.RequestId == michaelRequestViewerData.Id 
                                                                                    && x.RequestAccessTypesId == approverAccessType.Id 
                                                                                    && x.UserId == michaelRequestViewerData.UserId
                                                                                    && x.RequestAccessId == userAccess.Id
                                                                                    && x.ApproverStatusId == approvedPendingStatusBase.Id);

                if(approverAccess != null)
                {
                    approverAccess.ApproverStatusId = approvedStatusBase.Id;
                    approverAccess.AddedOn = dateCreated;
                    nueRequestContext.SaveChanges();
                }

                

                //check all approvers have approved for the request
                var requestApproverStatus = nueRequestContext.MichaelRequestApproverStatusMapper.Where(x => x.RequestId == michaelRequestViewerData.Id
                                                                                                        && x.RequestAccessTypesId == approverAccessType.Id
                                                                                                        && x.ApproverStatusId != approvedStatusBase.Id);
                if(requestApproverStatus == null || requestApproverStatus.Count() <= 0)
                {
                    var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Approver Accepted");
                    var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                    request.RequestStageBaseId = requestCompltedStage.Id;
                    request.IsApprovalProcessComplted = 1;
                    request.ModifiedOn = dateCreated;
                }

                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "approverapproved");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse withdrawMichaelRequest(MichaelRequestLog michaelRequestLog)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;

                var requestCompltedStage = nueRequestContext.MichaelRequestStageBase.SingleOrDefault(x => x.StageType == "Withdraw");
                var request = nueRequestContext.MichaelRequestMaster.SingleOrDefault(x => x.Id == michaelRequestLog.RequestId);
                request.RequestStageBaseId = requestCompltedStage.Id;
                request.ModifiedOn = dateCreated;
                
                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "withdraw");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse addNewMichaelRequestComment(MichaelRequestLog michaelRequestLog)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;
                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "commented");
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse getMichaelRequestAttachmentItem(MichaelRequestLog michaelRequestLog)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var requestLogs = nueRequestContext.MichaelRequestLog.Where(x => x.UserId == michaelRequestLog.UserId && x.Id == michaelRequestLog.Id);
                if(requestLogs != null && requestLogs.FirstOrDefault() != null)
                {
                    var requestLog = requestLogs.FirstOrDefault();
                    var attchmentLogs = nueRequestContext.MichaelRequestAttachmentLog.Where(x => x.Id == int.Parse(requestLog.Payload));
                    if(attchmentLogs != null && attchmentLogs.FirstOrDefault() != null)
                    {
                        jsonResponse = new JsonResponse("Ok", "Data loaded successfully.", attchmentLogs.FirstOrDefault());
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
                //var dateCreated = DateTime.UtcNow;
                //michaelRequestAttachmentLog.AddedOn = dateCreated;
                //michaelRequestAttachmentLog.ModifiedOn = dateCreated;
                //nueRequestContext.MichaelRequestAttachmentLog.Add(michaelRequestAttachmentLog);
                //nueRequestContext.SaveChanges();
                //var fileAtachmentId = michaelRequestAttachmentLog.Id;
                //var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "attachedfile");
                //MichaelRequestLog michaelRequestLog = new MichaelRequestLog();
                //michaelRequestLog.RequestId = michaelRequestAttachmentLog.RequestId;
                //michaelRequestLog.UserId = michaelRequestAttachmentLog.UserId;
                //michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                //michaelRequestLog.AddedOn = dateCreated;
                //michaelRequestLog.Payload = fileAtachmentId.ToString();
                //nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                //nueRequestContext.SaveChanges();
                
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse addNewMichaelRequestAttchment(MichaelRequestAttachmentLog michaelRequestAttachmentLog, MichaelRequestAttachment michaelRequestAttachment)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;
                michaelRequestAttachmentLog.AddedOn = dateCreated;
                michaelRequestAttachmentLog.ModifiedOn = dateCreated;
                nueRequestContext.MichaelRequestAttachmentLog.Add(michaelRequestAttachmentLog);
                nueRequestContext.SaveChanges();
                var fileAtachmentId = michaelRequestAttachmentLog.Id;
                var attachFileActivity = nueRequestContext.MichaelRequestLogTypes.FirstOrDefault(x => x.LogType == "attachedfile");
                MichaelRequestLog michaelRequestLog = new MichaelRequestLog();
                michaelRequestLog.RequestId = michaelRequestAttachmentLog.RequestId;
                michaelRequestLog.UserId = michaelRequestAttachmentLog.UserId;
                michaelRequestLog.RequestLogTypeId = attachFileActivity.Id;
                michaelRequestLog.AddedOn = dateCreated;
                michaelRequestLog.Payload = fileAtachmentId.ToString();
                nueRequestContext.MichaelRequestLog.Add(michaelRequestLog);
                nueRequestContext.SaveChanges();
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.");
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse addNewMichaelRequest(DepartmentRequestSaveTemplate departmentRequestSaveTemplate, NueUserProfile nueUserProfile, string contentRootPath)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;
                var dbField = departmentRequestSaveTemplate.DataFields;
                var baseRequest = departmentRequestSaveTemplate.michaelDepartmentRequestTypeMaster;

                var dataFields = dbField.Where(x => !x.IsApproverField).ToList<DepartmentRequestSaveFormItem>();
                var approverFields = dbField.Where(x => x.IsApproverField).ToList<DepartmentRequestSaveFormItem>();

                if (dataFields == null || dataFields.Count <= 0) {
                    throw new Exception("Invalid request");
                }

                var baseStage = nueRequestContext.MichaelRequestStageBase.Where(x => x.StageType == "Created").SingleOrDefault();

                var slaBaseObject = nueRequestContext.MichaelEscalationBase.Where(x => x.EscalationLevel == "EL0").First();
                var requestL1SLa = nueRequestContext.MichaelRequestEscalationMapper.Where(x => x.DepartmentId == baseRequest.DepartmentId && x.DepartmentRequestId == baseRequest.Id && x.EscalationBaseId == slaBaseObject.Id).SingleOrDefault();


                var activeUsers = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.Active == 1);
                var slaUser = activeUsers.Random();


                var requestStrId = new Modal.Utils().getUniqRequestId(contentRootPath);
                DAL.MichaelRequestMaster michaelRequestMaster = new DAL.MichaelRequestMaster();
                michaelRequestMaster.RequestId = requestStrId;
                michaelRequestMaster.DepartmentId = baseRequest.DepartmentId;
                michaelRequestMaster.DepartmentRequestId = baseRequest.Id;
                michaelRequestMaster.UserId = nueUserProfile.Id;
                michaelRequestMaster.RequestStageBaseId = baseStage.Id;
                michaelRequestMaster.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestMaster.RequestEscalationUserId = 1; //slaUser.Id;
                michaelRequestMaster.RequestPriorityId = nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.DepartmentId == baseRequest.DepartmentId && x.Id == baseRequest.Id).SingleOrDefault().RequestPriorityId;
                michaelRequestMaster.IsApprovalProcess = (approverFields != null && approverFields.Count > 0) ? 1 : 0;
                michaelRequestMaster.IsApprovalProcessComplted = 0;
                michaelRequestMaster.CurrentSla = requestL1SLa.MaxSla;
                michaelRequestMaster.AddedOn = dateCreated;
                michaelRequestMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelRequestMaster.Add(michaelRequestMaster);
                nueRequestContext.SaveChanges();

                var requestId = michaelRequestMaster.Id;

                foreach (var item in dataFields)
                {
                    MichaelRequestPayloadMaster michaelRequestPayloadMaster = new MichaelRequestPayloadMaster();
                    michaelRequestPayloadMaster.RequestId = requestId;
                    michaelRequestPayloadMaster.FieldName = item.Name;
                    michaelRequestPayloadMaster.PayloadDataType = nueRequestContext.MichaelPayloadDataType.Where(x => x.DataType == item.Fieldtype).SingleOrDefault().Id;
                    michaelRequestPayloadMaster.Payload = item.Fieldvalue;
                    michaelRequestPayloadMaster.AddedOn = dateCreated;
                    michaelRequestPayloadMaster.ModifiedOn = dateCreated;
                    nueRequestContext.MichaelRequestPayloadMaster.Add(michaelRequestPayloadMaster);
                }
                //nueRequestContext.SaveChanges();

                MichaelRequestEscalationAccessLogs michaelRequestEscalationAccessLogs = new MichaelRequestEscalationAccessLogs();
                michaelRequestEscalationAccessLogs.RequestId = requestId;
                michaelRequestEscalationAccessLogs.UserId = nueUserProfile.Id;
                michaelRequestEscalationAccessLogs.Active = 1;
                michaelRequestEscalationAccessLogs.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestEscalationAccessLogs.RequestEscalationUserId = 1;//slaUser.Id;
                michaelRequestEscalationAccessLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestEscalationAccessLogs.Add(michaelRequestEscalationAccessLogs);
                //nueRequestContext.SaveChanges();

                MichaelRequestEscalationDurationLogs michaelRequestEscalationDurationLogs = new MichaelRequestEscalationDurationLogs();
                michaelRequestEscalationDurationLogs.RequestId = requestId;
                michaelRequestEscalationDurationLogs.Duration = -1;
                michaelRequestEscalationDurationLogs.UserId = 1;// slaUser.Id;
                michaelRequestEscalationDurationLogs.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestEscalationDurationLogs.RequestEscalationUserId = 1;// slaUser.Id;
                michaelRequestEscalationDurationLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestEscalationDurationLogs.Add(michaelRequestEscalationDurationLogs);
                //nueRequestContext.SaveChanges();

                MichaelRequestStageLogs michaelRequestStageLogs = new MichaelRequestStageLogs();
                michaelRequestStageLogs.RequestId = requestId;
                michaelRequestStageLogs.RequestStageBaseId = baseStage.Id;
                michaelRequestStageLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestStageLogs.Add(michaelRequestStageLogs);
                nueRequestContext.SaveChanges();

                MichaelRequestAccessMapper michaelRequestOwnerAccessMapper = new MichaelRequestAccessMapper();
                michaelRequestOwnerAccessMapper.RequestId = requestId;
                michaelRequestOwnerAccessMapper.UserId = nueUserProfile.Id;
                michaelRequestOwnerAccessMapper.RequestAccessTypesId = nueRequestContext.MichaelRequestAccessTypes.Where(x => x.AccessTypes == "owner").SingleOrDefault().Id;
                michaelRequestOwnerAccessMapper.Active = 1;
                michaelRequestOwnerAccessMapper.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestAccessMapper.Add(michaelRequestOwnerAccessMapper);
                nueRequestContext.SaveChanges();

                if (approverFields != null && approverFields.Count > 0)
                {
                    foreach (var item in approverFields)
                    {

                        MichaelRequestAccessMapper michaelRequestAccessMapper = new MichaelRequestAccessMapper();
                        michaelRequestAccessMapper.RequestId = requestId;
                        michaelRequestAccessMapper.UserId = 1;// int.Parse(item.Fieldvalue);
                        michaelRequestAccessMapper.RequestAccessTypesId = nueRequestContext.MichaelRequestAccessTypes.Where(x => x.AccessTypes == "approver").SingleOrDefault().Id;
                        michaelRequestAccessMapper.Active = 1;
                        michaelRequestAccessMapper.AddedOn = dateCreated;
                        nueRequestContext.MichaelRequestAccessMapper.Add(michaelRequestAccessMapper);
                        nueRequestContext.SaveChanges();

                        MichaelRequestApproverStatusMapper michaelRequestApproverStatusMapper = new MichaelRequestApproverStatusMapper();
                        michaelRequestApproverStatusMapper.RequestId = requestId;
                        michaelRequestApproverStatusMapper.UserId = 1;//int.Parse(item.Fieldvalue);
                        michaelRequestApproverStatusMapper.RequestAccessId = michaelRequestAccessMapper.Id;
                        michaelRequestApproverStatusMapper.RequestAccessTypesId = michaelRequestAccessMapper.RequestAccessTypesId;
                        michaelRequestApproverStatusMapper.ApproverStatusId = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "pending").SingleOrDefault().Id;
                        michaelRequestApproverStatusMapper.Active = 1;
                        michaelRequestApproverStatusMapper.AddedOn = dateCreated;
                        nueRequestContext.MichaelRequestApproverStatusMapper.Add(michaelRequestApproverStatusMapper);
                        nueRequestContext.SaveChanges();
                    }
                }
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.", requestStrId);
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public JsonResponse addNewMichaelRequestX(DepartmentRequestSaveTemplate departmentRequestSaveTemplate, NueUserProfile nueUserProfile, string contentRootPath)
        {
            JsonResponse jsonResponse = new JsonResponse();
            NueRequestContext nueRequestContext = new NueRequestContext();
            try
            {
                var dateCreated = DateTime.UtcNow;
                var dbField = departmentRequestSaveTemplate.DataFields;
                var baseRequest = departmentRequestSaveTemplate.michaelDepartmentRequestTypeMaster;

                var dataFields = dbField.Where(x => !x.IsApproverField).ToList<DepartmentRequestSaveFormItem>();
                var approverFields = dbField.Where(x => x.IsApproverField).ToList<DepartmentRequestSaveFormItem>();

                if (dataFields == null || dataFields.Count <= 0)
                {
                    throw new Exception("Invalid request");
                }

                var baseStage = nueRequestContext.MichaelRequestStageBase.Where(x => x.StageType == "Created").SingleOrDefault();

                var slaBaseObject = nueRequestContext.MichaelEscalationBase.Where(x => x.EscalationLevel == "EL0").First();
                var requestL1SLa = nueRequestContext.MichaelRequestEscalationMapper.Where(x => x.DepartmentId == baseRequest.DepartmentId && x.DepartmentRequestId == baseRequest.Id && x.EscalationBaseId == slaBaseObject.Id).SingleOrDefault();


                var activeUsers = nueRequestContext.MichaelRequestEscalationUserBaseMapper.Where(x => x.RequestEscalationMapperId == requestL1SLa.Id && x.Active == 1);
                var slaUser = activeUsers.Random();


                var requestStrId = new Modal.Utils().getUniqRequestId(contentRootPath);
                DAL.MichaelRequestMaster michaelRequestMaster = new DAL.MichaelRequestMaster();
                michaelRequestMaster.RequestId = requestStrId;
                michaelRequestMaster.DepartmentId = baseRequest.DepartmentId;
                michaelRequestMaster.DepartmentRequestId = baseRequest.Id;
                michaelRequestMaster.UserId = nueUserProfile.Id;
                michaelRequestMaster.RequestStageBaseId = baseStage.Id;
                michaelRequestMaster.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestMaster.RequestEscalationUserId = slaUser.Id;
                michaelRequestMaster.RequestPriorityId = nueRequestContext.MichaelDepartmentRequestMaster.Where(x => x.DepartmentId == baseRequest.DepartmentId && x.Id == baseRequest.Id).SingleOrDefault().RequestPriorityId;
                michaelRequestMaster.IsApprovalProcess = (approverFields != null && approverFields.Count > 0) ? 1 : 0;
                michaelRequestMaster.IsApprovalProcessComplted = 0;
                michaelRequestMaster.CurrentSla = requestL1SLa.MaxSla;
                michaelRequestMaster.AddedOn = dateCreated;
                michaelRequestMaster.ModifiedOn = dateCreated;
                nueRequestContext.MichaelRequestMaster.Add(michaelRequestMaster);
                nueRequestContext.SaveChanges();

                var requestId = michaelRequestMaster.Id;

                foreach (var item in dataFields)
                {
                    MichaelRequestPayloadMaster michaelRequestPayloadMaster = new MichaelRequestPayloadMaster();
                    michaelRequestPayloadMaster.RequestId = requestId;
                    michaelRequestPayloadMaster.FieldName = item.Name;
                    michaelRequestPayloadMaster.PayloadDataType = nueRequestContext.MichaelPayloadDataType.Where(x => x.DataType == item.Fieldtype).SingleOrDefault().Id;
                    michaelRequestPayloadMaster.Payload = item.Fieldvalue;
                    michaelRequestPayloadMaster.AddedOn = dateCreated;
                    michaelRequestPayloadMaster.ModifiedOn = dateCreated;
                    nueRequestContext.MichaelRequestPayloadMaster.Add(michaelRequestPayloadMaster);
                }
                //nueRequestContext.SaveChanges();

                MichaelRequestEscalationAccessLogs michaelRequestEscalationAccessLogs = new MichaelRequestEscalationAccessLogs();
                michaelRequestEscalationAccessLogs.RequestId = requestId;
                michaelRequestEscalationAccessLogs.UserId = nueUserProfile.Id;
                michaelRequestEscalationAccessLogs.Active = 1;
                michaelRequestEscalationAccessLogs.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestEscalationAccessLogs.RequestEscalationUserId = slaUser.Id;
                michaelRequestEscalationAccessLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestEscalationAccessLogs.Add(michaelRequestEscalationAccessLogs);
                //nueRequestContext.SaveChanges();

                MichaelRequestEscalationDurationLogs michaelRequestEscalationDurationLogs = new MichaelRequestEscalationDurationLogs();
                michaelRequestEscalationDurationLogs.RequestId = requestId;
                michaelRequestEscalationDurationLogs.Duration = -1;
                michaelRequestEscalationDurationLogs.UserId = slaUser.Id;
                michaelRequestEscalationDurationLogs.RequestEscalationMapperId = requestL1SLa.Id;
                michaelRequestEscalationDurationLogs.RequestEscalationUserId = slaUser.Id;
                michaelRequestEscalationDurationLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestEscalationDurationLogs.Add(michaelRequestEscalationDurationLogs);
                //nueRequestContext.SaveChanges();

                MichaelRequestStageLogs michaelRequestStageLogs = new MichaelRequestStageLogs();
                michaelRequestStageLogs.RequestId = requestId;
                michaelRequestStageLogs.RequestStageBaseId = baseStage.Id;
                michaelRequestStageLogs.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestStageLogs.Add(michaelRequestStageLogs);
                nueRequestContext.SaveChanges();

                MichaelRequestAccessMapper michaelRequestOwnerAccessMapper = new MichaelRequestAccessMapper();
                michaelRequestOwnerAccessMapper.RequestId = requestId;
                michaelRequestOwnerAccessMapper.UserId = nueUserProfile.Id;
                michaelRequestOwnerAccessMapper.RequestAccessTypesId = nueRequestContext.MichaelRequestAccessTypes.Where(x => x.AccessTypes == "owner").SingleOrDefault().Id;
                michaelRequestOwnerAccessMapper.Active = 1;
                michaelRequestOwnerAccessMapper.AddedOn = dateCreated;
                nueRequestContext.MichaelRequestAccessMapper.Add(michaelRequestOwnerAccessMapper);
                nueRequestContext.SaveChanges();

                if (approverFields != null && approverFields.Count > 0)
                {
                    foreach (var item in approverFields)
                    {

                        MichaelRequestAccessMapper michaelRequestAccessMapper = new MichaelRequestAccessMapper();
                        michaelRequestAccessMapper.RequestId = requestId;
                        michaelRequestAccessMapper.UserId = int.Parse(item.Fieldvalue);
                        michaelRequestAccessMapper.RequestAccessTypesId = nueRequestContext.MichaelRequestAccessTypes.Where(x => x.AccessTypes == "approver").SingleOrDefault().Id;
                        michaelRequestAccessMapper.Active = 1;
                        michaelRequestAccessMapper.AddedOn = dateCreated;
                        nueRequestContext.MichaelRequestAccessMapper.Add(michaelRequestAccessMapper);
                        nueRequestContext.SaveChanges();

                        MichaelRequestApproverStatusMapper michaelRequestApproverStatusMapper = new MichaelRequestApproverStatusMapper();
                        michaelRequestApproverStatusMapper.RequestId = requestId;
                        michaelRequestApproverStatusMapper.UserId = int.Parse(item.Fieldvalue);
                        michaelRequestApproverStatusMapper.RequestAccessId = michaelRequestAccessMapper.Id;
                        michaelRequestApproverStatusMapper.RequestAccessTypesId = michaelRequestAccessMapper.RequestAccessTypesId;
                        michaelRequestApproverStatusMapper.ApproverStatusId = nueRequestContext.MichaelApproverStatusTypes.Where(x => x.Status == "pending").SingleOrDefault().Id;
                        michaelRequestApproverStatusMapper.Active = 1;
                        michaelRequestApproverStatusMapper.AddedOn = dateCreated;
                        nueRequestContext.MichaelRequestApproverStatusMapper.Add(michaelRequestApproverStatusMapper);
                        nueRequestContext.SaveChanges();
                    }
                }
                jsonResponse = new JsonResponse("Ok", "Data updated successfully.", requestStrId);
            }
            catch (Exception e1)
            {
                jsonResponse = new JsonResponse("Failed", "An error occerd.");
            }
            return jsonResponse;
        }

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }
    }
}
