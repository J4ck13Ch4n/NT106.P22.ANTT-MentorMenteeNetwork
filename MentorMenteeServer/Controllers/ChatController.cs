using Microsoft.AspNetCore.Mvc;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentorMenteeServer.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        // Lấy lịch sử tin nhắn
        [HttpGet("history")]
        public async Task<IActionResult> GetChatHistory()
        {
            var messages = await _context.Messages.OrderBy(m => m.CreatedAt).ToListAsync();
            return Ok(messages);
        }

        // Gửi tin nhắn
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (message == null)
                return BadRequest("Invalid message");

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        // Tạo nhóm chat
        [HttpPost("groups/create")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupChat group)
        {
            _context.GroupChats.Add(group);
            await _context.SaveChangesAsync();
            return Ok(group);
        }

        // Lấy tin nhắn trong nhóm
        [HttpGet("groups/{groupId}/messages")]
        public async Task<IActionResult> GetGroupMessages(int groupId)
        {
            var messages = await _context.Messages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
            return Ok(messages);
        }

        // Đăng ký người dùng
        [HttpPost("users/register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // Lấy trạng thái người dùng
        [HttpGet("users/status/{userId}")]
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