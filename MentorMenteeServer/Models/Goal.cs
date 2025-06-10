using System.ComponentModel.DataAnnotations.Schema;

public class Goal
{
    public int Id { get; set; } // STT
    public string Title { get; set; } // Mục tiêu
    public string Description { get; set; } // Mô tả
    public DateTime Deadline { get; set; } // Thời gian
    public string Status { get; set; } // Tình trạng (Chưa làm, Đang làm, Hoàn thành, etc.)
    public string Feedback { get; set; } // Đánh giá
    public int MenteeId { get; set; }
    public int MentorId { get; set; }
}