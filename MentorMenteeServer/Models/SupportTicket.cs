using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class SupportTicket
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [Required, MaxLength(255)]
    public string? Subject { get; set; }

    [Required]
    public string? Message { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "open"; // open, in_progress, resolved, closed

    public int? HandledBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public required User User { get; set; }

    [ForeignKey("HandledBy")]
    public User? Handler { get; set; }
}
