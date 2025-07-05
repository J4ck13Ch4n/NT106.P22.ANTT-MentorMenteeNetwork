using System.ComponentModel;

namespace MentorMenteeUI
{
    public partial class PostControl : UserControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PostId { get; set; }
        public event EventHandler LikeClicked;
        public event EventHandler UnlikeClicked;
        public event EventHandler DeletePostClicked;
        public event EventHandler<string> AddCommentClicked;
        public event EventHandler<int> DeleteCommentClicked;

        public PostControl(string user, string avatar, string content, int postId, int likeCount, bool likedByMe, bool canDelete)
        {
            InitializeComponent();
        }

        
    }
}
