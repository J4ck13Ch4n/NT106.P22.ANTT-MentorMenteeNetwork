using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    public int Id { get; set; }

    public int ReviewerId { get; set; }
    public int ReviewedId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("ReviewerId")]
    public required User Reviewer { get; set; }

    [ForeignKey("ReviewedId")]
    public required User Reviewed { get; set; }
}
