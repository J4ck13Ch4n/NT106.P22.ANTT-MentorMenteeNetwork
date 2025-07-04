using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string? Content { get; set; }
    public string? Image { get; set; }
    public string? Video { get; set; }

    [MaxLength(10)]
    public string Visibility { get; set; } = "public"; // public, private, friends

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public User User { get; set; }

    // Thêm danh sách comment
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    // Thêm danh sách like
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}
