using NeuRequest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NeuRequest.DB
{
    public class DataAccess
    {
        string connectionString = String.Empty;
        public DataAccess()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public int addNeuRequestAccessLogs(List<NueRequestAceessLog> nueRequestAceessLogs)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in nueRequestAceessLogs)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestAceessLog (RequestId, UserId, Completed) " +
                                       "output INSERTED.ID VALUES(@RequestId,@UserId,@Completed)", connection))
                    {

                        cmd.Parameters.AddWithValue("@RequestId", item.RequestId);
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);
                        cmd.Parameters.AddWithValue("@Completed", item.Completed);
                        modified = (int)cmd.ExecuteScalar();
                    }
                }
                connection.Close();
            }
            return modified;
        }

        public int addNeuRequest(NueRequestMaster nueRequestMaster)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestMaster (RequestId, IsApprovalProcess, CreatedBy, RequestStatus, PayloadId, RequestCatType) " +
                                       "output INSERTED.ID VALUES(@RequestId,@IsApprovalProcess,@CreatedBy,@RequestStatus,@PayloadId,@RequestCatType)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", nueRequestMaster.RequestId);
                    cmd.Parameters.AddWithValue("@IsApprovalProcess", nueRequestMaster.IsApprovalProcess);
                    cmd.Parameters.AddWithValue("@CreatedBy", nueRequestMaster.CreatedBy);
                    cmd.Parameters.AddWithValue("@RequestStatus", nueRequestMaster.RequestStatus);
                    cmd.Parameters.AddWithValue("@PayloadId", nueRequestMaster.PayloadId);
                    cmd.Parameters.AddWithValue("@RequestCatType", nueRequestMaster.RequestCatType);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addNewLeaveCancelation(NeuLeaveCancelation neuLeaveCancelation)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueLeaveCancelationRequest (UserId, StartDate, EndDate, Message) output INSERTED.ID VALUES(@UserId,@StartDate,@EndDate,@Message)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", neuLeaveCancelation.UserId);
                    cmd.Parameters.AddWithValue("@StartDate", neuLeaveCancelation.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", neuLeaveCancelation.EndDate);
                    cmd.Parameters.AddWithValue("@Message", neuLeaveCancelation.Message);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
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
                                FROM NueUserProfile as NP join EmploymentStatus as ems on NP.EmploymentStatus = ems.Id 
                                join Practice as pc on NP.Practice = pc.Id 
                                join JobLevel as jl on NP.JobLevel = jl.Id 
                                join Designation as ds on NP.Designation = ds.Id 
                                where Email != @Email";
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

        public UserProfile getUserProfile(string email)
        {
            UserProfile userProfile = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT NP.Id, NTPLID, Email, FullName, FirstName, MiddleName, LastName
	                           ,ems.Id as EmpStatusId, ems.Status as EmpStatusDesc, DateofJoining, pc.Id as PracticeId, 
                                pc.Practice as PracticeDesc, Location, jl.Id as JLId, jl.[LEVEL] as JLDesc, 
                                ds.Id as DSId, ds.Desig as DSDesc, Active, NP.AddedOn
                                FROM NueUserProfile as NP join EmploymentStatus as ems on NP.EmploymentStatus = ems.Id 
                                join Practice as pc on NP.Practice = pc.Id 
                                join JobLevel as jl on NP.JobLevel = jl.Id 
                                join Designation as ds on NP.Designation = ds.Id 
                                where Email = @Email";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Email";
                param.Value = email;
                command.Parameters.Add(param);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        userProfile = new UserProfile();
                        //var nt = dataReader["NTPLID"];
                        userProfile.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                       userProfile.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
                       userProfile.Email = ConvertFromDBVal<string>(dataReader["Email"]);
                       userProfile.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                       userProfile.FirstName = ConvertFromDBVal<string>(dataReader["FirstName"]);
                       userProfile.MiddleName = ConvertFromDBVal<string>(dataReader["MiddleName"]);
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
                        break;
                    }
                }

                if(userProfile != null)
                {
                    userProfile.userAccess = new List<UserAccess>();
                    string sql1 = @"select num.id as MapperId, nam.id as AccessItemId, nam.AccessDesc from NueAccessMapper num 
                                    join NueAccessMaster nam on num.AccessId = nam.Id where num.UserId=@UserId";
                    SqlCommand command1 = new SqlCommand(sql1, connection);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@UserId";
                    param1.Value = userProfile.Id;
                    command1.Parameters.Add(param1);
                    using (SqlDataReader dataReader = command1.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserAccess userAccess = new UserAccess();
                            userAccess.MapperId = ConvertFromDBVal<int>(dataReader["MapperId"]);
                            userAccess.AccessItemId = ConvertFromDBVal<int>(dataReader["AccessItemId"]);
                            userAccess.AccessDesc = ConvertFromDBVal<string>(dataReader["AccessDesc"]);
                            userProfile.userAccess.Add(userAccess);
                        }
                    }
                }

                connection.Close();
            }
            return userProfile;
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