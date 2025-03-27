using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentorMenteeServer.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Quan hệ với Message
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }

        // Quan hệ Mentor - Mentee
        public ICollection<MentorMenteeRelation> Mentors { get; set; }
        public ICollection<MentorMenteeRelation> Mentees { get; set; }
    }
}
