namespace MentorMenteeServer.Data
{
    public class FriendRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderUsername { get; set; } 
        public string ReceiverUsername { get; set; } 
    }
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; } // Thêm trường Avatar (Base64)
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}