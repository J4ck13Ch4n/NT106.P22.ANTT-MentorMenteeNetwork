using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using System; 


namespace MentorMenteeServer.Hubs
{
     public class ChatHub : Hub
     {
        private readonly ILogger<ChatHub> _logger;
        private static readonly ConcurrentDictionary<string, string> _users = new();

        public ChatHub(ILogger<ChatHub> logger) 
        {
            _logger = logger;
        }

        public async Task RegisterUser(string username)
        {
            var connectionId = Context.ConnectionId;
            _users[username] = connectionId;
            _logger.LogInformation("User {Username} registered with connection {ConnectionId}", username, connectionId);
            await Task.CompletedTask; 
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                _logger.LogInformation("User {User} sending message to all: {Message}", user, message);
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message from {User}", user);
            }
        }

        public async Task SendPrivateMessage(string senderUsername, string recipientUsername, string message)
        {
            try
            {
                _logger.LogInformation("User {SenderUsername} attempting to send private message to {RecipientUsername}: {Message}", senderUsername, recipientUsername, message);
                
                if (senderUsername != recipientUsername)
                {
                    if (_users.TryGetValue(recipientUsername, out string recipientConnectionId))
                    {
                        _logger.LogInformation("Recipient {RecipientUsername} (Connection: {RecipientConnectionId}) is online and different from sender. Sending message.", recipientUsername, recipientConnectionId);
                        await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", senderUsername, message, senderUsername);
                    }
                    else
                    {
                        _logger.LogWarning("Recipient {RecipientUsername} not found. Message will not be delivered to them.", recipientUsername);
                    }
                }
                else
                {
                    _logger.LogInformation("Sender {SenderUsername} is also the recipient. Message will be sent once via Caller.", senderUsername);
                }

                _logger.LogInformation("Sending message back to sender {SenderUsername} (Caller Connection: {ConnectionId}).", senderUsername, Context.ConnectionId);
                await Clients.Caller.SendAsync("ReceiveMessage", senderUsername, message, recipientUsername);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendPrivateMessage from {SenderUsername} to {RecipientUsername}", senderUsername, recipientUsername);
                await Clients.Caller.SendAsync("ReceiveMessage", "Hệ thống", "Lỗi khi gửi tin nhắn riêng.");
            }
        }


        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var userEntry = _users.FirstOrDefault(x => x.Value == connectionId);
            
            if (!string.IsNullOrEmpty(userEntry.Key)) 
            {
                if (_users.TryRemove(userEntry.Key, out _))
                {
                    _logger.LogInformation("User {User} (Connection: {ConnectionId}) disconnected and removed.", userEntry.Key, connectionId);
                }
                else
                {
                    _logger.LogWarning("Failed to remove user for ConnectionId {ConnectionId} on disconnect.", connectionId);
                }
            }
            else
            {
                _logger.LogInformation("Client {ConnectionId} disconnected (was not registered with a username or already removed).", connectionId);
            }
            
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation("Connection {ConnectionId} joined group {GroupName}", Context.ConnectionId, groupName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining group {GroupName} for Connection {ConnectionId}", groupName, Context.ConnectionId);
            }
        }

        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            try
            {
                _logger.LogInformation("User {User} sending message to group {GroupName}: {Message}", user, groupName, message);
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to group {GroupName} from {User}", groupName, user);
            }
        }
    }
}