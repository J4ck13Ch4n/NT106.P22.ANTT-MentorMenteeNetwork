namespace MentorMenteeServer.Data
{
    public class FriendRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderUsername { get; set; }  // Th�m d�ng n�y
        public string ReceiverUsername { get; set; } // Th�m d�ng n�y
    }
}