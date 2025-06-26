using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MentorMenteeUI.Services;

namespace MentorMenteeUI
{
    public partial class DangNhap : Form
    {
        [NonSerialized] // Thêm attribute này để tránh lỗi serialization
        public static string JwtToken = string.Empty; // Thêm thuộc tính tĩnh để lưu trữ JWT token

        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btDangNhap;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dk = new DangKy();
            dk.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DoiMatKhau dmk = new DoiMatKhau();
            dmk.ShowDialog();
        }

        private async void btDangNhap_Click(object sender, EventArgs e)
        {
            var authService = new AuthService("https://localhost:5268");
            var loginResult = await authService.LoginAsync(tbEmail.Text, tbMatKhau.Text);


            if (loginResult.Success && !string.IsNullOrEmpty(loginResult.UserId))
            {
                JwtToken = loginResult.Token; // Lưu token lấy từ loginResult
                MessageBox.Show("Đăng nhập thành công!");
                TrangCaNhan trangCaNhan = new TrangCaNhan(loginResult.UserId, this, loginResult.FullName, loginResult.Role, loginResult.Token); // Truyền userId và token
                trangCaNhan.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
            }
        }

        // In code-behind file (DangNhap.cs):
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 20;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel2.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel2.Width - radius, panel2.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel2.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            panel2.Region = new System.Drawing.Region(path);
        }
        // 
        private void btDangNhap_MouseEnter(object sender, EventArgs e)
        {
            btDangNhap.BackColor = System.Drawing.Color.FromArgb(0, 140, 200);
        }
        private void btDangNhap_MouseLeave(object sender, EventArgs e)
        {
            btDangNhap.BackColor = System.Drawing.Color.FromArgb(0, 116, 152);
        }
    }
}
