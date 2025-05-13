using Microsoft.AspNetCore.Mvc;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Text.RegularExpressions;

namespace MentorMenteeServer.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        // Phương thức để lấy lịch sử tin nhắn
        [HttpGet("history")]
        public async Task<IActionResult> GetChatHistory()
        {
            var messages = await _context.Messages.OrderBy(m => m.CreatedAt).ToListAsync();
            return Ok(messages);
        }

        // Phương thức để gửi tin nhắn (nếu không dùng SignalR)
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (message == null)
                return BadRequest("Invalid message");

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        [Route("api/chat/groups")]
        public class GroupController : ControllerBase
        {
            private readonly AppDbContext _context;

            public GroupController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("create")]
            public async Task<IActionResult> CreateGroup([FromBody] GroupChat group)
            {
                _context.GroupChats.Add(group);
                await _context.SaveChangesAsync();
                return Ok(group);
            }

            [HttpGet("{groupId}/messages")]
            public async Task<IActionResult> GetGroupMessages(int groupId)
            {
                var messages = await _context.Messages
                    .Where(m => m.GroupId == groupId)
                    .OrderBy(m => m.CreatedAt)
                    .ToListAsync();
                return Ok(messages);
            }
        }

        [Route("api/chat/users")]
        public class UserController : ControllerBase
        {
            private readonly AppDbContext _context;

            public UserController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }

            [HttpGet("status/{userId}")]
            public IActionResult GetUserStatus(int userId)
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user.IsOnline);
            }
        }


    }

}