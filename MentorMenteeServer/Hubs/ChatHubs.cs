using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;



namespace MentorMenteeServer.Hubs
{
     public class ChatHub : Hub
     {
        private readonly ILogger<ChatHub> _logger;
        private static readonly ConcurrentDictionary<string, string> _users = new();

        public async Task RegisterUser(string username)
        {
            _users[username] = Context.ConnectionId;
            _logger.LogInformation("User {0} registered with connection {1}", username, Context.ConnectionId);
        }

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
<<<<<<< HEAD
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
=======
>>>>>>> tongtai

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
            var user = _users.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(user))
            {
                _users.TryRemove(user, out _);
                _logger.LogInformation("User {0} disconnected", user);
            }


            _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation("{0} joined group {1}", Context.ConnectionId, groupName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining group {Group}", groupName);
            }
        }



        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
             await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

    }
}
