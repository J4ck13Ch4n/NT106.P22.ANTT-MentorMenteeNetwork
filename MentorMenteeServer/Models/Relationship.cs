using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Relationship
{
    public int Id { get; set; }
    public int MentorId { get; set; }
    public int MenteeId { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("MentorId")]
    public required User Mentor { get; set; }

    [ForeignKey("MenteeId")]
    public required User Mentee { get; set; }
}
