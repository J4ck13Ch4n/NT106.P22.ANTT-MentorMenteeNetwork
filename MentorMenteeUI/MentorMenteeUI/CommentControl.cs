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
        }

    }
}