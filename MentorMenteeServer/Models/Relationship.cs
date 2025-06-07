using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Relationship
{
    public int Id { get; set; }
    public int UserId { get; set; } // Người gửi lời mời
    public int FriendId { get; set; } // Người nhận lời mời

    [MaxLength(20)]
    public string Status { get; set; } = "pending"; // pending, accepted, rejected

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public required User User { get; set; }

    [ForeignKey("FriendId")]
    public required User Friend { get; set; }
}
