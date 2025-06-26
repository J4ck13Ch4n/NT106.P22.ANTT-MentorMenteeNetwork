using Microsoft.AspNetCore.Mvc;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            public string Role { get; set; }
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

            user.IsOnline = true;
            await _context.SaveChangesAsync();

            // Tạo JWT token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role ?? "mentee"),
            };
            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "super_secret_key_123!super_secret_key_123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"] ?? "MentorMenteeServer",
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return Ok(new
            {
                Success = true,
                UserId = user.Id.ToString(),
                FullName = user.Username,
                Role = user.Role,
                Token = tokenString
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