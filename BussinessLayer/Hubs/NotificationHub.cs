using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


namespace BussinessLayer.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }


        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation($"SignalR Client connected: {connectionId}");

            // Add client to general notifications group
            await Groups.AddToGroupAsync(connectionId, "GeneralNotifications");

            // Send connection confirmation
            await Clients.Caller.SendAsync("ConnectionConfirmed", new
            {
                ConnectionId = connectionId,
                Message = "Connected to notification hub",
                Timestamp = DateTime.Now
            });

            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation($"SignalR Client disconnected: {connectionId}");

            if (exception != null)
            {
                _logger.LogError(exception, $"SignalR Client disconnected with error: {connectionId}");
            }

            await base.OnDisconnectedAsync(exception);
        }


        // Allow clients to join specific groups
        public async Task JoinParentGroup(string parentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Parent_{parentId}");
            _logger.LogInformation($"Client {Context.ConnectionId} joined parent group: {parentId}");

            await Clients.Caller.SendAsync("GroupJoined", new
            {
                Group = $"Parent_{parentId}",
                Message = "Successfully joined parent group"
            });
        }


        public async Task JoinStaffGroup(string staffId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Staff_{staffId}");
            _logger.LogInformation($"Client {Context.ConnectionId} joined staff group: {staffId}");

            await Clients.Caller.SendAsync("GroupJoined", new
            {
                Group = $"Staff_{staffId}",
                Message = "Successfully joined staff group"
            });
        }


        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation($"Client {Context.ConnectionId} left group: {groupName}");

            await Clients.Caller.SendAsync("GroupLeft", new
            {
                Group = groupName,
                Message = "Successfully left group"
            });
        }


        // Test method for debugging
        public async Task SendTestMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveTestMessage", new
            {
                Message = message ?? "Test message from SignalR Hub",
                Timestamp = DateTime.Now,
                ConnectionId = Context.ConnectionId,
                Source = "SignalR Hub"
            });

            _logger.LogInformation($"Test message sent from connection: {Context.ConnectionId}");
        }


        // Ping method to test connection
        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong", new
            {
                Message = "Pong from server",
                Timestamp = DateTime.Now,
                ConnectionId = Context.ConnectionId
            });
        }
    }
}
