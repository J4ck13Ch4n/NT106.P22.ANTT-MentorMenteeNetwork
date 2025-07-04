using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMenteeUI
{
    public partial class TrangChuControl: UserControl
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiBase = "https://localhost:5268/api";
        private readonly int currentUserId = 1; // lấy từ đăng nhập thực tế
        public TrangChuControl()
        {
            InitializeComponent();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        private async void TrangChuControl_Load(object sender, EventArgs e)
        {
            await LoadPosts();
        }

        private async Task LoadPosts()
        {
            flowPosts.Controls.Clear();
            var posts = await httpClient.GetFromJsonAsync<List<PostDto>>($"{apiBase}/posts");
            foreach (var post in posts)
            {
                var postControl = CreatePostControl(post);
                flowPosts.Controls.Add(postControl);
            }
        }

        private PostControl CreatePostControl(PostDto post)
        {
            bool likedByMe = post.Likes.Exists(l => l.UserId == currentUserId);
            var postCtrl = new PostControl(
                post.User.Username,
                post.User.AvatarPath,
                post.Content,
                post.Id,
                post.Likes.Count,
                likedByMe,
                post.UserId == currentUserId // chỉ chủ bài được xóa
            );

            var commentControls = new List<CommentControl>();
            foreach (var comment in post.Comments)
            {
                var c = new CommentControl(
                    comment.User.Username,
                    comment.User.AvatarPath,
                    comment.CommentText,
                    comment.Id,
                    comment.UserId == currentUserId
                );
                c.DeleteCommentClicked += async (s, e) =>
                {
                    await httpClient.DeleteAsync($"{apiBase}/comments/{comment.Id}");
                    await LoadPosts();
                };
                commentControls.Add(c);
            }
            postCtrl.SetComments(commentControls);

            postCtrl.LikeClicked += async (s, e) =>
            {
                var body = new StringContent(currentUserId.ToString(), System.Text.Encoding.UTF8, "application/json");
                await httpClient.PostAsync($"{apiBase}/posts/{post.Id}/like", body);
                await LoadPosts();
            };
            postCtrl.UnlikeClicked += async (s, e) =>
            {
                var body = new StringContent(currentUserId.ToString(), System.Text.Encoding.UTF8, "application/json");
                await httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{apiBase}/posts/{post.Id}/like"),
                    Content = body
                });
                await LoadPosts();
            };
            postCtrl.DeletePostClicked += async (s, e) =>
            {
                await httpClient.DeleteAsync($"{apiBase}/posts/{post.Id}");
                await LoadPosts();
            };
            postCtrl.AddCommentClicked += async (s, commentText) =>
            {
                if (!string.IsNullOrWhiteSpace(commentText))
                {
                    var body = new
                    {
                        userId = currentUserId,
                        commentText = commentText
                    };
                    await httpClient.PostAsJsonAsync($"{apiBase}/posts/{post.Id}/comments", body);
                    await LoadPosts();
                }
            };

            return postCtrl;
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPostContent.Text))
            {
                var body = new CreatePostDto
                {
                    UserId = currentUserId,
                    Content = txtPostContent.Text,
                    Image = string.IsNullOrWhiteSpace(txtImagePath.Text) ? null : txtImagePath.Text,
                    Video = string.IsNullOrWhiteSpace(txtVideoPath.Text) ? null : txtVideoPath.Text,
                    Visibility = "public"
                };
                await httpClient.PostAsJsonAsync($"{apiBase}/posts", body);
                txtPostContent.Text = "";
                txtImagePath.Text = "";
                txtImagePath.Visible = false;
                txtVideoPath.Text = "";
                txtVideoPath.Visible = false;
                await LoadPosts();
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    txtImagePath.Visible = true;
                }
            }
        }

        private void btnChooseVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn video";
                ofd.Filter = "Video Files|*.mp4;*.avi;*.mov;*.wmv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtVideoPath.Text = ofd.FileName;
                    txtVideoPath.Visible = true;
                }
            }
        }

    }


    // DTOs dùng cho client, map y chang backend
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
        public List<LikeDto> Likes { get; set; } = new();
    }
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }
    public class LikeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }
    public class UserSimpleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? AvatarPath { get; set; }
    }
    public class CreatePostDto
    {
        public int UserId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string Visibility { get; set; } = "public";
    }
}
