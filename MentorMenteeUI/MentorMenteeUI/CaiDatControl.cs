using Microsoft.VisualBasic.ApplicationServices;
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
        private readonly string apiBaseUrl = "https://localhost:5268/api";
        private HttpClient httpClient;

        public CaiDatControl()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void CaiDatControl_Load(object sender, EventArgs e)
        {
            await LoadCurrentUser();
        }

        private async Task LoadCurrentUser()
        {
            try
            {
                var userId = DangNhap.UserId;
                var response = await httpClient.GetAsync($"{apiBaseUrl}/user/me?userId={userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
                UserId = int.Parse(DangNhap.UserId),
                Username = Username.Text,
                Email = Email.Text,
                Role = Role.SelectedItem?.ToString(),
                Gender = Gender.SelectedItem?.ToString(),
                Bio = Bio.Text
            };

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{apiBaseUrl}/user/update", content);

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
    }


}