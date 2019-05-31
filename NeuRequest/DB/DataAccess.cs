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


        public List<UserRequestUiGridRender> getUserHcmActiveRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        where nral.Completed = @Completed 
                                                        and nral.UserId = @UserId 
                                                        and nral.OwnerId= @OwnerId 
                                                        and nrt.RequestType= @RequestType", connection))
                {
                    cmd.Parameters.AddWithValue("@Completed", 0);
                    cmd.Parameters.AddWithValue("@UserId", uid);
                    cmd.Parameters.AddWithValue("@OwnerId", uid);
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            userRequestUiGridRender.NueRequestSubTypeId = ConvertFromDBVal<int>(dataReader["NueRequestSubTypeId"]);
                            userRequestUiGridRender.RequestSubType = ConvertFromDBVal<string>(dataReader["RequestSubType"]);
                            userRequestUiGridRender.NueRequestStatusId = ConvertFromDBVal<int>(dataReader["NueRequestStatusId"]);
                            userRequestUiGridRender.RequestStatus = ConvertFromDBVal<string>(dataReader["RequestStatus"]);
                            userRequestUiGridRender.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            userRequestUiGridRender.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            userRequestUiGridRenders.Add(userRequestUiGridRender);
                        }
                    }
                }
                connection.Close();
            }
            return userRequestUiGridRenders;
        }

        public int addNeuRequestAccessLogs(List<NueRequestAceessLog> nueRequestAceessLogs)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in nueRequestAceessLogs)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestAceessLog (RequestId, UserId, Completed, OwnerId, AddedOn, ModifiedOn) " +
                                       "output INSERTED.ID VALUES(@RequestId,@UserId,@Completed,@OwnerId,@AddedOn,@ModifiedOn)", connection))
                    {

                        cmd.Parameters.AddWithValue("@RequestId", item.RequestId);
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);
                        cmd.Parameters.AddWithValue("@Completed", item.Completed);
                        cmd.Parameters.AddWithValue("@OwnerId", item.OwnerId);
                        cmd.Parameters.AddWithValue("@AddedOn", item.AddedOn);
                        cmd.Parameters.AddWithValue("@ModifiedOn", item.ModifiedOn);
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
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestMaster (RequestId, IsApprovalProcess, CreatedBy, RequestStatus, PayloadId, RequestCatType, AddedOn, ModifiedOn) " +
                                       "output INSERTED.ID VALUES(@RequestId,@IsApprovalProcess,@CreatedBy,@RequestStatus,@PayloadId,@RequestCatType,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", nueRequestMaster.RequestId);
                    cmd.Parameters.AddWithValue("@IsApprovalProcess", nueRequestMaster.IsApprovalProcess);
                    cmd.Parameters.AddWithValue("@CreatedBy", nueRequestMaster.CreatedBy);
                    cmd.Parameters.AddWithValue("@RequestStatus", nueRequestMaster.RequestStatus);
                    cmd.Parameters.AddWithValue("@PayloadId", nueRequestMaster.PayloadId);
                    cmd.Parameters.AddWithValue("@RequestCatType", nueRequestMaster.RequestCatType);
                    cmd.Parameters.AddWithValue("@AddedOn", nueRequestMaster.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", nueRequestMaster.ModifiedOn);
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
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueLeaveCancelationRequest (UserId, RequestId, StartDate, EndDate, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@StartDate,@EndDate,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", neuLeaveCancelation.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", neuLeaveCancelation.RequestId);
                    cmd.Parameters.AddWithValue("@StartDate", neuLeaveCancelation.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", neuLeaveCancelation.EndDate);
                    cmd.Parameters.AddWithValue("@Message", neuLeaveCancelation.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", neuLeaveCancelation.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", neuLeaveCancelation.ModifiedOn);
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