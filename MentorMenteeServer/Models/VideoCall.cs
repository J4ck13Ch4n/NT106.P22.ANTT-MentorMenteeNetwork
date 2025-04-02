using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VideoCall
{
    public int Id { get; set; }
    public Guid SessionId { get; set; } = Guid.NewGuid();

    public int CallerId { get; set; }
    public int ReceiverId { get; set; }

    [MaxLength(15)]
    public string CallStatus { get; set; } = "initiated"; // initiated, ongoing, completed, missed

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }

    [ForeignKey("CallerId")]
    public required User Caller { get; set; }

    [ForeignKey("ReceiverId")]
    public required User Receiver { get; set; }
}
