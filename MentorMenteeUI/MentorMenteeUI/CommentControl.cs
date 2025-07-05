using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace MentorMenteeUI
{
    public partial class CommentControl : UserControl
    {
        private int currentUserId;
        private CommentDto cmt;
        public event EventHandler OnCommentDeleted;
        public CommentControl(CommentDto cmt, int currentUserId)
        {
            InitializeComponent();
            this.cmt = cmt;
            this.currentUserId = currentUserId;

            name.Text = cmt.User.Username;
            content.Text = cmt.CommentText;
            create_at.Text = cmt.CreatedAt.ToLocalTime().ToString("g");

            string baseUrl = "https://localhost:5268/";
            string avatarPath = string.IsNullOrEmpty(cmt.User?.AvatarPath)
                ? "blankAvatar.png"
                : cmt.User.AvatarPath.Replace("\\", "/");
            string imageUrl = baseUrl + avatarPath;
            avatar.ImageLocation = imageUrl;

            btDeleteComment.Visible = (cmt.UserId == currentUserId);
            btDeleteComment.Click -= BtDeleteComment_Click;
            btDeleteComment.Click += BtDeleteComment_Click;
        }
        private async void BtDeleteComment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa bình luận này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://localhost:5268/api/comments/{cmt.Id}";
                    var response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        OnCommentDeleted?.Invoke(this, EventArgs.Empty); // Để PostControl reload lại list comment
                    }
                    else
                    {
                        var errorMsg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Xóa không thành công: " + errorMsg);
                    }
                }
            }
        }
    }
}