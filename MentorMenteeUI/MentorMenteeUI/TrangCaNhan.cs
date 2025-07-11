using System.Net.Http.Json;
using System.Text.Json;

namespace MentorMenteeUI
{
    public partial class TrangCaNhan : Form
    {
        private readonly string userId, userName, role, jwtToken;
        private readonly Form loginForm;
        // Thêm biến lưu control
        private NhanTinControl nhanTinControl;
        private KetBanControl ketBanControl;

        private readonly string apiBaseUrl = "https://localhost:5268/";
        private HttpClient httpClient;
        public TrangCaNhan(string userId, Form loginForm, string userName, string role, string jwtToken)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;
            lbTen.Text = userName;
            this.role = role;
            this.jwtToken = jwtToken;
            // Khởi tạo control một lần
            nhanTinControl = new NhanTinControl(int.Parse(this.userId), this.loginForm, this.userName);
            ketBanControl = new KetBanControl(int.Parse(this.userId));
            ketBanControl.FriendListChanged += (s, e) => nhanTinControl.Invoke(new Action(async () => await nhanTinControl.RefreshConversationListAsync()));
            LoadAvt();
        }

        public async Task LoadAvt()
        {
            var res = await httpClient.GetAsync($"{apiBaseUrl}api/user/me?userId={userId}");

            res.EnsureSuccessStatusCode(); // báo lỗi nếu gọi API thất bại

            var json = await res.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<UserDTO>(json);

            string avatarPath = string.IsNullOrEmpty(user.AvatarPath)
                ? "blankAvatar.png"
                : user.AvatarPath.Replace("\\", "/");

            string imageUrl = apiBaseUrl + avatarPath;

            cpbAvatar.ImageLocation = imageUrl;
        }

        private void btTrangChu_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            TrangChuControl trangchu = new TrangChuControl(int.Parse(userId));
            trangchu.Dock = DockStyle.Fill;
            pContent.Controls.Add(trangchu);
        }

        private void btNhanTin_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            nhanTinControl.Dock = DockStyle.Fill;
            pContent.Controls.Add(nhanTinControl);
            _ = nhanTinControl.RefreshConversationListAsync();
        }

        private async void TrangCaNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:5268/api/logout", userId);
            }
            loginForm.Show();
        }

        private void TrangCaNhan_Load(object sender, EventArgs e)
        {
            TrangChuControl trangchu = new TrangChuControl(int.Parse(userId)); 
            trangchu.Dock = DockStyle.Fill;
            pContent.Controls.Add(trangchu);
        }

        private async void btDangXuat_Click(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:5268/api/logout", userId);
            }

            loginForm.Show();
            this.Close();
        }

        private void pContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btCaiDat_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            CaiDatControl caiDat = new CaiDatControl();
            caiDat.Dock = DockStyle.Fill;
            pContent.Controls.Add(caiDat);
        }

        private void lbTen_Click(object sender, EventArgs e)
        {

        }

        private void btGoal_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            if (role == "Mentor")
            {
                MucTieuMentorControl mentorGoalControl = new MucTieuMentorControl(int.Parse(userId));
                mentorGoalControl.Dock = DockStyle.Fill;
                pContent.Controls.Add(mentorGoalControl);
            }
            else if (role == "Mentee")
            {
                MucTieuMenteeControl menteeGoalControl = new MucTieuMenteeControl(int.Parse(userId));
                menteeGoalControl.Dock = DockStyle.Fill;
                pContent.Controls.Add(menteeGoalControl);
            }
        }

        private void btKetBan_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            ketBanControl.Dock = DockStyle.Fill;
            pContent.Controls.Add(ketBanControl);
        }
    }
}
