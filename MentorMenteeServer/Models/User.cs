using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public required string Username { get; set; }

    public bool IsOnline { get; set; } = false;

    public string? AvatarPath { get; set; }
    public string? Gender { get; set; }

    [Required, MaxLength(100)]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    [MaxLength(10)]
    public string Role { get; set; } = "mentee";

    public bool TwoFactorEnabled { get; set; } = false;
    public bool EmailVerified { get; set; } = false;

    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Xóa các property MentorRelationships, MenteeRelationships vì đã không còn dùng Mentor/Mentee

    public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    // Mã hóa end-to-end (RSA keys)
    [Column(TypeName = "TEXT")]
    public string? PublicKey { get; set; }

    [Column(TypeName = "TEXT")]
    public string? PrivateKeyEncrypted { get; set; }
}
