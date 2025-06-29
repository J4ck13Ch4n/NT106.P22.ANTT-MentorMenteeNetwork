using System.ComponentModel.DataAnnotations;

namespace MentorMenteeServer.Data
{
    public class FriendRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public required string SenderUsername { get; set; }
        public required string ReceiverUsername { get; set; }
    }
    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
        public required string Gender { get; set; }
        public required string Role { get; set; }
        public string? Avatar { get; set; } // Thêm trường Avatar (Base64)
    }

    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class UpdateUserDto
    {
        public int UserId { get; set; }
        [MaxLength(50)]
        public string? Username { get; set; }

        public string? AvatarPath { get; set; }
        public string? Gender { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(10)]
        public string? Role { get; set; }

        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
    }

    public class UserDto
    {
        public string? Username { get; set; }
        public string? Gender { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(10)]
        public string? Role { get; set; }
        public string? Bio { get; set; }
    }
}