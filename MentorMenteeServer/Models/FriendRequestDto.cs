namespace MentorMenteeServer.Data
{
    public class FriendRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderUsername { get; set; }  // Thêm dòng này
        public string ReceiverUsername { get; set; } // Thêm dòng này
    }
}