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
        private static readonly ConcurrentDictionary<string, string> _usersOnline = new ConcurrentDictionary<string, string>();

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

            _usersOnline[username] = Context.ConnectionId;
            _logger.LogInformation("User {Username} registered with connection {ConnectionId}", username, Context.ConnectionId);

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userEntity != null)
            {
                // Lấy danh sách partners trực tiếp từ _context
                var sentToUsernamesQuery = _context.Messages
                    .Where(m => m.SenderId == userEntity.Id && m.ReceiverId != null && m.GroupId == null && m.ReceiverId != userEntity.Id)
                    .Select(m => m.Receiver.Username);
                var receivedFromUsernamesQuery = _context.Messages
                    .Where(m => m.ReceiverId == userEntity.Id && m.SenderId != null && m.GroupId == null && m.SenderId != userEntity.Id)
                    .Select(m => m.Sender.Username);
                
                var sentToUsernames = await sentToUsernamesQuery.Distinct().ToListAsync();
                var receivedFromUsernames = await receivedFromUsernamesQuery.Distinct().ToListAsync();
                var partners = sentToUsernames.Union(receivedFromUsernames).Where(u => u != null).Distinct().ToList();

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
            var username = _usersOnline.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(username))
            {
                _usersOnline.TryRemove(username, out _);
                _logger.LogInformation("User {Username} disconnected.", username);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendPrivateMessage(string senderUsername, string recipientUsername, string messageContent)
        {
            try
            {
                _logger.LogInformation("User {SenderUsername} attempting to send message to {RecipientUsername}: {Message}", senderUsername, recipientUsername, messageContent);

                var senderUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == senderUsername);
                var recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == recipientUsername);

                if (senderUser == null || (recipientUsername != senderUsername && recipientUser == null))
                {
                    _logger.LogWarning("Sender or Recipient user not found. Sender: {SenderUsername}, Recipient: {RecipientUsername}", senderUsername, recipientUsername);
                    await Clients.Caller.SendAsync("ReceiveMessage", "Hệ thống", "Lỗi: Không tìm thấy người dùng.", recipientUsername, DateTime.UtcNow);
                    return;
                }

                var messageToSave = new Message
                {
                    Sender = senderUser, 
                    SenderId = senderUser.Id,
                    ReceiverId = (senderUsername != recipientUsername) ? recipientUser?.Id : senderUser.Id,
                    MessageText = messageContent,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,
                    EncryptedContent = null, // Sẽ cập nhật khi làm phần mã hóa
                    EncryptedSymmetricKey = null,
                    InitializationVector = null
                };
                if (senderUsername == recipientUsername)
                {
                    messageToSave.ReceiverId = senderUser.Id;
                }

                _context.Messages.Add(messageToSave);
                await _context.SaveChangesAsync(); // Lưu vào CSDL
                _logger.LogInformation("Message from {SenderUsername} to {RecipientUsername} saved to DB with ID {MessageId}.", senderUsername, recipientUsername, messageToSave.Id);

                // Gửi tin nhắn tới người nhận nếu họ online
                if (senderUsername != recipientUsername && recipientUser != null)
                {
                    if (_usersOnline.TryGetValue(recipientUsername, out string recipientConnectionId))
                    {
                        _logger.LogInformation("Recipient {RecipientUsername} is online. Sending message.", recipientUsername);
                        await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", senderUsername, messageContent, senderUsername, messageToSave.CreatedAt);
                    }
                }
                // Gửi lại tin nhắn cho người gửi (Caller)
                _logger.LogInformation("Sending message back to sender {SenderUsername}.", senderUsername);
                await Clients.Caller.SendAsync("ReceiveMessage", senderUsername, messageContent, recipientUsername, messageToSave.CreatedAt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendPrivateMessage from {SenderUsername} to {RecipientUsername}", senderUsername, recipientUsername);
                await Clients.Caller.SendAsync("ReceiveMessage", "Hệ thống", "Lỗi khi gửi tin nhắn.", recipientUsername, DateTime.UtcNow);
            }
        }

        public async Task LoadChatHistory(string partnerUsername)
        {
            var currentUsername = _usersOnline.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (string.IsNullOrEmpty(currentUsername))
            {
                _logger.LogWarning("Could not find username for connection {ConnectionId} requesting chat history.", Context.ConnectionId);
                return;
            }
             _logger.LogInformation("User {CurrentUsername} requesting chat history with {PartnerUsername}", currentUsername, partnerUsername);

            var currentUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUsername);
            var partnerUserEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == partnerUsername);

            if (currentUserEntity == null || partnerUserEntity == null)
            {
                 _logger.LogWarning("User not found for history: Current: {CurrentUsername}, Partner: {PartnerUsername}", currentUsername, partnerUsername);
                await Clients.Caller.SendAsync("ReceiveChatHistory", partnerUsername, new List<object>(), "Lỗi: Không tìm thấy người dùng.");
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
                .Select(m => new { 
                    SenderUsername = m.Sender.Username,
                    Content = m.MessageText, 
                    Timestamp = m.CreatedAt
                })
                .ToListAsync();
            
            await Clients.Caller.SendAsync("ReceiveChatHistory", partnerUsername, messagesFromDb, null);
            _logger.LogInformation("Sent chat history between {CurrentUsername} and {PartnerUsername} to {CurrentUsername}.", currentUsername, partnerUsername, currentUsername);
        }
    }
}