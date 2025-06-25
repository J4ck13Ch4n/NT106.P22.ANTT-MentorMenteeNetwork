using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMenteeUI
{
    public partial class CaiDatControl : UserControl
    {
        // Đổi URL cho phù hợp với backend của bạn
        private readonly string apiBaseUrl = "https://your-backend-url/api";
        private HttpClient httpClient;

        private readonly string _jwtToken;
        public CaiDatControl(string jwtToken)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            _jwtToken = jwtToken;
        }

        private async void CaiDatControl_Load(object sender, EventArgs e)
        {
            await LoadCurrentUser();
        }

        private async Task LoadCurrentUser()
        {
            try
            {
                string jwtToken = GetJwtToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var response = await httpClient.GetAsync($"{apiBaseUrl}/users/me");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Đổ dữ liệu lên form
                    Username.Text = user.Username;
                    Email.Text = user.Email;
                    Role.SelectedItem = user.Role;
                    Gender.SelectedItem = user.Gender;
                    Bio.Text = user.Bio;
                }
                else
                {
                    MessageBox.Show("Không lấy được thông tin người dùng.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void btLuu_Click(object sender, EventArgs e)
        {
            var dto = new UpdateUserDto
            {
                Username = Username.Text,
                Email = Email.Text,
                Role = Role.SelectedItem?.ToString(),
                Gender = Gender.SelectedItem?.ToString(),
                Bio = Bio.Text
            };

            try
            {
                string jwtToken = GetJwtToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{apiBaseUrl}/users/me", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Lỗi cập nhật: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm lấy token JWT, bạn cần sửa lại cho phù hợp cách app bạn lưu token
        private string GetJwtToken()
        {
            return !string.IsNullOrEmpty(_jwtToken) ? _jwtToken : DangNhap.JwtToken;
        }
    }

    // Model phù hợp với backend
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
}