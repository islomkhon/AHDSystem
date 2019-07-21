using HCMApi.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class MessagesRepository
    {
        readonly static string _connString = Startup.ConnectionString;
        public static IEnumerable<Messages> GetAllUnreadMessages()
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [MessageID], [Message], [EmptyMessage], [Processed], [Date] FROM [dbo].[NeuMessages] 
                    ORDER BY [Date] DESC;", connection))
                {
                    //command.Parameters.AddWithValue("@UserId", userId);
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    //int limit = 0;
                    //while (reader.Read())
                    //{
                    //    messages.Add(item: new Messages { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], EmptyMessage = reader["EmptyMessage"] != DBNull.Value ? (string)reader["EmptyMessage"] : "", Processed = (int)reader["Processed"], MessageDate = Convert.ToDateTime(reader["Date"]) });
                    //    limit++;
                    //}
                }

            }
            return messages;


        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessagesAsync();
                GetAllUnreadMessages();
            }
        }

        //readonly string _connString = Startup.ConnectionString;
        //public IEnumerable<Messages> GetAllUnreadMessages(int userId)
        //{
        //    var messages = new List<Messages>();
        //    using (var connection = new SqlConnection(_connString))
        //    {
        //        connection.Open();
        //        using (var command = new SqlCommand(@"SELECT [MessageID], [Message], [EmptyMessage], [Processed], [Date] FROM [dbo].[NeuMessages] 
        //            WHERE [UserId] = @UserId ORDER BY [Date] DESC;", connection))
        //        {
        //            command.Parameters.AddWithValue("@UserId", userId);
        //            command.Notification = null;

        //            var dependency = new SqlDependency(command);
        //            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();

        //            var reader = command.ExecuteReader();
        //            int limit = 0;
        //            while (reader.Read())
        //            {
        //                messages.Add(item: new Messages { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], EmptyMessage = reader["EmptyMessage"] != DBNull.Value ? (string)reader["EmptyMessage"] : "", Processed = (int)reader["Processed"], MessageDate = Convert.ToDateTime(reader["Date"]) });
        //                limit++;
        //            }
        //        }

        //    }
        //    return messages;


        //}

        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {

        //        //MessagesHub.SendMessages();
        //    }
        //}
    }
}
