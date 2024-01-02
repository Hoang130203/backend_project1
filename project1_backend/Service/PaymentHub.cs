using Microsoft.AspNetCore.SignalR;

namespace project1_backend.Service
{
    public class PaymentHub : Hub
    {
        private static readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();
        public async Task SendPaymentConfirmationToClient(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("PaymentConfirmed");
        }
        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }
         public override async Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;
        // Lưu ConnectionId của người dùng khi họ kết nối đến SignalR Hub
        _userConnections[Context.User.Identity.Name] = connectionId; // Lưu ConnectionId theo tên người dùng hoặc theo một định danh duy nhất

        await base.OnConnectedAsync();
    }

    public static string GetConnectionIdForUser(string username)
    {
        if (_userConnections.TryGetValue(username, out string connectionId))
        {
            return connectionId;
        }
        return null; // Trả về null nếu không tìm thấy ConnectionId cho người dùng
    }
    }

}
