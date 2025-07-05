using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace MentorMenteeUI
{
    public partial class PostControl : UserControl
    {
        private PostDto post;
        private int currentUserId;

        public event EventHandler OnLikeChanged;
        public event EventHandler OnCommentAdded;

        private string placeholder = "Nhập nội dung...";
        private Color placeholderColor = Color.Gray;
        private Color textColor = Color.Black;

        public PostControl(PostDto post, int currentUserId)
        {
            InitializeComponent();
            this.post = post;
            this.currentUserId = currentUserId;
            BindData();


            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            commentContent.ForeColor = placeholderColor;
            commentContent.Text = placeholder;

            commentContent.Enter += RemovePlaceholder;
            commentContent.Leave += SetPlaceholder;
        }

        private void BindData()
        {
            name.Text = post.User.Username;
            content.Text = post.Content;
            create_at.Text = post.CreatedAt.ToLocalTime().ToString("g");
            countLike.Text = $"{post.Likes.Count} lượt thích";
            role.Text = post.User.Role;

            string baseUrl = "https://localhost:5268/";
            string avatarPath = string.IsNullOrEmpty(post.User?.AvatarPath)
                ? "blankAvatar.png" 
                : post.User.AvatarPath.Replace("\\", "/");
            string imageUrl = baseUrl + avatarPath;
            avatar.ImageLocation = imageUrl;
            avatar_current.ImageLocation = imageUrl;

            picture.Visible = !string.IsNullOrEmpty(post.Image);
            string imagePath = string.IsNullOrEmpty(post.Image)
                ? "blankAvatar.png"
                : post.Image.Replace("\\", "/");
            string image = baseUrl + imagePath;
            picture.ImageLocation = image;

            video.Visible = !string.IsNullOrEmpty(post.Video);
            if (video.Visible)
                video.URL = baseUrl + post.Video.Replace("\\", "/");

            commentList.Controls.Clear();
            foreach (var cmt in post.Comments)
            {
                var cmtControl = new CommentControl(cmt, currentUserId);
                cmtControl.OnCommentDeleted += async (s, e) => await ReloadPost();
                commentList.Controls.Add(cmtControl);
            }

            // Đổi màu nút Like nếu đã thích
            btLike.Text = post.Likes.Any(l => l.UserId == currentUserId) ? "Đã thích" : "Thích";

            // Sự kiện
            btLike.Click += async (s, e) => await HandleLike();
            btComment.Click += async (s, e) => await HandleComment();

            btDeletePost.Visible = (post.UserId == currentUserId);
            btDeletePost.Click -= BtDeletePost_Click;
            btDeletePost.Click += BtDeletePost_Click;
        }

        private async Task ReloadPost()
        {
            OnCommentAdded?.Invoke(this, EventArgs.Empty);
        }

        private async void BtDeletePost_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa bài viết này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://localhost:5268/api/posts/{post.Id}";
                    var response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        OnLikeChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công");
                    }
                }
            }
        }

        private async Task HandleLike()
        {
            using (var client = new HttpClient())
            {
                var url = $"https://localhost:5268/api/posts/{post.Id}/like";
                if (post.Likes.Any(l => l.UserId == currentUserId))
                {
                    // Sử dụng HttpRequestMessage thay vì DeleteAsync
                    var request = new HttpRequestMessage(HttpMethod.Delete, url)
                    {
                        Content = new StringContent(currentUserId.ToString(), Encoding.UTF8, "application/json")
                    };
                    var response = await client.SendAsync(request);
                }
                else
                {
                    var content = new StringContent(currentUserId.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                }
            }
            OnLikeChanged?.Invoke(this, EventArgs.Empty);
        }

        private async Task HandleComment()
        {
            var text = commentContent.Text.Trim();
            using (var client = new HttpClient())
            {
                var url = $"https://localhost:5268/api/posts/{post.Id}/comments";
                var createComment = new { UserId = currentUserId, CommentText = text };
                var json = JsonConvert.SerializeObject(createComment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
            }
            commentContent.Text = "";
            OnCommentAdded?.Invoke(this, EventArgs.Empty);
        }

        //placeholder
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (commentContent.Text == placeholder && commentContent.ForeColor == placeholderColor)
            {
                commentContent.Text = "";
                commentContent.ForeColor = textColor;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(commentContent.Text))
            {
                commentContent.Text = placeholder;
                commentContent.ForeColor = placeholderColor;
            }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Văn bản gợi ý khi trống")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PlaceholderText
        {
            get => placeholder;
            set
            {
                placeholder = value;
                SetPlaceholder(null, null);
            }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Màu của placeholder")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color PlaceholderColor
        {
            get => placeholderColor;
            set
            {
                placeholderColor = value;
                SetPlaceholder(null, null);
            }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Màu chữ thực tế")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RealTextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                if (commentContent.ForeColor != placeholderColor)
                {
                    commentContent.ForeColor = textColor;
                }
            }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Nội dung văn bản thực tế")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TextValue
        {
            get
            {
                return (commentContent.Text == placeholder && commentContent.ForeColor == placeholderColor) ? "" : commentContent.Text;
            }
            set
            {
                commentContent.Text = value;
                commentContent.ForeColor = textColor;
            }
        }
    }
}
