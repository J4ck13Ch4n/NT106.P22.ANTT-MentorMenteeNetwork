using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorMenteeServer.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Quan hệ với User (người bình luận)
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Quan hệ với Post (bình luận thuộc bài viết nào)
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
