    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    namespace MentorMenteeServer.Hubs
    {
        public class ChatHub : Hub
        {
            private readonly ILogger<ChatHub> _logger;

            public ChatHub(ILogger<ChatHub> logger)
            {
                _logger = logger;
            }

            public async Task SendMessage(string user, string message)
            {
                try
                {
                    await Clients.All.SendAsync("ReceiveMessage", user, message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending message from {User}", user);
                }
            }

            public override async Task OnConnectedAsync()
            {
                _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
                await base.OnConnectedAsync();
            }

            public override async Task OnDisconnectedAsync(Exception? exception)
            {
                _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
                await base.OnDisconnectedAsync(exception);
            }

            public async Task JoinGroup(string groupName)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation("{0} joined group {1}", Context.ConnectionId, groupName);
            }

            public async Task SendMessageToGroup(string groupName, string message)
            {
                await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.ConnectionId, message);
            }


    }
}
