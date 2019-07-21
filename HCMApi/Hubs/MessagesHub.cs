using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
    }
}
