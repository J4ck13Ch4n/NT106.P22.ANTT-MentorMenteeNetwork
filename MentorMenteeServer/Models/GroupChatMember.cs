using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GroupChatMember
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }

    [MaxLength(10)]
    public string Status { get; set; } = "joined"; // joined, left, removed

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("GroupId")]
    public required GroupChat GroupChat { get; set; }

    [ForeignKey("UserId")]
    public required User User { get; set; }
}
