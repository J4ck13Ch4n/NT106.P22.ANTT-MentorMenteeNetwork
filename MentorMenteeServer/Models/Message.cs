using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorMenteeServer.Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Quan hệ với User (người gửi)
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        // Quan hệ với User (người nhận)
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }
}
