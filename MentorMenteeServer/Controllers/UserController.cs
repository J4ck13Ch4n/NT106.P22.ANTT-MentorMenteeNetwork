using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MentorMenteeServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize] 
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<string>>> SearchUsers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<string>());
            }

            var currentUsername = User.Identity?.Name;

            var queryableUsers = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(currentUsername))
            {
                queryableUsers = queryableUsers.Where(u => u.Username != currentUsername);
            }

            var users = await queryableUsers
                .Where(u => u.Username.Contains(query) || u.Role.Contains(query) || u.Email.Contains(query))
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Role,
                    u.Email
                })
                .Take(12)
                .ToListAsync();

            return Ok(users);
        }


        public class MentorDto
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }

        [HttpGet("search/mentor")]
        public async Task<ActionResult<IEnumerable<MentorDto>>> SearchMentors()
        {
            var mentorsQuery = _context.Users
                .Where(u => u.Role == "Mentor");

            var mentors = await mentorsQuery
                .Select(u => new MentorDto
                {
                    Id = u.Id,
                    Username = u.Username
                })
                .ToListAsync();

            return Ok(mentors);
        }

        // api/User/me
        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateMe([FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Lấy userId từ JWT (giả định claim name là "id")
            var userIdStr = User.FindFirst("id")?.Value;
            if (userIdStr == null) return Unauthorized();

            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            // Chỉ cập nhật các field được phép sửa
            if (!string.IsNullOrEmpty(dto.Username)) user.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Role)) user.Role = dto.Role;
            if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
            // Kiểm tra Email
            if (!string.IsNullOrEmpty(dto.Email))
            {
                if (!IsValidEmail(dto.Email))
                    return BadRequest(new { message = "Invalid email format" });
                var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != userId);
                if (emailExists)
                    return BadRequest(new { message = "Email already exists" });
                user.Email = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.))
                if (!string.IsNullOrEmpty(dto.Bio)) user.Bio = dto.Bio;


            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}