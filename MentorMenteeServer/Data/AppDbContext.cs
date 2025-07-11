﻿using Microsoft.EntityFrameworkCore;

namespace MentorMenteeServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VideoCall> VideoCalls { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupChatMember> GroupChatMembers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }

        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quan hệ giữa User và Relationship (kiểu mới)
            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Friend)
                .WithMany()
                .HasForeignKey(r => r.FriendId)
                .OnDelete(DeleteBehavior.Restrict);


            // Quan hệ Review (không thể tự đánh giá chính mình)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewed)
                .WithMany()
                .HasForeignKey(r => r.ReviewedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("chk_self_review", "[ReviewerId] <> [ReviewedId]"));


            // Quan hệ Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            // Quan hệ GroupChat và GroupChatMember
            modelBuilder.Entity<GroupChatMember>()
                .HasIndex(g => new { g.GroupId, g.UserId }).IsUnique();

            // Quan hệ User và Post
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ Post và Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ Post và Like
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VideoCall>()
                .HasOne(v => v.Caller)
                .WithMany()
                .HasForeignKey(v => v.CallerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VideoCall>()
                .HasOne(v => v.Receiver)
                .WithMany()
                .HasForeignKey(v => v.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupChatMember>()
                .HasOne(gcm => gcm.User)
                .WithMany()
                .HasForeignKey(gcm => gcm.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Hoặc DeleteBehavior.SetNull

            // Cấu hình khóa ngoại cho MenteeId
            modelBuilder.Entity<Goal>()
                .HasOne<User>() // Quan hệ với User (không cần thuộc tính điều hướng)
                .WithMany()     // Một User có thể có nhiều Goal
                .HasForeignKey(g => g.MenteeId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa lan (tùy chọn)

            // Cấu hình khóa ngoại cho MentorId
            modelBuilder.Entity<Goal>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.MentorId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa lan (tùy chọn)

        }
    }
}
