using MentorMenteeServer.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace MentorMenteeServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly AppDbContext _context;
        private static readonly ConcurrentDictionary<int, string> _usersOnlineById = new();

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

        /// <summary>
        /// Đăng ký user, gửi về danh sách cuộc trò chuyện gần nhất (partner).
        /// </summary>
        public async Task RegisterUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return;

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userEntity != null)
            {
                _usersOnlineById[userEntity.Id] = Context.ConnectionId;
                _logger.LogInformation("User {Username} (Id: {Id}) registered with connection {ConnectionId}", username, userEntity.Id, Context.ConnectionId);

                // Lấy danh sách partner (gồm cả username/email)
                var sentToPartners = _context.Messages
                    .Where(m => m.SenderId == userEntity.Id && m.ReceiverId != null && m.GroupId == null && m.ReceiverId != userEntity.Id)
                    .Select(m => new { Id = m.Receiver.Id, Username = m.Receiver.Username, Email = m.Receiver.Email });
                var receivedFromPartners = _context.Messages
                    .Where(m => m.ReceiverId == userEntity.Id && m.SenderId != null && m.GroupId == null && m.SenderId != userEntity.Id)
                    .Select(m => new { Id = m.Sender.Id, Username = m.Sender.Username, Email = m.Sender.Email });

                // Hợp nhất, loại trùng, loại null, loại chính mình
                var partners = await sentToPartners
                    .Concat(receivedFromPartners)
                    .Where(p => p.Id != userEntity.Id && p.Username != null)
                    .GroupBy(p => p.Id)
                    .Select(g => new PartnerInfo
                    {
                        Id = g.Key,
                        Username = g.First().Username,
                        Email = g.First().Email
                    })
                    .ToListAsync();

                await Clients.Caller.SendAsync("ReceiveConversationPartners", partners);
                _logger.LogInformation("Sent conversation partners to {Username}", username);

                await LoadConversations();
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

        /// <summary>
        /// Tìm user theo username (gợi ý tìm kiếm), trả về kèm email cho UI chọn chat.
        /// </summary>
        public async Task SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                await Clients.Caller.SendAsync("ReceiveUserSearchResults", new List<object>());
                return;
            }

            // Có thể giới hạn số lượng kết quả trả về
            var users = await _context.Users
                .Where(u => u.Username.Contains(searchTerm))
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email
                })
                .Take(10)
                .ToListAsync();

            await Clients.Caller.SendAsync("ReceiveUserSearchResults", users);
        }

        /// <summary>
        /// Gửi tin nhắn riêng tư dựa vào userId
        /// </summary>
        public async Task SendPrivateMessageById(int senderId, int recipientId, string messageContent)
        {
            try
            {
                var senderUser = await _context.Users.FindAsync(senderId);
                var recipientUser = await _context.Users.FindAsync(recipientId);

                if (senderUser == null || recipientUser == null)
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", "System", "Error: User not found.", recipientId.ToString(), DateTime.UtcNow);
                    return;
                }

                var messageToSave = new Message
                {
                    Sender = senderUser,
                    SenderId = senderUser.Id,
                    ReceiverId = recipientUser.Id,
                    MessageText = messageContent,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
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
                _logger.LogError(ex, "Error sending message");
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "Error sending message.", recipientId.ToString(), DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Lấy lịch sử trò chuyện với 1 user (theo partnerId)
        /// </summary>
        public async Task LoadChatHistory(int partnerId)
        {
            var currentId = _usersOnlineById.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (currentId == 0)
            {
                _logger.LogWarning("Could not find user for connection {ConnectionId} requesting chat history.", Context.ConnectionId);
                return;
            }

            var currentUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentId);
            var partnerUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == partnerId);

            if (currentUserEntity == null || partnerUserEntity == null)
            {
                _logger.LogWarning("User not found for history: Current: {CurrentId}, Partner: {PartnerId}", currentId, partnerId);
                await Clients.Caller.SendAsync("ReceiveChatHistory", partnerId, new List<object>(), "Error: User not found.");
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
        }

        /// <summary>
        /// Lấy danh sách cuộc trò chuyện của user hiện tại (partner + last message)
        /// </summary>
        public async Task LoadConversations()
        {
            var currentId = _usersOnlineById.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (currentId == 0)
            {
                _logger.LogWarning("Could not find user for connection {ConnectionId} requesting conversations.", Context.ConnectionId);
                return;
            }

            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentId);
            if (currentUser == null) return;

            var latestMsgs = await _context.Messages
                .Where(m => m.GroupId == null &&
                            (m.SenderId == currentId || m.ReceiverId == currentId))
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            var partnerDict = new Dictionary<int, ConversationInfo>();

            foreach (var msg in latestMsgs)
            {
                int partnerId = msg.SenderId == currentId ? msg.ReceiverId.Value : msg.SenderId;
                if (!partnerDict.ContainsKey(partnerId))
                {
                    var partner = await _context.Users.FindAsync(partnerId);
                    if (partner == null) continue;

                    partnerDict[partnerId] = new ConversationInfo
                    {
                        PartnerId = partner.Id,
                        PartnerUsername = partner.Username,
                        PartnerEmail = partner.Email,
                        LastMessage = msg.MessageText,
                        LastMessageTime = msg.CreatedAt
                    };
                }
            }

            await Clients.Caller.SendAsync("ReceiveConversationList", partnerDict.Values.ToList());
        }

        // Định nghĩa các class truyền SignalR
        public class PartnerInfo
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }

        public class MessageEntry
        {
            public string SenderUsername { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
        }

        public class ConversationInfo
        {
            public int PartnerId { get; set; }
            public string PartnerUsername { get; set; }
            public string PartnerEmail { get; set; }
            public string LastMessage { get; set; }
            public DateTime LastMessageTime { get; set; }
        }
    }
}