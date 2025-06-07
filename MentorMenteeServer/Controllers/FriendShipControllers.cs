using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentorMenteeServer.Controllers
{
    [Route("api/friendship")]
    [ApiController]
    [Authorize]
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

            // Tìm existing relationship cả 2 chiều
            var existing = await _context.Relationships
                .FirstOrDefaultAsync(r =>
                    (r.UserId == request.SenderId && r.FriendId == request.ReceiverId) ||
                    (r.UserId == request.ReceiverId && r.FriendId == request.SenderId));

            if (existing != null)
            {
                if (existing.Status == "pending")
                    return BadRequest("Đã có lời mời đang chờ xử lý!");
                if (existing.Status == "accepted")
                    return BadRequest("Hai người đã là bạn!");
                if (existing.Status == "rejected")
                    return BadRequest("Lời mời trước đó đã bị từ chối!");
            }

            var relationship = new Relationship
            {
                UserId = request.SenderId,
                FriendId = request.ReceiverId,
                Status = "pending",
                User = sender,
                Friend = receiver
            };

            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã gửi lời mời kết bạn!");
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendRequestDto request)
        {
            // Tìm relationship đúng chiều
            var relationship = await _context.Relationships
                .FirstOrDefaultAsync(r =>
                    r.UserId == request.SenderId && r.FriendId == request.ReceiverId && r.Status == "pending");

            if (relationship == null)
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
                    r.UserId == request.SenderId && r.FriendId == request.ReceiverId && r.Status == "pending");

            if (relationship == null)
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
                    ((r.UserId == request.SenderId && r.FriendId == request.ReceiverId) ||
                     (r.UserId == request.ReceiverId && r.FriendId == request.SenderId)) &&
                    r.Status == "accepted");

            if (relationship == null)
                return BadRequest("Không phải bạn bè!");

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return Ok("Đã hủy kết bạn!");
        }

        [HttpGet("friends/{userId}")]
        public async Task<IActionResult> GetFriends(int userId)
        {
            var friends = await _context.Relationships
                .Where(r => (r.UserId == userId || r.FriendId == userId) && r.Status == "accepted")
                .Select(r => new
                {
                    FriendId = r.UserId == userId ? r.FriendId : r.UserId,
                    FriendName = r.UserId == userId ? r.Friend.Username : r.User.Username
                })
                .ToListAsync();
            return Ok(friends);
        }

        [HttpGet("pending-request/{userId}")]
        public async Task<IActionResult> GetPendingRequests(int userId)
        {
            var pendingRequests = await _context.Relationships
                .Where(r => r.FriendId == userId && r.Status == "pending")
                .Select(r => new
                {
                    SenderId = r.UserId,
                    SenderName = r.User.Username
                })
                .ToListAsync();
            return Ok(pendingRequests);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetRelationshipStatus([FromQuery] int userId, [FromQuery] int friendId)
        {
            var relationship = await _context.Relationships.FirstOrDefaultAsync(r =>
                (r.UserId == userId && r.FriendId == friendId) ||
                (r.UserId == friendId && r.FriendId == userId));
            if (relationship == null)
                return Ok("none");
            return Ok(relationship.Status);
        }
    }
}