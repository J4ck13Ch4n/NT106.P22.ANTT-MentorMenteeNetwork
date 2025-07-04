using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            lblUser.Text = user;
            lblContent.Text = content;
            PostId = postId;
            lblLikeCount.Text = $"{likeCount} lượt thích";
            btnLike.Visible = !likedByMe;
            btnUnlike.Visible = likedByMe;
            btnDelete.Visible = canDelete;
            if (!string.IsNullOrEmpty(avatar))
            {
                try
                {
                    picAvatar.LoadAsync(avatar);
                }
                catch { }
            }
        }

        public void SetComments(List<CommentControl> commentControls)
        {
            flowComments.Controls.Clear();
            foreach (var c in commentControls)
            {
                c.DeleteCommentClicked += (s, e) =>
                {
                    DeleteCommentClicked?.Invoke(this, c.CommentId);
                };
                flowComments.Controls.Add(c);
            }
        }

        private void btnLike_Click(object sender, EventArgs e) => LikeClicked?.Invoke(this, EventArgs.Empty);
        private void btnUnlike_Click(object sender, EventArgs e) => UnlikeClicked?.Invoke(this, EventArgs.Empty);
        private void btnDelete_Click(object sender, EventArgs e) => DeletePostClicked?.Invoke(this, EventArgs.Empty);
        private void btnAddComment_Click(object sender, EventArgs e)
        {
            AddCommentClicked?.Invoke(this, txtComment.Text);
            txtComment.Text = "";
        }
    }
}
