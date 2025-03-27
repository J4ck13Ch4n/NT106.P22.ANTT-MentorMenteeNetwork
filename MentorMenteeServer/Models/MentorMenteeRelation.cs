using System.ComponentModel.DataAnnotations.Schema;

namespace MentorMenteeServer.Data
{
    public class MentorMenteeRelation
    {
        public int MentorId { get; set; }
        [ForeignKey("MentorId")]
        public User Mentor { get; set; }

        public int MenteeId { get; set; }
        [ForeignKey("MenteeId")]
        public User Mentee { get; set; }
    }
}
