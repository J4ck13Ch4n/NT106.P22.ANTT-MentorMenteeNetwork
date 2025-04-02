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
}