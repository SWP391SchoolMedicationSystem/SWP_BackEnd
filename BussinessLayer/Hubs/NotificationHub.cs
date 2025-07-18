using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
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
            // Lấy User ID từ ClaimsPrincipal từ JWT (được thêm vào khi đăng nhập)
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userId != null)
            {
                // Thêm người dùng vào nhóm riêng tư dựa trên ID của họ
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                _logger.LogInformation("User {UserId} connected with ConnectionId: {ConnectionId}", userId, Context.ConnectionId);

                // Có thể thêm vào nhiều group khác nhau
                // Ví dụ: thêm vào group theo role
                if (!string.IsNullOrEmpty(userRole) && userRole == "Parent")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Parents");
                    _logger.LogInformation("User {UserId} added to role group: Parents", userId);
                }
                if (!string.IsNullOrEmpty(userRole) && userRole == "Nurse")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Staffs");
                    _logger.LogInformation("User {UserId} added to role group: Staffs", userId);
                }
                // Ví dụ: thêm vào group chung cho tất cả user
                await Groups.AddToGroupAsync(Context.ConnectionId, "AllUsers");
                _logger.LogInformation("User {UserId} added to general group: AllUsers", userId);
            }
            else
            {
                _logger.LogWarning("User connected without valid UserId. ConnectionId: {ConnectionId}", Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        // Khi một client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userId != null)
            {
                // Remove từ tất cả các group mà user đã join
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

                if (!string.IsNullOrEmpty(userRole))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Role_{userRole}");
                }

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AllUsers");

                _logger.LogInformation("User {UserId} disconnected from ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
            }
            else
            {
                _logger.LogWarning("User disconnected without valid UserId. ConnectionId: {ConnectionId}", Context.ConnectionId);
            }

            if (exception != null)
            {
                _logger.LogError(exception, "User {UserId} disconnected with exception. ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Phương thức để join thêm group tùy chọn
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("Connection {ConnectionId} joined group: {GroupName}", Context.ConnectionId, groupName);
        }

        // Phương thức để leave group
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("Connection {ConnectionId} left group: {GroupName}", Context.ConnectionId, groupName);
        }

        // Phương thức để join nhiều group cùng lúc
        public async Task JoinMultipleGroups(List<string> groupNames)
        {
            foreach (var groupName in groupNames)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation("Connection {ConnectionId} joined group: {GroupName}", Context.ConnectionId, groupName);
            }
        }
    }
}
