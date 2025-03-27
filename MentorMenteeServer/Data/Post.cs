using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorMenteeServer.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Quan hệ với User (người đăng bài)
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Danh sách bình luận
        public ICollection<Comment> Comments { get; set; }
    }
}
