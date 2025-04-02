using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public int? GroupId { get; set; }

    public string? MessageText { get; set; }
    public string? FileAttachment { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("SenderId")]
    public required User Sender { get; set; }

    [ForeignKey("ReceiverId")]
    public User? Receiver { get; set; }

    [ForeignKey("GroupId")]
    public GroupChat? GroupChat { get; set; }
}
