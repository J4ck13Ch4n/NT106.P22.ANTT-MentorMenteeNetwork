using MentorMenteeServer.Data;  
using MentorMenteeServer.Hubs;   
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;

namespace MentorMenteeServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly AppDbContext _context;
        private static readonly ConcurrentDictionary<int, string> _usersOnlineById = new ConcurrentDictionary<int, string>();

        public ChatHub(ILogger<ChatHub> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task RegisterUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return;

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userEntity != null)
            {
                _usersOnlineById[userEntity.Id] = Context.ConnectionId;
                _logger.LogInformation("User {Username} (Id: {Id}) registered with connection {ConnectionId}", username, userEntity.Id, Context.ConnectionId);

                // Lấy danh sách partner (cả Id và Username)
                var sentToPartners = _context.Messages
                    .Where(m => m.SenderId == userEntity.Id && m.ReceiverId != null && m.GroupId == null && m.ReceiverId != userEntity.Id)
                    .Select(m => new { Id = m.Receiver.Id, Username = m.Receiver.Username });
                var receivedFromPartners = _context.Messages
                    .Where(m => m.ReceiverId == userEntity.Id && m.SenderId != null && m.GroupId == null && m.SenderId != userEntity.Id)
                    .Select(m => new { Id = m.Sender.Id, Username = m.Sender.Username });

                // Hợp nhất, loại trùng, loại null, loại chính mình
                var partners = await sentToPartners
                    .Concat(receivedFromPartners)
                    .Where(p => p.Id != userEntity.Id && p.Username != null)
                    .GroupBy(p => p.Id)
                    .Select(g => new PartnerInfo
                    {
                        Id = g.Key,
                        Username = g.First().Username
                    })
                    .ToListAsync();

                await Clients.Caller.SendAsync("ReceiveConversationPartners", partners);
                _logger.LogInformation("Sent conversation partners to {Username}", username);
            }
            else
            {
                _logger.LogWarning("User {Username} not found in DB during registration.", username);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = _usersOnlineById.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (userId != 0)
            {
                _usersOnlineById.TryRemove(userId, out _);
                _logger.LogInformation("UserId {UserId} disconnected.", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendPrivateMessageById(int senderId, int recipientId, string messageContent)
        {
            try
            {
                var senderUser = await _context.Users.FindAsync(senderId);
                var recipientUser = await _context.Users.FindAsync(recipientId);

                if (senderUser == null || recipientUser == null)
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", "Hệ thống", "Lỗi: Không tìm thấy người dùng.", recipientId.ToString(), DateTime.UtcNow);
                    return;
                }

                var messageToSave = new Message
                {
                    Sender = senderUser,
                    SenderId = senderUser.Id,
                    ReceiverId = recipientUser.Id,
                    MessageText = messageContent,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,
                    EncryptedContent = null,
                    EncryptedSymmetricKey = null,
                    InitializationVector = null
                };

                _context.Messages.Add(messageToSave);
                await _context.SaveChangesAsync();

                if (_usersOnlineById.TryGetValue(recipientUser.Id, out string recipientConnectionId))
                {
                    await Clients.Client(recipientConnectionId).SendAsync(
                        "ReceiveMessage",
                        senderUser.Username,
                        messageContent,
                        recipientUser.Username,
                        messageToSave.CreatedAt
                    );
                }

                await Clients.Caller.SendAsync(
                    "ReceiveMessage",
                    senderUser.Username,
                    messageContent,
                    recipientUser.Username,
                    messageToSave.CreatedAt
                );
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Hệ thống", "Lỗi khi gửi tin nhắn.", recipientId.ToString(), DateTime.UtcNow);
            }
        }

        public async Task LoadChatHistory(int partnerId)
        {
            var currentId = _usersOnlineById.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (currentId == 0)
            {
                _logger.LogWarning("Could not find username for connection {ConnectionId} requesting chat history.", Context.ConnectionId);
                return;
            }
            _logger.LogInformation("User {CurrentId} requesting chat history with {PartnerId}", currentId, partnerId);

            var currentUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentId);
            var partnerUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == partnerId);

            if (currentUserEntity == null || partnerUserEntity == null)
            {
                _logger.LogWarning("User not found for history: Current: {CurrentId}, Partner: {PartnerId}", currentId, partnerId);
                await Clients.Caller.SendAsync("ReceiveChatHistory", partnerId, new List<object>(), "Lỗi: Không tìm thấy người dùng.");
                return;
            }

            var messagesFromDb = await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.GroupId == null &&
                            ((m.SenderId == currentUserEntity.Id && m.ReceiverId == partnerUserEntity.Id) ||
                             (m.SenderId == partnerUserEntity.Id && m.ReceiverId == currentUserEntity.Id)))
                .OrderByDescending(m => m.CreatedAt)
                .Take(50)
                .OrderBy(m => m.CreatedAt)
                .Select(m => new MessageEntry
                {
                    SenderUsername = m.Sender.Username,
                    Content = m.MessageText,
                    Timestamp = m.CreatedAt
                })
                .ToListAsync();

            await Clients.Caller.SendAsync("ReceiveChatHistory", partnerId, messagesFromDb, null);
            _logger.LogInformation("Sent chat history between {CurrentId} and {PartnerId} to {CurrentId}.", currentId, partnerId, currentId);
        }
        
        public async Task NotifyGoalUpdated(int menteeId)
        {
            await Clients.Group($"user_{menteeId}").SendAsync("ReceiveGoalUpdate");
        }
        
        // Định nghĩa PartnerInfo cho truyền SignalR
        public class PartnerInfo
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }

        // Định nghĩa MessageEntry cho truyền SignalR
        public class MessageEntry
        {
            public string SenderUsername { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}