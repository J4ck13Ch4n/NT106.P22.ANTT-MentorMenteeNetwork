using Microsoft.EntityFrameworkCore;

namespace MentorMenteeServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MentorMenteeRelation> MentorMenteeRelations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập quan hệ giữa User và Message (1 user có thể gửi nhiều tin nhắn)
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

            // Thiết lập quan hệ Mentor - Mentee (nhiều mentor - nhiều mentee)
            modelBuilder.Entity<MentorMenteeRelation>()
                .HasKey(r => new { r.MentorId, r.MenteeId });

            modelBuilder.Entity<MentorMenteeRelation>()
                .HasOne(r => r.Mentor)
                .WithMany(u => u.Mentees)
                .HasForeignKey(r => r.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MentorMenteeRelation>()
                .HasOne(r => r.Mentee)
                .WithMany(u => u.Mentors)
                .HasForeignKey(r => r.MenteeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Sender)
                .WithMany()
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Receiver)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);


        }
    }
}
