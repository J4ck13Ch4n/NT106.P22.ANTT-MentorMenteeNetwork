
using System.ComponentModel;
using System.Text.Json;

namespace MentorMenteeUI
{
    public partial class TrangChuControl : UserControl
    {
        private readonly string apiBaseUrl = "https://localhost:5268/";
        private HttpClient httpClient;

        private string placeholder = "Bạn đang nghĩ gì?";
        private Color placeholderColor = Color.Gray;
        private Color textColor = Color.Black;
        public TrangChuControl(int currentUserId)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            this.currentUserId = currentUserId;
            LoadPosts();
            LoadAvt();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            content.ForeColor = placeholderColor;
            content.Text = placeholder;

            content.Enter += RemovePlaceholder;
            content.Leave += SetPlaceholder;
        }

        private int currentUserId;
        private string imageFilePath;
        private string videoFilePath;

        public async Task LoadAvt()
        {
            var res = await httpClient.GetAsync($"{apiBaseUrl}api/user/me?userId={currentUserId}");

            res.EnsureSuccessStatusCode(); // báo lỗi nếu gọi API thất bại

            var json = await res.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<UserDTO>(json);

            string avatarPath = string.IsNullOrEmpty(user.AvatarPath)
                ? "blankAvatar.png"
                : user.AvatarPath.Replace("\\", "/");

            string imageUrl = apiBaseUrl + avatarPath;

            avatar.ImageLocation = imageUrl;
        }


        public async Task LoadPosts()
        {
            try
            {
                postList.Controls.Clear();
                var posts = await GetPostsAsync();
                foreach (var post in posts)
                {
                    var postControl = new PostControl(post, currentUserId);
                    postControl.OnLikeChanged += async (s, e) => await LoadPosts();
                    postControl.OnCommentAdded += async (s, e) => await LoadPosts();
                    postList.Controls.Add(postControl);
                }
                postList.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gọi API: {ex.Message}");
            }
        }

        public async Task<List<PostDto>> GetPostsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5268/");
                var response = await client.GetAsync("api/posts");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PostDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private async void btPost_Click(object sender, EventArgs e)
        {
            // Lấy nội dung thật thay vì Text; TextValue đã lọc placeholder
            var text = this.TextValue.Trim();          // <-- dùng thuộc tính bạn đã viết
            bool hasText = !string.IsNullOrEmpty(text);
            bool hasMedia = imageFilePath != null || videoFilePath != null;

            // 1. Không có gì để đăng
            if (!hasText && !hasMedia)
            {
                MessageBox.Show("Bạn cần nhập nội dung hoặc chọn ảnh/video trước khi đăng!");
                return;
            }

            using var client = new HttpClient();
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(currentUserId.ToString()), "UserId");

            // 2. Gửi Content khi có chữ, hoặc chuỗi rỗng nếu back‑end bắt buộc
            form.Add(new StringContent(hasText ? text : string.Empty), "Content");

            form.Add(new StringContent("Public"), "Visibility");

            // 3. Đính kèm media nếu có
            if (imageFilePath != null)
                form.Add(new StreamContent(File.OpenRead(imageFilePath)), "image",
                         Path.GetFileName(imageFilePath));

            if (videoFilePath != null)
                form.Add(new StreamContent(File.OpenRead(videoFilePath)), "video",
                         Path.GetFileName(videoFilePath));

            // 4. Gọi API
            var response = await client.PostAsync("https://localhost:5268/api/posts", form);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi: {response.StatusCode}\nChi tiết: {error}");
                return;
            }

            // 5. Reset giao diện
            content.Text = "";               // xoá nội dung
            SetPlaceholder(null, null);      // khôi phục placeholder
            previewPic.Image = null;
            axWindowsMediaPlayer1.URL = null;
            imageFilePath = videoFilePath = null;

            await LoadPosts();
        }


        private void ClearAttachmentPreview()
        {
            // xóa preview ảnh
            previewPic.Image = null;
            previewPic.Visible = false;

            // dừng và ẩn video
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.URL = string.Empty;   // hoặc null
            axWindowsMediaPlayer1.Visible = false;

            // reset đường dẫn tạm
            imageFilePath = null;
            videoFilePath = null;
        }


        private void btFile_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Ảnh hoặc video|*.jpg;*.jpeg;*.png;*.mp4";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var ext = Path.GetExtension(ofd.FileName).ToLower();
                if (ext == ".mp4")
                {
                    videoFilePath = ofd.FileName;
                    imageFilePath = null;
                    previewPic.Visible = false;
                    axWindowsMediaPlayer1.Visible = true;
                    axWindowsMediaPlayer1.URL = videoFilePath;
                }
                else
                {
                    imageFilePath = ofd.FileName;
                    videoFilePath = null;
                    previewPic.Visible = true;
                    axWindowsMediaPlayer1.Visible = false;
                    previewPic.ImageLocation = imageFilePath;
                }
            }
        }


        //placeholder
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (content.Text == placeholder && content.ForeColor == placeholderColor)
            {
                content.Text = "";
                content.ForeColor = textColor;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(content.Text))
            {
                content.Text = placeholder;
                content.ForeColor = placeholderColor;
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
                if (content.ForeColor != placeholderColor)
                {
                    content.ForeColor = textColor;
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
                return (content.Text == placeholder && content.ForeColor == placeholderColor) ? "" : content.Text;
            }
            set
            {
                content.Text = value;
                content.ForeColor = textColor;
            }
        }

    }
}
