using HCMApi.DB;
using HCMApi.Modal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Protocols;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAgo;

namespace HCMApi.Hubs
{
    public class MessagesHub : Hub
    {
        //[HubMethodName("sendMessages")]
        //public static void SendMessages()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
        //    context.Clients.All.updateMessages();
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        static MqttFactory factory = new MqttFactory();
        

        internal static async Task SendMessagesAsync()
        {
            var notifications = new DataAccess(Startup.ConnectionString).getNotifications();
            if (notifications != null)
            {
                Dictionary<string, List<MichaeNotificationPayload>> userNotification = new Dictionary<string, List<MichaeNotificationPayload>>();

               
                var messages = from m in notifications orderby m.MessageId descending select m;
                //var messagesForNotification = messages.Take(10);
                List<MichaeNotificationPayload> michaeNotifications = new List<MichaeNotificationPayload>();
                foreach (var item in messages)
                {
                    var notification = new MichaeNotificationPayload()
                    {
                        MessageId = item.MessageId,
                        UserId = (int)item.UserId,
                        EmptyMessage = item.EmptyMessage,
                        Message = item.Message,
                        Target = item.Target,
                        Processed = (int)item.Processed,
                        DateAdded = item.Date.Value.ToLocalTime().TimeAgo()
                    };
                    //michaeNotifications.Add(notification);
                    if(userNotification.Keys.Where(x=>x == notification.UserId.ToString()).Count() <= 0)
                    {
                        userNotification.Add(notification.UserId.ToString(), new List<MichaeNotificationPayload>() { notification });
                    }
                    else
                    {
                        userNotification[notification.UserId.ToString()].Add(notification);
                    }
                }

                int totalUnreadNotification = messages.Count();
                //MichaeNotificationBase michaeNotificationBase = new MichaeNotificationBase() { Count = totalUnreadNotification, MichaeNotificationPayloads = michaeNotifications };

                if (userNotification.Count > 0)
                {
                    await publishNotificationsAsync(userNotification);
                }
            }
        }

        internal static async Task SendMessagesAsync1()
        {
            var notifications = new DataAccess(Startup.ConnectionString).getNotifications();
            if(notifications != null)
            {
                var messages = from m in notifications orderby m.MessageId descending select m;
                var messagesForNotification = messages.Take(10);
                List<MichaeNotificationPayload> michaeNotifications = new List<MichaeNotificationPayload>();
                foreach (var item in messagesForNotification)
                {
                    var notification = new MichaeNotificationPayload()
                    {
                        MessageId = item.MessageId,
                        UserId = (int)item.UserId,
                        EmptyMessage = item.EmptyMessage,
                        Message = item.Message,
                        Target = item.Target,
                        Processed = (int)item.Processed,
                        DateAdded = item.Date.Value.ToLocalTime().TimeAgo()
                    };
                    michaeNotifications.Add(notification);
                    //michaeNotifications.Add(new MichaeNotification()
                    //{
                    //    Channel = item.UserId.ToString(),
                    //    michaeNotificationPayload = new MichaeNotificationPayload()
                    //    {
                    //        MessageId = item.MessageId,
                    //        UserId = (int)item.UserId,
                    //        EmptyMessage = item.EmptyMessage,
                    //        Message = item.Message,
                    //        Target = item.Target,
                    //        Processed = (int)item.Processed,
                    //        DateAdded = item.Date.Value.ToLocalTime().TimeAgo()
                    //    }
                    //});
                }

                int totalUnreadNotification = messages.Count();
                MichaeNotificationBase michaeNotificationBase = new MichaeNotificationBase() { Count = totalUnreadNotification, MichaeNotificationPayloads = michaeNotifications };

                if (totalUnreadNotification > 0)
                {
                    //await publishNotificationsAsync(michaeNotificationBase);
                }
            }
        }

        public static async Task publishNotificationsAsync(Dictionary<string, List<MichaeNotificationPayload>> userNotification)
        {
            try
            {
                var mqttClient = factory.CreateMqttClient();
                var options = new MqttClientOptionsBuilder()
                //.WithTcpServer("localhost", 1883) // Port is optional
                .WithWebSocketServer("ws://localhost:1884")
                .WithCredentials("root", "12345678")
                .Build();
                await mqttClient.ConnectAsync(options);
                foreach (var item in userNotification)
                {
                    MichaeNotificationBase michaeNotificationBase = new MichaeNotificationBase();
                    michaeNotificationBase.Count = new DataAccess(Startup.ConnectionString).getUnreadNotifications(int.Parse(item.Key)).Count;
                    michaeNotificationBase.MichaeNotificationPayloads = item.Value.Take(5).ToList<MichaeNotificationPayload>();
                    var payoad = JsonConvert.SerializeObject(michaeNotificationBase);
                    if (!mqttClient.IsConnected)
                    {
                        await mqttClient.ConnectAsync(options);
                    }

                    var message = new MqttApplicationMessageBuilder()
                    .WithTopic(item.Key)
                    .WithPayload(payoad)
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                        await mqttClient.PublishAsync(message);
                    
                }
            }
            catch (Exception e1) { }
        }

        public static async Task publishNotificationsAsync1(MichaeNotificationBase michaeNotificationBase)
        {
            try
            {
                //var mqttClient = factory.CreateMqttClient();
                //var options = new MqttClientOptionsBuilder()
                ////.WithTcpServer("localhost", 1883) // Port is optional
                //.WithWebSocketServer("ws://localhost:1884")
                //.WithCredentials("root", "12345678")
                //.Build();
                //await mqttClient.ConnectAsync(options);
                //if (!mqttClient.IsConnected)
                //{
                //    await mqttClient.ConnectAsync(options);
                //}
                //var payoad = JsonConvert.SerializeObject(michaeNotifications[i].michaeNotificationPayload);
                //var message = new MqttApplicationMessageBuilder()
                //.WithTopic(michaeNotifications[i].Channel)
                //.WithPayload(payoad)
                //.WithExactlyOnceQoS()
                //.WithRetainFlag()
                //.Build();
                //await mqttClient.PublishAsync(message);
               
            }
            catch (Exception e1)
            {

            }
            
            //int count = 0;
            //var t = DateTime.Now;
            //var t1 = DateTime.Now;
            //while (true)
            //{
            //    count++;
            //    var message = new MqttApplicationMessageBuilder()
            //    .WithTopic(count.ToString())
            //    .WithPayload(t.ToString() + " " + t1.ToString())
            //    .WithExactlyOnceQoS()
            //    .WithRetainFlag()
            //    .Build();

            //    await mqttClient.PublishAsync(message);
            //    if (count == 100001)
            //    {
            //        t1 = DateTime.Now;
            //        count = 0;
            //    }

            //}
        }
    }
}
