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

            // Xác định có media không
            bool hasImage = !string.IsNullOrEmpty(post.Image);
            bool hasVideo = !string.IsNullOrEmpty(post.Video);
            bool hasMedia = hasImage || hasVideo;

            // Giao diện cho bài có media
            if (hasMedia)
            {
                // Hiển thị media như bình thường
                picture.Visible = hasImage;
                if (hasImage)
                {
                    string imagePath = post.Image.Replace("\\", "/");
                    picture.ImageLocation = baseUrl + imagePath;
                }
                video.Visible = hasVideo;
                if (hasVideo)
                {
                    video.URL = baseUrl + post.Video.Replace("\\", "/");
                }

                // Đặt content ở vị trí cũ
                content.ForeColor = Color.Gray;
                content.Location = new Point(58, 65);
                content.Multiline = true;
                content.Name = "content";
                content.ReadOnly = true;
                content.Size = new Size(792, 53);
                content.TabIndex = 2;

                // Đảm bảo các control media hiện đúng
                picture.BringToFront();
                video.BringToFront();
            }
            // Giao diện cho bài chỉ có content
            else
            {
                // Ẩn media
                picture.Visible = false;
                video.Visible = false;

                // Đặt content ra giữa, to hơn, nổi bật hơn
                content.Location = new Point(50,65); // Đặt lại vị trí theo ý bạn
                content.Size = new Size(800, 100);    // Tăng chiều cao/ngang nếu muốn
                content.Font = new Font("Segoe UI", 13, FontStyle.Bold);

                //localtion list comment
                commentList.AutoScroll = true;
                commentList.BackColor = SystemColors.ButtonHighlight;
                commentList.FlowDirection = FlowDirection.TopDown;
                commentList.Location = new Point(51, 200);
                commentList.Name = "commentList";
                commentList.Size = new Size(798, 240);
                commentList.TabIndex = 6;
                commentList.WrapContents = false;

                //localtion avt
                avatar_current.Location = new Point(170, 448);
                avatar_current.Name = "avatar_current";
                avatar_current.Size = new Size(35, 33);
                avatar_current.SizeMode = PictureBoxSizeMode.StretchImage;
                avatar_current.TabIndex = 1;
                avatar_current.TabStop = false;

                //localtion comment
                commentContent.ForeColor = Color.Gray;
                commentContent.Location = new Point(220, 448);
                commentContent.Multiline = true;
                commentContent.Name = "commentContent";
                commentContent.Size = new Size(545, 53);
                commentContent.TabIndex = 9;

                cmtLabel.Location = new Point(55, 180);

                // Có thể đổi màu nền hoặc border cho nổi bật
                content.BackColor = Color.FromArgb(245, 250, 255);
                content.ForeColor = Color.DimGray;
            }

            // Hiển thị danh sách comment
            commentList.Controls.Clear();
            foreach (var cmt in post.Comments)
            {
                var cmtControl = new CommentControl(cmt, currentUserId);
                cmtControl.OnCommentDeleted += async (s, e) => await ReloadPost();
                commentList.Controls.Add(cmtControl);
            }

            // Like/Comment
            btLike.Text = post.Likes.Any(l => l.UserId == currentUserId) ? "Đã thích" : "Like";
            btLike.Click -= asyncBtLikeClick;
            btLike.Click += asyncBtLikeClick;
            btComment.Click -= asyncBtCommentClick;
            btComment.Click += asyncBtCommentClick;
            btDeletePost.Visible = (post.UserId == currentUserId);
            btDeletePost.Click -= BtDeletePost_Click;
            btDeletePost.Click += BtDeletePost_Click;
        }

        // Để tránh gán nhiều lần event handler, bạn nên khai báo delegate
        private async void asyncBtLikeClick(object sender, EventArgs e) => await HandleLike();
        private async void asyncBtCommentClick(object sender, EventArgs e) => await HandleComment();

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
                        if (this.Parent is FlowLayoutPanel flp)
                        {
                            flp.Controls.Remove(this);
                            this.Dispose();
                        }
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
                int likeCount = post.Likes.Count;
                bool daThich = post.Likes.Any(l => l.UserId == currentUserId);

                if (daThich)
                {
                    // Unlike
                    var request = new HttpRequestMessage(HttpMethod.Delete, url)
                    {
                        Content = new StringContent(currentUserId.ToString(), Encoding.UTF8, "application/json")
                    };
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        // Xóa khỏi danh sách like local
                        post.Likes.RemoveAll(l => l.UserId == currentUserId);
                        btLike.Text = "Like";
                        likeCount--;
                    }
                }
                else
                {
                    // Like
                    var content = new StringContent(currentUserId.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        post.Likes.Add(new LikeDto { UserId = currentUserId }); // Cập nhật local
                        btLike.Text = "Đã thích";
                        likeCount++;
                    }
                }
                countLike.Text = $"{likeCount} lượt thích";
            }
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
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var commentDto = JsonConvert.DeserializeObject<CommentDto>(responseJson);
                    var cmtControl = new CommentControl(commentDto, currentUserId);
                    commentList.Controls.Add(cmtControl);
                    commentContent.Text = "";
                }
                else
                {
                    MessageBox.Show("Bình luận thất bại!");
                }
            }
            commentContent.Text = "";
            //OnCommentAdded?.Invoke(this, EventArgs.Empty);
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
