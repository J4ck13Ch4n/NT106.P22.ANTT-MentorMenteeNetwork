using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MentorMenteeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                .Where(u => u.Username.Contains(query))
                .Select(u => u.Username)
                .Take(12)
                .ToListAsync();

            return Ok(users);
        }
    }
}