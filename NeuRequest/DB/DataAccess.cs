using NeuRequest.DAL;
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
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        where nral.OwnerId = @OwnerId
														and (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close')
                                                        and nrt.RequestType = @RequestType ORDER BY nrm.AddedOn DESC;", connection))
                {
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


        public List<UserRequestUiGridRender> getUserHcmActiveRequests(int uid, int limit)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT TOP "+limit+@" nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        where nral.OwnerId = @OwnerId
														and (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close')
                                                        and nrt.RequestType = @RequestType ORDER BY nrm.AddedOn DESC;", connection))
                {
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

        public List<UserRequestUiGridRender> getUserHcmInactiveRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        where nral.OwnerId = @OwnerId
														and (nrs.RequestStatus = 'withdraw' or nrs.RequestStatus = 'close')
                                                        and nrt.RequestType = @RequestType", connection))
                {
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

        public List<UserRequestUiGridRender> getHcmActivePreApproverRequests()
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close')
                                                        and nrt.RequestType = @RequestType
														and nral.Completed = @Completed
														and nral.OwnerId != nral.UserId", connection))
                {
                    cmd.Parameters.AddWithValue("@Completed", 0);
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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

        public List<UserRequestUiGridRender> getHcmActiveApproverRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close')
                                                        and nrt.RequestType = @RequestType
														and nral.Completed = @Completed
														and nral.OwnerId != nral.UserId
														and nral.UserId = @OwnerId", connection))
                {
                    cmd.Parameters.AddWithValue("@OwnerId", uid);
                    cmd.Parameters.AddWithValue("@Completed", 0);
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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

        public List<UserRequestUiGridRender> getHcmActiveApproverRequests(int uid, int limit)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT TOP "+limit+ @" nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close')
                                                        and nrt.RequestType = @RequestType
														and nral.Completed = @Completed
														and nral.OwnerId != nral.UserId
														and nral.UserId = @OwnerId ORDER BY nrm.AddedOn DESC;", connection))
                {
                    cmd.Parameters.AddWithValue("@OwnerId", uid);
                    cmd.Parameters.AddWithValue("@Completed", 0);
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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

        public List<UserRequestUiGridRender> getHcmInactiveApproverRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        join NueUserProfile nup on nrm.CreatedBy = nup.Id       
                                                        where nrt.RequestType = @RequestType
														and nral.Completed = @Completed
														and nral.OwnerId != nral.UserId
														and nral.UserId = @OwnerId", connection))
                {
                    cmd.Parameters.AddWithValue("@OwnerId", uid);
                    cmd.Parameters.AddWithValue("@Completed", 1);
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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

        public List<UserRequestUiGridRender> getHcmActiveApproverFinalRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
                                                        join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where (nrs.RequestStatus != 'withdraw' and nrs.RequestStatus != 'close'  and nrs.RequestStatus != 'completed')
                                                        and nrt.RequestType = @RequestType
														and nrm.Id in (SELECT RequestId
                                                        FROM NueRequestAceessLog nral
                                                        GROUP BY RequestId
                                                        HAVING (select COUNT(RequestId) from NueRequestAceessLog 
                                                        where RequestId = nral.RequestId and Completed = 0) <= 0)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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

        public List<UserRequestUiGridRender> getHcmInactiveApproverFinalRequests(int uid)
        {
            List<UserRequestUiGridRender> userRequestUiGridRenders = new List<UserRequestUiGridRender>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID as NTPLID, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
														join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where (nrs.RequestStatus = 'close' or nrs.RequestStatus != 'completed')
                                                        and nrt.RequestType = @RequestType
														and nrm.Id in (SELECT RequestId
                                                        FROM NueRequestAceessLog nral
                                                        GROUP BY RequestId
                                                        HAVING (select COUNT(RequestId) from NueRequestAceessLog 
                                                        where RequestId = nral.RequestId and Completed = 1) > 0)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestType", "HCM");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequestUiGridRender userRequestUiGridRender = new UserRequestUiGridRender();
                            userRequestUiGridRender.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequestUiGridRender.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            userRequestUiGridRender.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            userRequestUiGridRender.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequestUiGridRender.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
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
        
        public int addNeuMessagess(List<MessagesModel> messages)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in messages)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO NeuMessages (Message, EmptyMessage, Processed, UserId, Target, Date) " +
                                      "output INSERTED.MessageID VALUES(@Message, @EmptyMessage, @Processed, @UserId, @Target, @Date)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Message", item.Message);
                        cmd.Parameters.AddWithValue("@EmptyMessage", item.EmptyMessage);
                        cmd.Parameters.AddWithValue("@Processed", item.Processed);
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);
                        cmd.Parameters.AddWithValue("@Target", item.Target);
                        cmd.Parameters.AddWithValue("@Date", item.MessageDate);
                        modified = (int)cmd.ExecuteScalar();
                    }
                }
                connection.Close();
            }
            return modified;
        }

        public int addNeuRequestAccessLogs(List<NuRequestAceessLog> nueRequestAceessLogs)
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

        public int updateNeuRequestAccessLogs(NuRequestAceessLog nueRequestAceessLog)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NueRequestAceessLog SET Completed = @Completed, ModifiedOn = @ModifiedOn" +
                                                        " WHERE RequestId = @RequestId and UserId = @UserId", connection))
                {

                    cmd.Parameters.AddWithValue("@Completed", nueRequestAceessLog.Completed);
                    cmd.Parameters.AddWithValue("@ModifiedOn", nueRequestAceessLog.ModifiedOn);
                    cmd.Parameters.AddWithValue("@RequestId", nueRequestAceessLog.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", nueRequestAceessLog.UserId);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public int updateNeuRequestStatusLogs(NuRequestMaster nueRequestMaster)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NueRequestMaster SET RequestStatus = @RequestStatus, ModifiedOn = @ModifiedOn" +
                                                        " WHERE Id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@RequestStatus", nueRequestMaster.RequestStatus);
                    cmd.Parameters.AddWithValue("@ModifiedOn", nueRequestMaster.ModifiedOn);
                    cmd.Parameters.AddWithValue("@Id", nueRequestMaster.Id);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public int addNeuRequest(NuRequestMaster nueRequestMaster)
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

        public int addLeaveBalanceEnquiry(LeaveBalanceEnquiry leaveBalanceEnquiry)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueLeaveBalanceEnquiryRequest (UserId, RequestId, StartDate, EndDate, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@StartDate,@EndDate,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", leaveBalanceEnquiry.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", leaveBalanceEnquiry.RequestId);
                    cmd.Parameters.AddWithValue("@StartDate", leaveBalanceEnquiry.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", leaveBalanceEnquiry.EndDate);
                    cmd.Parameters.AddWithValue("@Message", leaveBalanceEnquiry.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", leaveBalanceEnquiry.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", leaveBalanceEnquiry.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addPGBRequestUsers(List<PGBRequestUsers> posibleUsers)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in posibleUsers)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO NuePGBRequestUsers (RequestId, UserId, PGBRequestId, AddedOn, ModifiedOn) " +
                                      "output INSERTED.ID VALUES(@RequestId, @UserId, @PGBRequestId, @AddedOn, @ModifiedOn)", connection))
                    {
                        cmd.Parameters.AddWithValue("@RequestId", item.RequestId);
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);
                        cmd.Parameters.AddWithValue("@PGBRequestId", item.PGBRequestId);
                        cmd.Parameters.AddWithValue("@AddedOn", item.AddedOn);
                        cmd.Parameters.AddWithValue("@ModifiedOn", item.ModifiedOn);
                        modified = (int)cmd.ExecuteScalar();
                    }
                }
                connection.Close();
            }
            return modified;

        }

        public int addPGBRequest(PGBRequest pGBRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NuePGBRequest (RequestId, UserId, ProjectName, ClientName, CountryId, StartDate, EndDate, StartFinancialQuarter, OpMode, OpportunitiesCount, EstimatedRevenue, NeedVisiaProcessing, Message, AddedOn, ModifiedOn) " +
                    "output INSERTED.ID VALUES(@RequestId, @UserId, @ProjectName, @ClientName, @CountryId, @StartDate, @EndDate, @StartFinancialQuarter, @OpMode, @OpportunitiesCount, @EstimatedRevenue, @NeedVisiaProcessing, @Message, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", pGBRequest.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", pGBRequest.UserId);
                    cmd.Parameters.AddWithValue("@ProjectName", pGBRequest.ProjectName);
                    cmd.Parameters.AddWithValue("@ClientName", pGBRequest.ClientName);
                    cmd.Parameters.AddWithValue("@CountryId", pGBRequest.CountryId);
                    if (pGBRequest.StartDate != null)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", pGBRequest.StartDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                    }
                    if (pGBRequest.EndDate != null)
                    {
                        cmd.Parameters.AddWithValue("@EndDate", pGBRequest.EndDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@StartFinancialQuarter", pGBRequest.StartFinancialQuarter);
                    cmd.Parameters.AddWithValue("@OpMode", pGBRequest.OpMode);
                    cmd.Parameters.AddWithValue("@OpportunitiesCount", pGBRequest.OpportunitiesCount);
                    if (pGBRequest.EstimatedRevenue != null)
                    {
                        cmd.Parameters.AddWithValue("@EstimatedRevenue", pGBRequest.EstimatedRevenue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EstimatedRevenue", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@NeedVisiaProcessing", pGBRequest.NeedVisiaProcessing);
                    if (pGBRequest.Message != null)
                    {
                        cmd.Parameters.AddWithValue("@Message", pGBRequest.Message);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Message", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@AddedOn", pGBRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", pGBRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addDBLocationChangeRequest(DBLocationChangeRequest dBLocationChangeRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueDBLocationChangeRequest (RequestId, UserId, Location, Message, AddedOn, ModifiedOn) " +
                    "output INSERTED.ID VALUES(@RequestId, @UserId, @Location, @Message, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", dBLocationChangeRequest.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", dBLocationChangeRequest.UserId);
                    cmd.Parameters.AddWithValue("@Location", dBLocationChangeRequest.Location);
                    if (dBLocationChangeRequest.Message != null)
                    {
                        cmd.Parameters.AddWithValue("@Message", dBLocationChangeRequest.Message);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Message", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@AddedOn", dBLocationChangeRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", dBLocationChangeRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addDBManagerChangeRequest(DBManagerChangeRequest dBManagerChangeRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueDBManagerChangeRequest (RequestId, UserId, ManagerId, ProjectName, Message, AddedOn, ModifiedOn) " +
                    "output INSERTED.ID VALUES(@RequestId, @UserId, @ManagerId, @ProjectName, @Message, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", dBManagerChangeRequest.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", dBManagerChangeRequest.UserId);
                    cmd.Parameters.AddWithValue("@ManagerId", dBManagerChangeRequest.ManagerId);
                    if (dBManagerChangeRequest.ProjectName != null)
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", dBManagerChangeRequest.ProjectName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                    }

                    if (dBManagerChangeRequest.Message != null)
                    {
                        cmd.Parameters.AddWithValue("@Message", dBManagerChangeRequest.Message);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Message", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@AddedOn", dBManagerChangeRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", dBManagerChangeRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addInternationalTripRequest(InternationalTripRequest internationalTripRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueInternationalTripRequest (RequestId, UserId, NeedVisiaProcessing, PlaceToVisit, StartDate, ProjectName, Message, AddedOn, ModifiedOn) " +
                    "output INSERTED.ID VALUES(@RequestId, @UserId, @NeedVisiaProcessing, @PlaceToVisit, @StartDate, @ProjectName, @Message, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", internationalTripRequest.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", internationalTripRequest.UserId);
                    cmd.Parameters.AddWithValue("@NeedVisiaProcessing", internationalTripRequest.NeedVisiaProcessing);
                    cmd.Parameters.AddWithValue("@PlaceToVisit", internationalTripRequest.PlaceToVisit);
                    cmd.Parameters.AddWithValue("@StartDate", internationalTripRequest.StartDate);
                    if (internationalTripRequest.ProjectName != null)
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", internationalTripRequest.ProjectName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                    }

                    if (internationalTripRequest.Message != null)
                    {
                        cmd.Parameters.AddWithValue("@Message", internationalTripRequest.Message);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Message", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@AddedOn", internationalTripRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", internationalTripRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addDomesticTripRequest(DomesticTripRequest domesticTripRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueDomesticTripRequest (RequestId, UserId, Accommodation, LocationFrom, LocationTo, StartDate, EndDate, Message, AddedOn, ModifiedOn) " +
                    "output INSERTED.ID VALUES(@RequestId, @UserId, @Accommodation, @LocationFrom, @LocationTo, @StartDate, @EndDate, @Message, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", domesticTripRequest.RequestId);
                    cmd.Parameters.AddWithValue("@UserId", domesticTripRequest.UserId);
                    cmd.Parameters.AddWithValue("@Accommodation", domesticTripRequest.Accommodation);
                    cmd.Parameters.AddWithValue("@LocationFrom", domesticTripRequest.LocationFrom);
                    cmd.Parameters.AddWithValue("@LocationTo", domesticTripRequest.LocationTo);
                    cmd.Parameters.AddWithValue("@StartDate", domesticTripRequest.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", domesticTripRequest.EndDate);
                    if (domesticTripRequest.Message != null)
                    {
                        cmd.Parameters.AddWithValue("@Message", domesticTripRequest.Message);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Message", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@AddedOn", domesticTripRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", domesticTripRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addGeneralRequest(GeneralRequest generalRequest)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueGeneralRequest (UserId, RequestId, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", generalRequest.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", generalRequest.RequestId);
                    cmd.Parameters.AddWithValue("@Message", generalRequest.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", generalRequest.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", generalRequest.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addSalaryCertificateRequest(SalaryCertificate salaryCertificate)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueSalaryCertificateRequest (UserId, RequestId, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", salaryCertificate.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", salaryCertificate.RequestId);
                    cmd.Parameters.AddWithValue("@Message", salaryCertificate.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", salaryCertificate.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", salaryCertificate.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }

        public int addEmployeeVerificationRequest(EmployeeVerificationReq employeeVerificationReq)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NeuEmployeeVerificationRequest (UserId, RequestId, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", employeeVerificationReq.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", employeeVerificationReq.RequestId);
                    cmd.Parameters.AddWithValue("@Message", employeeVerificationReq.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", employeeVerificationReq.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", employeeVerificationReq.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;

        }


        public int addAddressProofRequest(AddressProof addressProof)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueAddressProofRequest (UserId, RequestId, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", addressProof.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", addressProof.RequestId);
                    cmd.Parameters.AddWithValue("@Message", addressProof.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", addressProof.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", addressProof.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addNewLeaveWFHApply(LeaveWFHApply leaveWFHApply)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueLeaveWFHApplyRequest (UserId, RequestId, StartDate, EndDate, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@StartDate,@EndDate,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", leaveWFHApply.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", leaveWFHApply.RequestId);
                    cmd.Parameters.AddWithValue("@StartDate", leaveWFHApply.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", leaveWFHApply.EndDate);
                    cmd.Parameters.AddWithValue("@Message", leaveWFHApply.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", leaveWFHApply.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", leaveWFHApply.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addNewLeavePastApply(NeLeavePastApply neuLeavePastApply)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueLeavePastApplyRequest (UserId, RequestId, StartDate, EndDate, Message, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@UserId,@RequestId,@StartDate,@EndDate,@Message,@AddedOn,@ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", neuLeavePastApply.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", neuLeavePastApply.RequestId);
                    cmd.Parameters.AddWithValue("@StartDate", neuLeavePastApply.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", neuLeavePastApply.EndDate);
                    cmd.Parameters.AddWithValue("@Message", neuLeavePastApply.Message);
                    cmd.Parameters.AddWithValue("@AddedOn", neuLeavePastApply.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", neuLeavePastApply.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addNewLeaveCancelation(NeLeaveCancelation neuLeaveCancelation)
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

        public int addRequestAttachmentLog(AttachmentLog attachmentLog)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestAttachmentLog (RequestId, Request, UserId, OwnerId, FileName, FileExt, VFileName, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@RequestId, @Request, @UserId, @OwnerId, @FileName, @FileExt, @VFileName, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", attachmentLog.RequestId);
                    cmd.Parameters.AddWithValue("@Request", attachmentLog.Request);
                    cmd.Parameters.AddWithValue("@UserId", attachmentLog.UserId);
                    cmd.Parameters.AddWithValue("@OwnerId", attachmentLog.OwnerId);
                    cmd.Parameters.AddWithValue("@FileName", attachmentLog.FileName);
                    cmd.Parameters.AddWithValue("@FileExt", attachmentLog.FileExt);
                    cmd.Parameters.AddWithValue("@VFileName", attachmentLog.VFileName);
                    cmd.Parameters.AddWithValue("@AddedOn", attachmentLog.ModifiedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", attachmentLog.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int addRequestComment(NuRequestActivity nueRequestActivity)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueRequestActivity (Payload, PayloadType, UserId, RequestId, Request, AddedOn, ModifiedOn) output INSERTED.ID VALUES(@Payload, @PayloadType, @UserId, @RequestId, @Request, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@Payload", nueRequestActivity.Payload);
                    cmd.Parameters.AddWithValue("@PayloadType", nueRequestActivity.PayloadType);
                    cmd.Parameters.AddWithValue("@UserId", nueRequestActivity.UserId);
                    cmd.Parameters.AddWithValue("@RequestId", nueRequestActivity.RequestId);
                    cmd.Parameters.AddWithValue("@Request", nueRequestActivity.Request);
                    cmd.Parameters.AddWithValue("@AddedOn", nueRequestActivity.AddedOn);
                    cmd.Parameters.AddWithValue("@ModifiedOn", nueRequestActivity.ModifiedOn);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public NeLeavePastApplyModal getNeuLeavePastApplyDetails(string requestId)
        {
            NeLeavePastApplyModal neuLeavePastApplyModal = new NeLeavePastApplyModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.StartDate, nucr.EndDate, nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueLeavePastApplyRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            neuLeavePastApplyModal = new NeLeavePastApplyModal();
                            neuLeavePastApplyModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            neuLeavePastApplyModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            neuLeavePastApplyModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            neuLeavePastApplyModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            neuLeavePastApplyModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            neuLeavePastApplyModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            neuLeavePastApplyModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            neuLeavePastApplyModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            neuLeavePastApplyModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return neuLeavePastApplyModal;
        }


        public PGBRequestModal getPGBRequestModal(string requestId)
        {
            PGBRequestModal pGBRequestModal = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.ProjectName,
														nucr.ClientName, nucr.CountryId, nuc.CountryName, nuc.TwoCharCountryCode as CountryCode,
														nucr.StartDate, nucr.EndDate,
														nucr.StartFinancialQuarter, nucr.OpMode,
														nucr.OpportunitiesCount, nucr.EstimatedRevenue,
														nucr.NeedVisiaProcessing,
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NuePGBRequest nucr 
														join NueUserProfile nup on nucr.UserId = nup.Id 
														join NeuCountry nuc on nucr.CountryId = nuc.CountryID
														where nucr.RequestId = @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            pGBRequestModal = new PGBRequestModal();
                            pGBRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            pGBRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            pGBRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            pGBRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            pGBRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);

                            pGBRequestModal.ProjectName = ConvertFromDBVal<string>(dataReader["ProjectName"]);
                            pGBRequestModal.ClientName = ConvertFromDBVal<string>(dataReader["ClientName"]);
                            pGBRequestModal.CountryId = ConvertFromDBVal<int>(dataReader["CountryId"]);
                            pGBRequestModal.CountryName = ConvertFromDBVal<string>(dataReader["CountryName"]);
                            pGBRequestModal.CountryCode = ConvertFromDBVal<string>(dataReader["CountryCode"]);
                            pGBRequestModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            pGBRequestModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            pGBRequestModal.StartFinancialQuarter = ConvertFromDBVal<string>(dataReader["StartFinancialQuarter"]);
                            pGBRequestModal.OpMode = ConvertFromDBVal<string>(dataReader["OpMode"]);
                            pGBRequestModal.OpportunitiesCount = ConvertFromDBVal<int>(dataReader["OpportunitiesCount"]);
                            pGBRequestModal.EstimatedRevenue = ConvertFromDBVal<string>(dataReader["EstimatedRevenue"]);
                            pGBRequestModal.NeedVisiaProcessing = ConvertFromDBVal<int>(dataReader["NeedVisiaProcessing"]);
                            pGBRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            pGBRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                List<PGBRequestUsers> posibleUsers = new List<PGBRequestUsers>();
                if (pGBRequestModal != null && pGBRequestModal.RequestId != null && pGBRequestModal.RequestId.Trim() != "")
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId,
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NuePGBRequestUsers nucr 
														join NueUserProfile nup on nucr.UserId = nup.Id 
														where nucr.RequestId = @RequestId and nucr.PGBRequestId = @PGBRequestId", connection))
                    {
                        cmd.Parameters.AddWithValue("@RequestId", requestId);
                        cmd.Parameters.AddWithValue("@PGBRequestId", pGBRequestModal.Id);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                PGBRequestUsers pGBRequestUsers = new PGBRequestUsers();
                                pGBRequestUsers.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                                pGBRequestUsers.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                                pGBRequestUsers.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                                pGBRequestUsers.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                                pGBRequestUsers.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                                pGBRequestUsers.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                                posibleUsers.Add(pGBRequestUsers);
                            }
                        }
                    }
                    pGBRequestModal.posibleUsers = posibleUsers;
                }

                connection.Close();
            }
            return pGBRequestModal;
        }


        public DBLocationChangeRequestModal getDBLocationChangeRequestModal(string requestId)
        {
            DBLocationChangeRequestModal dBLocationChangeRequestModal = new DBLocationChangeRequestModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.Location,
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueDBLocationChangeRequest nucr 
														join NueUserProfile nup on nucr.UserId = nup.Id 
														where nucr.RequestId = @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            dBLocationChangeRequestModal = new DBLocationChangeRequestModal();
                            dBLocationChangeRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            dBLocationChangeRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            dBLocationChangeRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            dBLocationChangeRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            dBLocationChangeRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            dBLocationChangeRequestModal.Location = ConvertFromDBVal<string>(dataReader["Location"]);
                            dBLocationChangeRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            dBLocationChangeRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return dBLocationChangeRequestModal;
        }

        public DBManagerChangeRequestModal getDBManagerChangeRequestModal(string requestId)
        {
            DBManagerChangeRequestModal dBManagerChangeRequestModal = new DBManagerChangeRequestModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.ProjectName,
														nupM.Id as ManagerId, nupM.FullName as ManagerName,
														nupM.NTPLID as ManagerNTPLID, nupm.Email as ManagerEmail, 
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueDBManagerChangeRequest nucr 
														join NueUserProfile nup on nucr.UserId = nup.Id 
														join NueUserProfile nupM on nucr.ManagerId = nupM.Id
														where nucr.RequestId = @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            dBManagerChangeRequestModal = new DBManagerChangeRequestModal();
                            dBManagerChangeRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            dBManagerChangeRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            dBManagerChangeRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            dBManagerChangeRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            dBManagerChangeRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            dBManagerChangeRequestModal.ProjectName = ConvertFromDBVal<string>(dataReader["ProjectName"]);
                            dBManagerChangeRequestModal.ManagerId = ConvertFromDBVal<int>(dataReader["ManagerId"]);
                            dBManagerChangeRequestModal.ManagerName = ConvertFromDBVal<string>(dataReader["ManagerName"]);
                            dBManagerChangeRequestModal.ManagerNTPLID = ConvertFromDBVal<string>(dataReader["ManagerNTPLID"]);
                            dBManagerChangeRequestModal.ManagerEmail = ConvertFromDBVal<string>(dataReader["ManagerEmail"]);
                            dBManagerChangeRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            dBManagerChangeRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return dBManagerChangeRequestModal;
        }

        public InternationalTripRequestModal getInternationalTripRequestModal(string requestId)
        {
            InternationalTripRequestModal internationalTripRequestModal = new InternationalTripRequestModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.NeedVisiaProcessing, nucr.PlaceToVisit, 
														nucr.StartDate, nucr.ProjectName,
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueInternationalTripRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.Id where nucr.RequestId = @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            internationalTripRequestModal = new InternationalTripRequestModal();
                            internationalTripRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            internationalTripRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            internationalTripRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            internationalTripRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            internationalTripRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            internationalTripRequestModal.NeedVisiaProcessing = ConvertFromDBVal<int>(dataReader["NeedVisiaProcessing"]);
                            internationalTripRequestModal.PlaceToVisit = ConvertFromDBVal<string>(dataReader["PlaceToVisit"]);
                            internationalTripRequestModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            internationalTripRequestModal.ProjectName = ConvertFromDBVal<string>(dataReader["ProjectName"]);
                            internationalTripRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            internationalTripRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return internationalTripRequestModal;
        }



        public DomesticTripRequestModal getNeuDomesticTripRequestModal(string requestId)
        {
            DomesticTripRequestModal domesticTripRequestModal = new DomesticTripRequestModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.Accommodation, nucr.LocationFrom, 
														nucr.LocationTo, nucr.StartDate, nucr.EndDate,
														nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueDomesticTripRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId = @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            domesticTripRequestModal = new DomesticTripRequestModal();
                            domesticTripRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            domesticTripRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            domesticTripRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            domesticTripRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            domesticTripRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            domesticTripRequestModal.Accommodation = ConvertFromDBVal<int>(dataReader["Accommodation"]);
                            domesticTripRequestModal.LocationFrom = ConvertFromDBVal<string>(dataReader["LocationFrom"]);
                            domesticTripRequestModal.LocationTo = ConvertFromDBVal<string>(dataReader["LocationTo"]);
                            domesticTripRequestModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            domesticTripRequestModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            domesticTripRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            domesticTripRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return domesticTripRequestModal;
        }

        public GeneralRequestModal getNeuGeneralRequestModalDetails(string requestId)
        {
            GeneralRequestModal generalRequestModal = new GeneralRequestModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueGeneralRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            generalRequestModal = new GeneralRequestModal();
                            generalRequestModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            generalRequestModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            generalRequestModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            generalRequestModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            generalRequestModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            generalRequestModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            generalRequestModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return generalRequestModal;
        }

        public SalaryCertificateModal getNeuSalaryCertificateModalDetails(string requestId)
        {
            SalaryCertificateModal salaryCertificateModal = new SalaryCertificateModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueSalaryCertificateRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            salaryCertificateModal = new SalaryCertificateModal();
                            salaryCertificateModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            salaryCertificateModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            salaryCertificateModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            salaryCertificateModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            salaryCertificateModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            salaryCertificateModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            salaryCertificateModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return salaryCertificateModal;
        }

        public EmployeeVerificationReqModal getNeuEmployeeVerificationReqModalDetails(string requestId)
        {
            EmployeeVerificationReqModal employeeVerificationReqModal = new EmployeeVerificationReqModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NeuEmployeeVerificationRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            employeeVerificationReqModal = new EmployeeVerificationReqModal();
                            employeeVerificationReqModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            employeeVerificationReqModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            employeeVerificationReqModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            employeeVerificationReqModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            employeeVerificationReqModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            employeeVerificationReqModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            employeeVerificationReqModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return employeeVerificationReqModal;
        }

        public AddressProofModal getNeuAddressProofModalDetails(string requestId)
        {
            AddressProofModal addressProofModal = new AddressProofModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueAddressProofRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            addressProofModal = new AddressProofModal();
                            addressProofModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            addressProofModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            addressProofModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            addressProofModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            addressProofModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            addressProofModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            addressProofModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return addressProofModal;
        }

        public LeaveBalanceEnquiryModal getNeuLeaveBalanceEnquiryDetails(string requestId)
        {
            LeaveBalanceEnquiryModal leaveBalanceEnquiryModal = new LeaveBalanceEnquiryModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.StartDate, nucr.EndDate, nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueLeaveBalanceEnquiryRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            leaveBalanceEnquiryModal = new LeaveBalanceEnquiryModal();
                            leaveBalanceEnquiryModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            leaveBalanceEnquiryModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            leaveBalanceEnquiryModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            leaveBalanceEnquiryModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            leaveBalanceEnquiryModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            leaveBalanceEnquiryModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            leaveBalanceEnquiryModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            leaveBalanceEnquiryModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            leaveBalanceEnquiryModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return leaveBalanceEnquiryModal;
        }

        public NeLeaveWFHApplyModal getNeuLeaveWFHApplyDetails(string requestId)
        {
            NeLeaveWFHApplyModal neuLeaveWFHApplyModal = new NeLeaveWFHApplyModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.StartDate, nucr.EndDate, nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueLeaveWFHApplyRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            neuLeaveWFHApplyModal = new NeLeaveWFHApplyModal();
                            neuLeaveWFHApplyModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            neuLeaveWFHApplyModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            neuLeaveWFHApplyModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            neuLeaveWFHApplyModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            neuLeaveWFHApplyModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            neuLeaveWFHApplyModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            neuLeaveWFHApplyModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            neuLeaveWFHApplyModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            neuLeaveWFHApplyModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return neuLeaveWFHApplyModal;
        }

        public NeLeaveCancelationModal getNeuLeaveCancelationDetails(string requestId)
        {
            NeLeaveCancelationModal neuLeaveCancelationModal = new NeLeaveCancelationModal();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nucr.Id, nup.Id as UserId, nup.FullName, nucr.RequestId, 
                                                        nucr.StartDate, nucr.EndDate, nucr.Message, nucr.AddedOn, nucr.ModifiedOn 
                                                        from NueLeaveCancelationRequest nucr join NueUserProfile nup 
                                                        on nucr.UserId = nup.id where nucr.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            neuLeaveCancelationModal = new NeLeaveCancelationModal();
                            neuLeaveCancelationModal.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            neuLeaveCancelationModal.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            neuLeaveCancelationModal.Fullname = ConvertFromDBVal<string>(dataReader["FullName"]);
                            neuLeaveCancelationModal.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            neuLeaveCancelationModal.StartDate = ConvertFromDBVal<string>(dataReader["StartDate"]);
                            neuLeaveCancelationModal.EndDate = ConvertFromDBVal<string>(dataReader["EndDate"]);
                            neuLeaveCancelationModal.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            neuLeaveCancelationModal.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            neuLeaveCancelationModal.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return neuLeaveCancelationModal;
        }


        public List<AttachmentLogModel> getAttachmentLogs(string requestId)
        {
            List<AttachmentLogModel> attachmentLogModels = new List<AttachmentLogModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nral.Id, RequestId, Request, UserId, nup.FullName, OwnerId, 
                                                        FileName, FileExt, VFileName, nral.AddedOn, nral.ModifiedOn 
                                                        from NueRequestAttachmentLog nral join NueUserProfile nup on nral.UserId = nup.Id
                                                        where nral.Request=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            AttachmentLogModel attachmentLogModel = new AttachmentLogModel();
                            attachmentLogModel.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            attachmentLogModel.RequestId = ConvertFromDBVal<int>(dataReader["RequestId"]);
                            attachmentLogModel.Request = ConvertFromDBVal<string>(dataReader["Request"]);
                            attachmentLogModel.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            attachmentLogModel.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            attachmentLogModel.OwnerId = ConvertFromDBVal<int>(dataReader["OwnerId"]);
                            attachmentLogModel.FileName = ConvertFromDBVal<string>(dataReader["FileName"]);
                            attachmentLogModel.FileExt = ConvertFromDBVal<string>(dataReader["FileExt"]);
                            attachmentLogModel.VFileName = ConvertFromDBVal<string>(dataReader["VFileName"]);
                            attachmentLogModel.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            attachmentLogModel.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            attachmentLogModels.Add(attachmentLogModel);
                        }
                    }
                }
                connection.Close();
            }
            return attachmentLogModels;
        }

        public List<NuRequestActivityModel> getRequestLogs(string requestId)
        {
            List<NuRequestActivityModel> nueRequestActivityModels = new List<NuRequestActivityModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nra.Id as Id, nra.Payload as Payload, nram.Id as PayloadTypeId, 
                            nram.ActivityDesc as PayloadTypeDesc, nup.Id as UserId, nup.FullName as FullName, nup.NTPLID, nrm.Id as RequestId, 
                            nrm.RequestId as Request, nra.AddedOn, nra.ModifiedOn 
                            from NueRequestActivity nra 
                            join NueRequestActivityMaster nram 
                            on nra.PayloadType = nram.Id 
                            join NueUserProfile nup on nra.UserId = nup.Id
                            join NueRequestMaster nrm on nra.RequestId = nrm.Id
                            where nra.Request=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            NuRequestActivityModel nueRequestActivityModel = new NuRequestActivityModel();
                            nueRequestActivityModel.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestActivityModel.Payload = ConvertFromDBVal<string>(dataReader["Payload"]);
                            nueRequestActivityModel.PayloadTypeId = ConvertFromDBVal<int>(dataReader["PayloadTypeId"]);
                            nueRequestActivityModel.PayloadTypeDesc = ConvertFromDBVal<string>(dataReader["PayloadTypeDesc"]);
                            nueRequestActivityModel.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            nueRequestActivityModel.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            nueRequestActivityModel.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
                            nueRequestActivityModel.RequestId = ConvertFromDBVal<int>(dataReader["RequestId"]);
                            nueRequestActivityModel.Request = ConvertFromDBVal<string>(dataReader["Request"]);
                            nueRequestActivityModel.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            nueRequestActivityModel.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            nueRequestActivityModels.Add(nueRequestActivityModel);
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestActivityModels;
        }

        public List<NuRequestAceessLog> getRequestAccessList(string requestId)
        {
            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select nral.id as Id, nral.RequestId,nral.UserId, nral.OwnerId, 
                                            nral.Completed, nral.AddedOn,nral.ModifiedOn from NueRequestAceessLog nral 
                                            join NueRequestMaster nrm on nral.RequestId = nrm.Id 
                                            where nrm.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestAceessLog.RequestId = ConvertFromDBVal<int>(dataReader["RequestId"]);
                            nueRequestAceessLog.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            nueRequestAceessLog.OwnerId = ConvertFromDBVal<int>(dataReader["OwnerId"]);
                            nueRequestAceessLog.Completed = ConvertFromDBVal<int>(dataReader["Completed"]);
                            nueRequestAceessLog.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            nueRequestAceessLog.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestAceessLogs;
        }

        public List<NuRequestAceessLog> getAllRequestAccessList()
        {
            List<NuRequestAceessLog> nueRequestAceessLogs = new List<NuRequestAceessLog>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select nral.id as Id, nral.RequestId,nral.UserId, nral.OwnerId, 
                                            nral.Completed, nral.AddedOn,nral.ModifiedOn from NueRequestAceessLog nral 
                                            join NueRequestMaster nrm on nral.RequestId = nrm.Id", connection))
                {
                    //cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            NuRequestAceessLog nueRequestAceessLog = new NuRequestAceessLog();
                            nueRequestAceessLog.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestAceessLog.RequestId = ConvertFromDBVal<int>(dataReader["RequestId"]);
                            nueRequestAceessLog.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            nueRequestAceessLog.OwnerId = ConvertFromDBVal<int>(dataReader["OwnerId"]);
                            nueRequestAceessLog.Completed = ConvertFromDBVal<int>(dataReader["Completed"]);
                            nueRequestAceessLog.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            nueRequestAceessLog.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            nueRequestAceessLogs.Add(nueRequestAceessLog);
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestAceessLogs;
        }

        public List<UserRequest> getRequestDetailsSearchById(string requestId)
        {
            List<UserRequest> userRequests = new List<UserRequest>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select DISTINCT nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrm.IsApprovalProcess as ApprovalProcess, nrm.PayloadId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nup.Id as OwnerId, nup.FullName as FullName, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
														join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where nrm.RequestId like @RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", "%" + requestId + "%");
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            UserRequest userRequest = new UserRequest();
                            userRequest.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequest.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            userRequest.ApprovalProcess = ConvertFromDBVal<int>(dataReader["ApprovalProcess"]);
                            userRequest.PayloadId = ConvertFromDBVal<int>(dataReader["PayloadId"]);
                            userRequest.NueRequestSubTypeId = ConvertFromDBVal<int>(dataReader["NueRequestSubTypeId"]);
                            userRequest.RequestSubType = ConvertFromDBVal<string>(dataReader["RequestSubType"]);
                            userRequest.NueRequestStatusId = ConvertFromDBVal<int>(dataReader["NueRequestStatusId"]);
                            userRequest.RequestStatus = ConvertFromDBVal<string>(dataReader["RequestStatus"]);
                            userRequest.OwnerId = ConvertFromDBVal<int>(dataReader["OwnerId"]);
                            userRequest.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequest.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            userRequest.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            userRequests.Add(userRequest);
                        }
                    }
                }
                connection.Close();
            }
            return userRequests;
        }

        public UserRequest getRequestDetailsByReqId(string requestId)
        {
            UserRequest userRequest = new UserRequest();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"select nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrm.IsApprovalProcess as ApprovalProcess, nrm.PayloadId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
                                                        nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nup.Id as OwnerId, nup.FullName as FullName, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
                                                        NueRequestMaster nrm 
                                                        join NueRequestAceessLog nral on nrm.Id = nral.RequestId
                                                        join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
                                                        join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                        join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
														join NueUserProfile nup on nrm.CreatedBy = nup.Id
                                                        where nrm.RequestId=@RequestId", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            userRequest = new UserRequest();
                            userRequest.NueRequestMasterId = ConvertFromDBVal<int>(dataReader["NueRequestMasterId"]);
                            userRequest.RequestId = ConvertFromDBVal<string>(dataReader["RequestId"]);
                            userRequest.ApprovalProcess = ConvertFromDBVal<int>(dataReader["ApprovalProcess"]);
                            userRequest.PayloadId = ConvertFromDBVal<int>(dataReader["PayloadId"]);
                            userRequest.NueRequestSubTypeId = ConvertFromDBVal<int>(dataReader["NueRequestSubTypeId"]);
                            userRequest.RequestSubType = ConvertFromDBVal<string>(dataReader["RequestSubType"]);
                            userRequest.NueRequestStatusId = ConvertFromDBVal<int>(dataReader["NueRequestStatusId"]);
                            userRequest.RequestStatus = ConvertFromDBVal<string>(dataReader["RequestStatus"]);
                            userRequest.OwnerId = ConvertFromDBVal<int>(dataReader["OwnerId"]);
                            userRequest.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            userRequest.AddedOn = ConvertFromDBVal<DateTime>(dataReader["AddedOn"]);
                            userRequest.ModifiedOn = ConvertFromDBVal<DateTime>(dataReader["ModifiedOn"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return userRequest;
        }

        public NuRequestActivityMaster getRequestType(string requestMainType, string requestSubType)
        {
            NuRequestActivityMaster nueRequestActivityMaster = new NuRequestActivityMaster();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT nrst.Id, RequestSubType
                                                          FROM NueRequestSubType nrst 
                                                          join NueRequestType nrt on nrst.RequestType = nrt.Id
                                                          WHERE nrt.RequestType = @RequestType and nrst.RequestSubType = @RequestSubType", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestType", requestMainType);
                    cmd.Parameters.AddWithValue("@RequestSubType", requestSubType);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            nueRequestActivityMaster = new NuRequestActivityMaster();
                            nueRequestActivityMaster.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestActivityMaster.ActivityDesc = ConvertFromDBVal<string>(dataReader["RequestSubType"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestActivityMaster;
        }

        public NuRequestActivityMaster getRequestStatus(string statusType)
        {
            NuRequestActivityMaster nueRequestActivityMaster = new NuRequestActivityMaster();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT Id, RequestStatus from NueRequestStatus where RequestStatus=@RequestStatusDesc", connection))
                {
                    cmd.Parameters.AddWithValue("@RequestStatusDesc", statusType);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            nueRequestActivityMaster = new NuRequestActivityMaster();
                            nueRequestActivityMaster.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestActivityMaster.ActivityDesc = ConvertFromDBVal<string>(dataReader["RequestStatus"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestActivityMaster;
        }

        public NuRequestActivityMaster getRequestActivityMasterId(string activityType)
        {
            NuRequestActivityMaster nueRequestActivityMaster = new NuRequestActivityMaster();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT Id, ActivityDesc from NueRequestActivityMaster where ActivityDesc=@ActivityDesc", connection))
                {
                    cmd.Parameters.AddWithValue("@ActivityDesc", activityType);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            nueRequestActivityMaster = new NuRequestActivityMaster();
                            nueRequestActivityMaster.Id = ConvertFromDBVal<int>(dataReader["Id"]);
                            nueRequestActivityMaster.ActivityDesc = ConvertFromDBVal<string>(dataReader["ActivityDesc"]);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return nueRequestActivityMaster;
        }

        public List<UserProfile> getAllUserProfilesAll()
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
                                join NeuDesignation as ds on NP.Designation = ds.Id";
                SqlCommand command = new SqlCommand(sql, connection);
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

                        if (userProfile != null)
                        {
                            userProfile.userAccess = new List<UserAccess>();
                            string sql1 = @"select num.id as MapperId, nam.id as AccessItemId, nam.AccessDesc from NueAccessMapper num 
                                    join NueAccessMaster nam on num.AccessId = nam.Id where num.UserId=@UserId";
                            SqlCommand command1 = new SqlCommand(sql1, connection);
                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@UserId";
                            param1.Value = userProfile.Id;
                            command1.Parameters.Add(param1);
                            using (SqlDataReader dataReader1 = command1.ExecuteReader())
                            {
                                while (dataReader1.Read())
                                {
                                    UserAccess userAccess = new UserAccess();
                                    userAccess.MapperId = ConvertFromDBVal<int>(dataReader1["MapperId"]);
                                    userAccess.AccessItemId = ConvertFromDBVal<int>(dataReader1["AccessItemId"]);
                                    userAccess.AccessDesc = ConvertFromDBVal<string>(dataReader1["AccessDesc"]);
                                    userProfile.userAccess.Add(userAccess);
                                }
                            }
                        }

                        if (userProfile != null)
                        {
                            userProfile.userPreference = new UserPreference();
                            string sql1 = @"SELECT nup.Id, UserId, IsMailCommunication, 
                                          FirstApprover, SecondApprover
                                          FROM NeuUserPreference nup 
                                          join NueUserProfile as np on nup.UserId = np.Id
                                          where np.Id = @UserId";
                            SqlCommand command1 = new SqlCommand(sql1, connection);
                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@UserId";
                            param1.Value = userProfile.Id;
                            command1.Parameters.Add(param1);
                            using (SqlDataReader dataReader1 = command1.ExecuteReader())
                            {
                                while (dataReader1.Read())
                                {
                                    UserPreference userPreference = new UserPreference();
                                    userPreference.Id = ConvertFromDBVal<int>(dataReader1["Id"]);
                                    userPreference.UserId = ConvertFromDBVal<int>(dataReader1["UserId"]);
                                    userPreference.FirstApprover = ConvertFromDBVal<int>(dataReader1["FirstApprover"]);
                                    userPreference.SecondApprover = ConvertFromDBVal<int>(dataReader1["SecondApprover"]);
                                    userPreference.IsMailCommunication = ConvertFromDBVal<int>(dataReader1["IsMailCommunication"]);
                                    userProfile.userPreference = userPreference;
                                }
                            }
                        }


                        userProfiles.Add(userProfile);
                    }
                }

                connection.Close();
            }
            return userProfiles;
        }

        public List<Country> getAllCountries()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT CountryID, CountryName, TwoCharCountryCode, ThreeCharCountryCode FROM NeuCountry";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Country country = new Country();
                        //var nt = dataReader["NTPLID"];
                        country.CountryID = ConvertFromDBVal<int>(dataReader["CountryID"]);
                        country.CountryName = ConvertFromDBVal<string>(dataReader["CountryName"]);
                        country.TwoCharCountryCode = ConvertFromDBVal<string>(dataReader["TwoCharCountryCode"]);
                        country.ThreeCharCountryCode = ConvertFromDBVal<string>(dataReader["ThreeCharCountryCode"]);
                        countries.Add(country);
                    }
                }
                connection.Close();
            }
            return countries;
        }

        public List<NueUserProfile> getAllUserProfilesDinamic()
        {
            NueRequestEntities nueRequestEntities = new NueRequestEntities();
            return nueRequestEntities.NueUserProfile.ToList<NueUserProfile>();
        }

        public List<UserProfile> getAllUserProfiles()
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
                                WHERE NP.Active = 1";
                SqlCommand command = new SqlCommand(sql, connection);
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

                        if (userProfile != null)
                        {
                            userProfile.userAccess = new List<UserAccess>();
                            string sql1 = @"select num.id as MapperId, nam.id as AccessItemId, nam.AccessDesc from NueAccessMapper num 
                                    join NueAccessMaster nam on num.AccessId = nam.Id where num.UserId=@UserId";
                            SqlCommand command1 = new SqlCommand(sql1, connection);
                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@UserId";
                            param1.Value = userProfile.Id;
                            command1.Parameters.Add(param1);
                            using (SqlDataReader dataReader1 = command1.ExecuteReader())
                            {
                                while (dataReader1.Read())
                                {
                                    UserAccess userAccess = new UserAccess();
                                    userAccess.MapperId = ConvertFromDBVal<int>(dataReader1["MapperId"]);
                                    userAccess.AccessItemId = ConvertFromDBVal<int>(dataReader1["AccessItemId"]);
                                    userAccess.AccessDesc = ConvertFromDBVal<string>(dataReader1["AccessDesc"]);
                                    userProfile.userAccess.Add(userAccess);
                                }
                            }
                        }

                        if (userProfile != null)
                        {
                            userProfile.userPreference = new UserPreference();
                            string sql1 = @"SELECT nup.Id, UserId, IsMailCommunication,
                                          FirstApprover, SecondApprover
                                          FROM NeuUserPreference nup 
                                          join NueUserProfile as np on nup.UserId = np.Id
                                          where np.Id = @UserId";
                            SqlCommand command1 = new SqlCommand(sql1, connection);
                            SqlParameter param1 = new SqlParameter();
                            param1.ParameterName = "@UserId";
                            param1.Value = userProfile.Id;
                            command1.Parameters.Add(param1);
                            using (SqlDataReader dataReader1 = command1.ExecuteReader())
                            {
                                while (dataReader1.Read())
                                {
                                    UserPreference userPreference = new UserPreference();
                                    userPreference.Id = ConvertFromDBVal<int>(dataReader1["Id"]);
                                    userPreference.UserId = ConvertFromDBVal<int>(dataReader1["UserId"]);
                                    userPreference.FirstApprover = ConvertFromDBVal<int>(dataReader1["FirstApprover"]);
                                    userPreference.SecondApprover = ConvertFromDBVal<int>(dataReader1["SecondApprover"]);
                                    userPreference.IsMailCommunication = ConvertFromDBVal<int>(dataReader1["IsMailCommunication"]);
                                    userProfile.userPreference = userPreference;
                                }
                            }
                        }


                        userProfiles.Add(userProfile);
                    }
                }

                connection.Close();
            }
            return userProfiles;
        }

        public List<UserProfile> getAllUserProfilesX()
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
                                where Active = 1";
                SqlCommand command = new SqlCommand(sql, connection);
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
        public List<UserAccess> getUserAccess()
        {
            List<UserAccess> userAccesses = new List<UserAccess>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Id, AccessDesc, AddedOn FROM NueAccessMaster";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        UserAccess userAccess = new UserAccess();
                        //var nt = dataReader["NTPLID"];
                        userAccess.AccessItemId = ConvertFromDBVal<int>(dataReader["Id"]);
                        userAccess.AccessDesc = ConvertFromDBVal<string>(dataReader["AccessDesc"]);
                        userAccesses.Add(userAccess);
                    }
                }

                connection.Close();
            }
            return userAccesses;
        }

        public UserProfile addUserFullProfile(UserProfile userProfile)
        {
            List<UserProfile> userProfiles = getAllUserProfiles();
            var exist = userProfiles.Where(x => (x.Email == userProfile.Email
                && x.NTPLID == userProfile.NTPLID && x.FullName == userProfile.FullName));
            if (userProfile != null && exist != null && exist.Count() <= 0)
            {//update
                int rowsAffected = addNewUserProfile(userProfile);
                if (rowsAffected != -1)
                {
                    UserProfile userProfile1 = getUserProfile(userProfile.Email);
                    userProfile.Id = userProfile1.Id;
                    addUserAccess(userProfile);
                    userProfile = getUserProfile(userProfile.Email);
                }
                else
                {
                    throw new Exception("Invalid request. Unable to locate requested information");
                }
            }
            return userProfile;
        }

        public UserProfile updateUserFullProfile(UserProfile userProfile)
        {
            UserProfile userProfileOld = getUserProfile(userProfile.Email);
            List<UserProfile> userProfiles = getAllUserProfiles();
            var exist = userProfiles.Where(x => x.Id != userProfile.Id && (x.Email == userProfile.Email
                && x.NTPLID == userProfile.NTPLID && x.FullName == userProfile.FullName));
            if (userProfileOld != null && exist != null && exist.Count() <= 0)
            {//update
                userProfileOld.NTPLID = userProfile.NTPLID;
                userProfileOld.FirstName = userProfile.FirstName;
                userProfileOld.MiddleName = userProfile.MiddleName;
                userProfileOld.LastName = userProfile.LastName;
                userProfileOld.EmpStatusId = userProfile.EmpStatusId;
                userProfileOld.PracticeId = userProfile.PracticeId;
                userProfileOld.DateofJoining = userProfile.DateofJoining;
                userProfileOld.Location = userProfile.Location;
                userProfileOld.JLId = userProfile.JLId;
                userProfileOld.DSId = userProfile.DSId;
                userProfileOld.Active = userProfile.Active;
                int rowsAffected = updateUserProfile(userProfileOld);
                if (rowsAffected != -1)
                {
                    if (deleteUserAccess(userProfile) != -1)
                    {
                        addUserAccess(userProfile);
                        userProfile = getUserProfile(userProfile.Email);
                    }
                }
                else
                {
                    throw new Exception("Invalid request. Unable to update data");
                }
            }
            return userProfile;
        }

        public UserProfile saveUserProfile(UserProfile userProfile)
        {
            UserProfile userProfileOld = getUserProfile(userProfile.Email);
            if(userProfileOld != null)
            {//update
                userProfileOld.NTPLID = userProfile.NTPLID;
                userProfileOld.FirstName = userProfile.FirstName;
                userProfileOld.MiddleName = userProfile.MiddleName;
                userProfileOld.LastName = userProfile.LastName;
                userProfileOld.EmpStatusId = userProfile.EmpStatusId;
                userProfileOld.PracticeId = userProfile.PracticeId;
                userProfileOld.DateofJoining = userProfile.DateofJoining;
                userProfileOld.Location = userProfile.Location;
                userProfileOld.JLId = userProfile.JLId;
                userProfileOld.DSId = userProfile.DSId;
                userProfileOld.Active = userProfile.Active;
                int rowsAffected = updateUserProfile(userProfileOld);
                if(rowsAffected != -1)
                {
                    //update user pref
                    UserPreference userPreference = userProfile.userPreference;
                    userPreference.UserId = userProfileOld.Id;
                    updateUserPreference(userPreference);
                    userProfile = getUserProfile(userProfile.Email);
                }
                else
                {
                    throw new Exception("Invalid request. Unable to locate requested information");
                }
            }
            else
            {//insert new user id
                int rowsAffected = addNewUserProfile(userProfile);
                if (rowsAffected != -1)
                {
                    userProfile = getUserProfile(userProfile.Email);
                    UserPreference userPreference = new UserPreference();
                    userPreference.UserId = userProfile.Id;
                    userPreference.IsMailCommunication = 1;
                    addUserPreference(userPreference);
                    userProfile = getUserProfile(userProfile.Email);
                }
                else
                {
                    throw new Exception("Invalid request. Unable to locate requested information");
                }
            }
            return userProfile;
        }

        private int addNewUserProfile(UserProfile userProfile)
        {
            userProfile.Email = userProfile.Email.ToLower();
            var dateCreated = DateTime.UtcNow;
            //userProfile.AddedOn = dateCreated.ToString();
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NueUserProfile (NTPLID, Email, FullName, FirstName, MiddleName, LastName, EmploymentStatus, DateofJoining, Practice, Location, JobLevel, Designation, Active, AddedOn) " +
                                       "output INSERTED.ID VALUES(@NTPLID, @Email, @FullName, @FirstName, @MiddleName, @LastName, @EmploymentStatus, @DateofJoining, @Practice, @Location, @JobLevel, @Designation, @Active, @AddedOn)", connection))
                {

                    cmd.Parameters.AddWithValue("@NTPLID", userProfile.NTPLID);
                    cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@FullName", userProfile.FullName);
                    cmd.Parameters.AddWithValue("@FirstName", userProfile.FirstName);
                    if (userProfile.MiddleName != null)
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", userProfile.MiddleName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@LastName", userProfile.LastName);
                    cmd.Parameters.AddWithValue("@EmploymentStatus", userProfile.EmpStatusId);
                    cmd.Parameters.AddWithValue("@DateofJoining", userProfile.DateofJoining);
                    cmd.Parameters.AddWithValue("@Practice", userProfile.PracticeId);
                    cmd.Parameters.AddWithValue("@Location", userProfile.Location);
                    cmd.Parameters.AddWithValue("@JobLevel", userProfile.JLId);
                    cmd.Parameters.AddWithValue("@Designation", userProfile.DSId);
                    cmd.Parameters.AddWithValue("@Active", userProfile.Active);
                    cmd.Parameters.AddWithValue("@AddedOn", dateCreated);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        private int deleteUserAccess(UserProfile userProfile)
        {
            userProfile.Email = userProfile.Email.ToLower();
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM NueAccessMapper " +
                                                        " WHERE UserId = @UserId", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userProfile.Id);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public int addUserAccess(UserProfile userProfile)
        {
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in userProfile.userAccess)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO NueAccessMapper (UserId, AccessId) " +
                                       "output INSERTED.ID VALUES(@UserId,@AccessId)", connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", item.MapperId);
                        cmd.Parameters.AddWithValue("@AccessId", item.AccessItemId);
                        modified = (int)cmd.ExecuteScalar();
                    }
                }
                connection.Close();
            }
            return modified;
        }

        public int addUserPreferenceL1(UserPreference userPreference)
        {
            var dateCreated = DateTime.UtcNow;
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NeuUserPreference SET FirstApprover = @FirstApprover WHERE UserId = @UserId", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userPreference.UserId);
                    cmd.Parameters.AddWithValue("@FirstApprover", userPreference.FirstApprover);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public int addUserPreference(UserPreference userPreference)
        {
            var dateCreated = DateTime.UtcNow;
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NeuUserPreference (UserId, IsMailCommunication, AddedOn, ModifiedOn) " +
                                      "output INSERTED.ID VALUES(@UserId, @IsMailCommunication, @AddedOn, @ModifiedOn)", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userPreference.UserId);
                    cmd.Parameters.AddWithValue("@IsMailCommunication", userPreference.IsMailCommunication);
                    cmd.Parameters.AddWithValue("@AddedOn", dateCreated);
                    cmd.Parameters.AddWithValue("@ModifiedOn", dateCreated);
                    modified = (int)cmd.ExecuteScalar();
                }
                connection.Close();
            }
            return modified;
        }

        public int updateUserPreference(UserPreference userPreference)
        {
            var dateCreated = DateTime.UtcNow;
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NeuUserPreference SET IsMailCommunication = @IsMailCommunication, " +
                        "ModifiedOn = @ModifiedOn  WHERE UserId = @UserId", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userPreference.UserId);
                    cmd.Parameters.AddWithValue("@IsMailCommunication", userPreference.IsMailCommunication);
                    cmd.Parameters.AddWithValue("@ModifiedOn", dateCreated);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public List<MessagesModel> getAllNotification(MessagesModel messagesModel)
        {
            List<MessagesModel> messages = new List<MessagesModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MessageID, Message, EmptyMessage, Processed, UserId, nup.FullName, nup.NTPLID , " +
                    "Target, Date FROM NeuMessages m join NueUserProfile nup on m.UserId = nup.Id WHERE UserId = @UserId ORDER BY [Date] DESC;", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", messagesModel.UserId);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            MessagesModel message = new MessagesModel();
                            //var nt = dataReader["NTPLID"];
                            message.MessageID = ConvertFromDBVal<int>(dataReader["MessageID"]);
                            message.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            message.EmptyMessage = ConvertFromDBVal<string>(dataReader["EmptyMessage"]);
                            message.Processed = ConvertFromDBVal<int>(dataReader["Processed"]);
                            message.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            message.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            message.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
                            message.Target = ConvertFromDBVal<string>(dataReader["Target"]);
                            message.MessageDate = ConvertFromDBVal<DateTime>(dataReader["Date"]);
                            messages.Add(message);
                        }
                    }
                }
                connection.Close();
            }
            return messages;
        }

        public MessagesModel updateNotification(MessagesModel messagesModel)
        {
            MessagesModel messagesModelRet = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MessageID, Message, EmptyMessage, Processed, UserId, nup.FullName, nup.NTPLID , " +
                    "Target, Date FROM NeuMessages m join NueUserProfile nup on m.UserId = nup.Id where MessageID = @MessageID", connection))
                {
                    cmd.Parameters.AddWithValue("@MessageID", messagesModel.MessageID);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            messagesModelRet = new MessagesModel();
                            //var nt = dataReader["NTPLID"];
                            messagesModelRet.MessageID = ConvertFromDBVal<int>(dataReader["MessageID"]);
                            messagesModelRet.Message = ConvertFromDBVal<string>(dataReader["Message"]);
                            messagesModelRet.EmptyMessage = ConvertFromDBVal<string>(dataReader["EmptyMessage"]);
                            messagesModelRet.Processed = ConvertFromDBVal<int>(dataReader["Processed"]);
                            messagesModelRet.UserId = ConvertFromDBVal<int>(dataReader["UserId"]);
                            messagesModelRet.FullName = ConvertFromDBVal<string>(dataReader["FullName"]);
                            messagesModelRet.NTPLID = ConvertFromDBVal<string>(dataReader["NTPLID"]);
                            messagesModelRet.Target = ConvertFromDBVal<string>(dataReader["Target"]);
                            messagesModelRet.MessageDate = ConvertFromDBVal<DateTime>(dataReader["Date"]);
                            break;
                        }
                    }

                    if(messagesModelRet != null && messagesModelRet.MessageID == messagesModel.MessageID)
                    {
                        using (SqlCommand cmd1 = new SqlCommand("UPDATE NeuMessages SET Processed = @Processed where MessageID = @MessageID", connection))
                        {
                            cmd1.Parameters.AddWithValue("@Processed", 1);
                            cmd1.Parameters.AddWithValue("@MessageID", messagesModel.MessageID);
                            int modified = (int)cmd1.ExecuteNonQuery();
                            if(modified != -1)
                            {
                                messagesModelRet.Processed = 1;
                            }
                            else
                            {
                                messagesModelRet = null;
                            }
                        }
                    }
                    else
                    {
                        messagesModelRet = null;
                    }
                }
                connection.Close();
            }
            return messagesModelRet;
        }

        public int updateUserProfileActiveState(UserProfile userProfile)
        {
            userProfile.Email = userProfile.Email.ToLower();
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NueUserProfile SET Active = @Active " +
                                                        " WHERE Id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", userProfile.Id);
                    cmd.Parameters.AddWithValue("@Active", userProfile.Active);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        private int updateUserProfile(UserProfile userProfile)
        {
            userProfile.Email = userProfile.Email.ToLower();
            int modified = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NueUserProfile SET NTPLID = @NTPLID, " +
                                                        " FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, " +
                                                        " EmploymentStatus = @EmploymentStatus, DateofJoining = @DateofJoining," +
                                                        " Practice = @Practice, Location = @Location, JobLevel = @JobLevel, " +
                                                        " Designation = @Designation, Active = @Active " +
                                                        " WHERE Email = @Email", connection))
                {

                    cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@NTPLID", userProfile.NTPLID);
                    cmd.Parameters.AddWithValue("@FirstName", userProfile.FirstName);
                    if(userProfile.MiddleName != null)
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", userProfile.MiddleName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@LastName", userProfile.LastName);
                    cmd.Parameters.AddWithValue("@EmploymentStatus", userProfile.EmpStatusId);
                    cmd.Parameters.AddWithValue("@DateofJoining", userProfile.DateofJoining);
                    cmd.Parameters.AddWithValue("@Practice", userProfile.PracticeId);
                    cmd.Parameters.AddWithValue("@Location", userProfile.Location);
                    cmd.Parameters.AddWithValue("@JobLevel", userProfile.JLId);
                    cmd.Parameters.AddWithValue("@Designation", userProfile.DSId);
                    cmd.Parameters.AddWithValue("@Active", userProfile.Active);
                    modified = (int)cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return modified;
        }

        public UserProfile getUserProfileById(string id)
        {
            UserProfile userProfile = null;
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
                                where NP.Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;
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
                        break;
                    }
                }

                if (userProfile != null)
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

                if (userProfile != null)
                {
                    userProfile.userPreference = new UserPreference();
                    string sql1 = @"SELECT nup.Id, UserId, IsMailCommunication, 
                                          FirstApprover, SecondApprover
                                          FROM NeuUserPreference nup 
                                          join NueUserProfile as np on nup.UserId = np.Id
                                          where np.Id = @UserId";
                    SqlCommand command1 = new SqlCommand(sql1, connection);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@UserId";
                    param1.Value = userProfile.Id;
                    command1.Parameters.Add(param1);
                    using (SqlDataReader dataReader1 = command1.ExecuteReader())
                    {
                        while (dataReader1.Read())
                        {
                            UserPreference userPreference = new UserPreference();
                            userPreference.Id = ConvertFromDBVal<int>(dataReader1["Id"]);
                            userPreference.UserId = ConvertFromDBVal<int>(dataReader1["UserId"]);
                            userPreference.FirstApprover = ConvertFromDBVal<int>(dataReader1["FirstApprover"]);
                            userPreference.SecondApprover = ConvertFromDBVal<int>(dataReader1["SecondApprover"]);
                            userPreference.IsMailCommunication = ConvertFromDBVal<int>(dataReader1["IsMailCommunication"]);
                            userProfile.userPreference = userPreference;
                        }
                    }
                }

                connection.Close();
            }
            return userProfile;
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
                                FROM NueUserProfile as NP join NeuEmploymentStatus as ems on NP.EmploymentStatus = ems.Id 
                                join NeuPractice as pc on NP.Practice = pc.Id 
                                join NeuJobLevel as jl on NP.JobLevel = jl.Id 
                                join NeuDesignation as ds on NP.Designation = ds.Id 
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

                if (userProfile != null)
                {
                    userProfile.userPreference = new UserPreference();
                    string sql1 = @"SELECT nup.Id, UserId, IsMailCommunication,
                                          FirstApprover, SecondApprover
                                          FROM NeuUserPreference nup 
                                          join NueUserProfile as np on nup.UserId = np.Id
                                          where np.Id = @UserId";
                    SqlCommand command1 = new SqlCommand(sql1, connection);
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@UserId";
                    param1.Value = userProfile.Id;
                    command1.Parameters.Add(param1);
                    using (SqlDataReader dataReader1 = command1.ExecuteReader())
                    {
                        while (dataReader1.Read())
                        {
                            UserPreference userPreference = new UserPreference();
                            userPreference.Id = ConvertFromDBVal<int>(dataReader1["Id"]);
                            userPreference.UserId = ConvertFromDBVal<int>(dataReader1["UserId"]);
                            userPreference.FirstApprover = ConvertFromDBVal<int>(dataReader1["FirstApprover"]);
                            userPreference.SecondApprover = ConvertFromDBVal<int>(dataReader1["SecondApprover"]);
                            userPreference.IsMailCommunication = ConvertFromDBVal<int>(dataReader1["IsMailCommunication"]);
                            userProfile.userPreference = userPreference;
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