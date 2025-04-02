using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentorMenteeServer.Controllers
{
    [Route("api/friendship")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FriendshipController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
        {
            if (request.SenderId == request.ReceiverId)
                return BadRequest("Không thể kết bạn với chính mình!");

            var sender = await _context.Users.FindAsync(request.SenderId);
            var receiver = await _context.Users.FindAsync(request.ReceiverId);

            if (sender == null || receiver == null)
                return NotFound("Người dùng không tồn tại!");

            var existingRequest = await _context.Relationships
                .FirstOrDefaultAsync(r => (r.Mentor.Id == request.SenderId && r.Mentee.Id == request.ReceiverId) ||
                                          (r.Mentor.Id == request.ReceiverId && r.Mentee.Id == request.SenderId));

            if (existingRequest != null)
                return BadRequest("Đã có yêu cầu kết bạn trước đó!");

            var relationship = new Relationship
            {
                Mentor = sender,
                Mentee = receiver,
                Status = "pending"
            };

            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã gửi lời mời kết bạn!");
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendRequestDto request)
        {
            var relationship = await _context.Relationships
                .Include(r => r.Mentor)
                .Include(r => r.Mentee)
                .FirstOrDefaultAsync(r => r.Mentor.Id == request.SenderId && r.Mentee.Id == request.ReceiverId);

            if (relationship == null || relationship.Status != "pending")
                return BadRequest("Lời mời không hợp lệ!");

            relationship.Status = "accepted";
            await _context.SaveChangesAsync();

            return Ok("Đã chấp nhận lời mời kết bạn!");
        }

        [HttpPost("reject-request")]
        public async Task<IActionResult> RejectFriendRequest([FromBody] FriendRequestDto request)
        {
            var relationship = await _context.Relationships
                .Include(r => r.Mentor)
                .Include(r => r.Mentee)
                .FirstOrDefaultAsync(r => r.Mentor.Id == request.SenderId && r.Mentee.Id == request.ReceiverId);

            if (relationship == null || relationship.Status != "pending")
                return BadRequest("Lời mời không hợp lệ!");

            relationship.Status = "rejected";
            await _context.SaveChangesAsync();

            return Ok("Đã từ chối lời mời kết bạn!");
        }

        [HttpPost("remove-friend")]
        public async Task<IActionResult> RemoveFriend([FromBody] FriendRequestDto request)
        {
            var relationship = await _context.Relationships
                .Include(r => r.Mentor)
                .Include(r => r.Mentee)
                .FirstOrDefaultAsync(r => (r.Mentor.Id == request.SenderId && r.Mentee.Id == request.ReceiverId) ||
                                          (r.Mentor.Id == request.ReceiverId && r.Mentee.Id == request.SenderId));

            if (relationship == null || relationship.Status != "accepted")
                return BadRequest("Không phải bạn bè!");

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã hủy kết bạn!");
        }
    }
}
