using Microsoft.AspNetCore.Mvc;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace MentorMenteeServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public class LoginResult
        {
            public bool Success { get; set; }
            public string UserId { get; set; }
            public string FullName { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                    return BadRequest("Email đã được sử dụng!");

                string? avatarFilePath = null;

                if (!string.IsNullOrEmpty(registerDto.Avatar))
                {
                    byte[] avatarBytes = Convert.FromBase64String(registerDto.Avatar);
                    avatarFilePath = Path.Combine("Client/Avatar", $"{Guid.NewGuid()}.png");
                    await System.IO.File.WriteAllBytesAsync(avatarFilePath, avatarBytes);
                }

                var user = new User
                {
                    Email = registerDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    Username = registerDto.Username,
                    Gender = registerDto.Gender,
                    Role = registerDto.Role,
                    AvatarPath = avatarFilePath // Lưu đường dẫn ảnh vào database
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("Đăng ký thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ!");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Unauthorized("Email hoặc mật khẩu không đúng!");

            user.IsOnline = true;  //lưu trạng thái online
            await _context.SaveChangesAsync();

            return Ok(new LoginResult
            {
                Success = true,
                UserId = user.Id.ToString(),
                FullName = user.Username
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Không tìm thấy người dùng.");

            user.IsOnline = false;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Đăng xuất thành công!" });
        }


        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Server is running!");
        }
    }
}