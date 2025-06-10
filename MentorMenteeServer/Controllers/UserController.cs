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
    //[Authorize] // <-- Bỏ hoặc comment dòng này để cho phép truy cập không cần đăng nhập
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
    }
}