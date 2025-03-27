using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorMenteeServer.Data
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }

    public enum FriendshipStatus
    {
        Pending,  // Đang chờ xác nhận
        Accepted, // Đã kết bạn
        Rejected  // Bị từ chối
    }
}
