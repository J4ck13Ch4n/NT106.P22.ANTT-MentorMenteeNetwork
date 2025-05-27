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

            // Tìm existing relationship cả 2 chiều (mentor-mentee và mentee-mentor)
            var existing = await _context.Relationships
                .FirstOrDefaultAsync(r =>
                    (r.MentorId == request.SenderId && r.MenteeId == request.ReceiverId) ||
                    (r.MentorId == request.ReceiverId && r.MenteeId == request.SenderId));

            if (existing != null)
            {
                if (existing.Status == "pending")
                    return BadRequest("Đã có lời mời đang chờ xử lý!");
                if (existing.Status == "accepted")
                    return BadRequest("Hai người đã là bạn!");
                if (existing.Status == "rejected")
                    return BadRequest("Lời mời trước đó đã bị từ chối!");
            }

            Relationship relationship;

            if (sender.Role == "mentor")
            {
                relationship = new Relationship
                {
                    Mentor = sender,
                    MentorId = sender.Id,
                    Mentee = receiver,
                    MenteeId = receiver.Id,
                    Status = "pending"
                };
            }
            else if (sender.Role == "mentee")
            {
                relationship = new Relationship
                {
                    Mentor = receiver,
                    MentorId = receiver.Id,
                    Mentee = sender,
                    MenteeId = sender.Id,
                    Status = "pending"
                };
            }
            else
            {
                return BadRequest("Role người gửi không hợp lệ.");
            }

            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã gửi lời mời kết bạn!");
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendRequestDto request)
        {
            // Tìm relationship theo đúng chiều mentor-mentee
            var relationship = await _context.Relationships
                .FirstOrDefaultAsync(r =>
                    r.MentorId == request.SenderId && r.MenteeId == request.ReceiverId);

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
                .FirstOrDefaultAsync(r =>
                    r.MentorId == request.SenderId && r.MenteeId == request.ReceiverId);

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
                .FirstOrDefaultAsync(r =>
                    (r.MentorId == request.SenderId && r.MenteeId == request.ReceiverId) ||
                    (r.MentorId == request.ReceiverId && r.MenteeId == request.SenderId));

            if (relationship == null || relationship.Status != "accepted")
                return BadRequest("Không phải bạn bè!");

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã hủy kết bạn!");
        }
        [HttpGet("friends/{userId}")]
        public async Task<IActionResult> GetFriends(int userId)
        {
            var friends = await _context.Relationships
                .Where(r => (r.MentorId == userId || r.MenteeId == userId) && r.Status == "accepted")
                .Select(r => new
                {
                    FriendId = r.MentorId == userId ? r.MenteeId : r.MentorId,
                    FriendName = r.MentorId == userId ? r.Mentee.Username : r.Mentor.Username
                })
                .ToListAsync();
            return Ok(friends);
        }
        [HttpGet("pending-request/{userId}")]
        public async Task<IActionResult> GetPendingRequests(int userId)
        {
            var pendingRequests = await _context.Relationships
                .Where(r => r.MenteeId == userId && r.Status == "pending")
                .Select(r => new
                {
                    SenderId = r.MentorId,
                    SenderName = r.Mentor.Username
                })
        }
    }
}