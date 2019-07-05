using HCMApi.DAL;
using HCMApi.Modal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.DB
{
    public class DataAccess
    {
        
        string connectionString = String.Empty;

        public DataAccess(AzureAd AzureAdSettings)
        {
            this.connectionString = AzureAdSettings.Db;
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

        public MichaelDepartmentRequestTypeMaster GetDepartmentRequestDetails(int departmentId, int requestId)
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
        }

        public List<MichaelDepartmentRequestTypeMaster> GetDepartmentRequestList(Department department)
        {
            NueRequestContext nueRequestContext = new NueRequestContext();
            return nueRequestContext.MichaelDepartmentRequestTypeMaster.Where(x => x.DepartmentId == department.Id && x.UserId == department.UserId).ToList<MichaelDepartmentRequestTypeMaster>();
        }

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

        public List<MichaelDepartmentMaster> GetDepartmentRequestList(int departmentId)
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
        }


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

        public JsonResponse editDepartmentRequestType(DepartmentRequest departmentRequest)
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
        }

        public JsonResponse addNewDepartmentRequestType(DepartmentRequest departmentRequest)
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
