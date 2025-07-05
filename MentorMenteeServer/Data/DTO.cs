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
        public string? AvatarPath { get; set; }
    }

    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
        public List<LikeDto> Likes { get; set; } = new();
    }

    public class CreatePostFormDto
    {
        public int UserId { get; set; }
        public string? Content { get; set; }
        public string Visibility { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? Video { get; set; }
    }

    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }

    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public string? CommentText { get; set; }
    }

    public class LikeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }

    public class UserSimpleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? AvatarPath { get; set; }
        public string Role { get; set; }
    }
}