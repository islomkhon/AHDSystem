using NeuRequest.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NeuRequest.Models
{
    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public IEnumerable<Messages> GetAllUnreadMessages(int userId)
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [MessageID], [Message], [EmptyMessage], [Processed], [Date] FROM [dbo].[NeuMessages] 
                    WHERE [UserId] = @UserId ORDER BY [Date] DESC;", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    int limit = 0;
                    while (reader.Read())
                    {
                        messages.Add(item: new Messages { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], EmptyMessage = reader["EmptyMessage"] != DBNull.Value ? (string)reader["EmptyMessage"] : "", Processed = (int)reader["Processed"], MessageDate = Convert.ToDateTime(reader["Date"]) });
                        limit++;
                    }
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
        //public IEnumerable<Messages> GetUnseenMessages()
        //{
        //    var messages = new List<Messages>();
        //    using (var connection = new SqlConnection(_connString))
        //    {
        //        connection.Open();
        //        using (var command = new SqlCommand(@"SELECT m.Id, m.Message, m.MessageBody, m.UserId, 
        //                nup.FullName, nup.NTPLID, m.Seen, m.AddedOn, m.ModifiedOn  FROM Messages m 
        //                join NueUserProfile nup on m.UserId = nup.Id
        //                Where Seen = @Seen", connection))
        //        {
        //            command.Parameters.AddWithValue("@Seen", 0);
        //            command.Notification = null;

        //            var dependency = new SqlDependency(command);
        //            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();

        //            var reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                messages.Add(item: new Messages { Id = (int)reader["Id"], Message = (string)reader["Message"],
        //                    MessageBody = reader["MessageBody"] != DBNull.Value ? (string)reader["MessageBody"] : "",
        //                    UserId = (int)reader["UserId"],
        //                    FullName = reader["FullName"] != DBNull.Value ? (string)reader["FullName"] : "",
        //                    NTPLID = reader["NTPLID"] != DBNull.Value ? (string)reader["NTPLID"] : "",
        //                    Seen = (int)reader["Seen"],
        //                    AddedOn = Convert.ToDateTime(reader["AddedOn"]), ModifiedOn = Convert.ToDateTime(reader["ModifiedOn"]) });
        //            }
        //        }

        //    }
        //    return messages;


        //}

        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        MessagesHub.SendMessages();
        //    }
        //}
    }

}