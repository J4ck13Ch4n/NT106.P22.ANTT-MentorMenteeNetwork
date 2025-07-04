using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace MentorMenteeUI
{
    public partial class CommentControl : UserControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CommentId { get; set; }
        public event EventHandler DeleteCommentClicked;

        public CommentControl(string user, string avatar, string content, int commentId, bool canDelete)
        {
            InitializeComponent();
            lblUser.Text = user;
            lblContent.Text = content;
            CommentId = commentId;
            btnDelete.Visible = canDelete;
            if (!string.IsNullOrEmpty(avatar))
            {
                try { picAvatar.LoadAsync(avatar); } catch { }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCommentClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}