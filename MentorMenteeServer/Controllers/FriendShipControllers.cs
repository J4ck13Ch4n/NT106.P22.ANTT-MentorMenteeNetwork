using MentorMenteeServer.Data;
using MentorMenteeServer.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Gửi lời mời kết bạn
        [HttpPost("send-request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
        {
            if (request.SenderId == request.ReceiverId)
                return BadRequest("Không thể kết bạn với chính mình!");

            var existingRequest = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.SenderId == request.SenderId && f.ReceiverId == request.ReceiverId) ||
                    (f.SenderId == request.ReceiverId && f.ReceiverId == request.SenderId));

            if (existingRequest != null)
                return BadRequest("Đã có yêu cầu kết bạn trước đó!");

            var friendship = new Friendship
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Status = FriendshipStatus.Pending
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return Ok("Đã gửi lời mời kết bạn!");
        }

        // Chấp nhận lời mời kết bạn
        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendRequestDto request)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.SenderId == request.SenderId && f.ReceiverId == request.ReceiverId);

            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                return BadRequest("Lời mời không hợp lệ!");

            friendship.Status = FriendshipStatus.Accepted;
            await _context.SaveChangesAsync();

            return Ok("Đã chấp nhận lời mời kết bạn!");
        }

        // Từ chối lời mời kết bạn
        [HttpPost("reject-request")]
        public async Task<IActionResult> RejectFriendRequest([FromBody] FriendRequestDto request)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.SenderId == request.SenderId && f.ReceiverId == request.ReceiverId);

            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                return BadRequest("Lời mời không hợp lệ!");

            friendship.Status = FriendshipStatus.Rejected;
            await _context.SaveChangesAsync();

            return Ok("Đã từ chối lời mời kết bạn!");
        }

        // Hủy kết bạn
        [HttpPost("remove-friend")]
        public async Task<IActionResult> RemoveFriend([FromBody] FriendRequestDto request)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.SenderId == request.SenderId && f.ReceiverId == request.ReceiverId) ||
                    (f.SenderId == request.ReceiverId && f.ReceiverId == request.SenderId));

            if (friendship == null || friendship.Status != FriendshipStatus.Accepted)
                return BadRequest("Không phải bạn bè!");

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return Ok("Đã hủy kết bạn!");
        }
        [HttpPost("send-friend-request-by-name")]
        public async Task<IActionResult> SendFriendRequestByName([FromBody] FriendRequestDto request)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.SenderUsername);
            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.ReceiverUsername);

            if (sender == null || receiver == null)
                return NotFound("Người dùng không tồn tại!");

            // Kiểm tra nếu đã là bạn bè
            var existingFriendship = await _context.Friendships
                .FirstOrDefaultAsync(f => (f.SenderId == sender.Id && f.ReceiverId == receiver.Id) ||
                                          (f.ReceiverId == receiver.Id && f.SenderId == sender.Id));

            if (existingFriendship != null)
                return BadRequest("Hai người đã là bạn bè!");

            // Gửi yêu cầu kết bạn
            _context.FriendRequests.Add(new FriendRequest
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Status = "Pending"
            });
            await _context.SaveChangesAsync();

            return Ok("Lời mời kết bạn đã được gửi!");
        }
    }
}
