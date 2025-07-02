using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BussinessLayer.Hubs
{
    public class ChatHubs : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Send the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override Task OnConnectedAsync()
        {
            // Logic when a new client connects
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // Logic when a client disconnects
            return base.OnDisconnectedAsync(exception);
        }
    }
}
