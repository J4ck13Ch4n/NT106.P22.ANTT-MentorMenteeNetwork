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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                    return BadRequest("Email đã được sử dụng!");

                string avatarFilePath = null;

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Unauthorized("Email hoặc mật khẩu không đúng!");

            return Ok(new { Message = "Đăng nhập thành công!", UserId = user.Id });
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Server is running!");
        }
    }
}