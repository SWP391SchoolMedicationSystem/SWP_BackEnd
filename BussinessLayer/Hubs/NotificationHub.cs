using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BussinessLayer.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            // Lấy User ID từ ClaimsPrincipal từ JWT (được thêm vào khi đăng nhập)
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userId != null)
            {
                // Thêm người dùng vào nhóm riêng tư dựa trên ID của họ
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                Console.WriteLine($"User {userId} connected with ConnectionId: {Context.ConnectionId}");

                // Có thể thêm vào nhiều group khác nhau
                // Ví dụ: thêm vào group theo role
                if (!string.IsNullOrEmpty(userRole))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Role_{userRole}");
                    Console.WriteLine($"User {userId} added to role group: Role_{userRole}");
                }

                // Ví dụ: thêm vào group chung cho tất cả user
                await Groups.AddToGroupAsync(Context.ConnectionId, "AllUsers");
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

            }
            await base.OnDisconnectedAsync(exception);
        }

        // Phương thức để join thêm group tùy chọn
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Connection {Context.ConnectionId} joined group: {groupName}");
        }

        // Phương thức để leave group
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Connection {Context.ConnectionId} left group: {groupName}");
        }

        // Phương thức để join nhiều group cùng lúc
        public async Task JoinMultipleGroups(List<string> groupNames)
        {
            foreach (var groupName in groupNames)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine($"Connection {Context.ConnectionId} joined group: {groupName}");
            }
        }
    }
}
