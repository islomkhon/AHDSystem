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

        public int updateNeuRequestAccessLogs(NueRequestAceessLog nueRequestAceessLog)
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

        public int updateNeuRequestStatusLogs(NueRequestMaster nueRequestMaster)
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

        public int addRequestComment(NueRequestActivity nueRequestActivity)
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

        public NeuLeaveCancelationModal getNeuLeaveCancelationDetails(string requestId)
        {
            NeuLeaveCancelationModal neuLeaveCancelationModal = new NeuLeaveCancelationModal();
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
                            neuLeaveCancelationModal = new NeuLeaveCancelationModal();
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

        public List<NueRequestActivityModel> getRequestLogs(string requestId)
        {
            List<NueRequestActivityModel> nueRequestActivityModels = new List<NueRequestActivityModel>();
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
                            NueRequestActivityModel nueRequestActivityModel = new NueRequestActivityModel();
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

        public List<NueRequestAceessLog> getRequestAccessList(string requestId)
        {
            List<NueRequestAceessLog> nueRequestAceessLogs = new List<NueRequestAceessLog>();
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
                            NueRequestAceessLog nueRequestAceessLog = new NueRequestAceessLog();
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

        public NueRequestActivityMaster getRequestStatus(string statusType)
        {
            NueRequestActivityMaster nueRequestActivityMaster = new NueRequestActivityMaster();
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
                            nueRequestActivityMaster = new NueRequestActivityMaster();
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

        public NueRequestActivityMaster getRequestActivityMasterId(string activityType)
        {
            NueRequestActivityMaster nueRequestActivityMaster = new NueRequestActivityMaster();
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
                            nueRequestActivityMaster = new NueRequestActivityMaster();
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
                                FROM NueUserProfile as NP join EmploymentStatus as ems on NP.EmploymentStatus = ems.Id 
                                join Practice as pc on NP.Practice = pc.Id 
                                join JobLevel as jl on NP.JobLevel = jl.Id 
                                join Designation as ds on NP.Designation = ds.Id";
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