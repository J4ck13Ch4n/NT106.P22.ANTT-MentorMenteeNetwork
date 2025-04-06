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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private string avatarPath;
        private void btChonAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn ảnh đại diện";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarPath = openFileDialog.FileName; // Lưu đường dẫn ảnh
                    circlePictureBox1.Image = Image.FromFile(avatarPath); // Hiển thị ảnh trong PictureBox
                }
            }
        }
        private async void btDangKy_Click(object sender, EventArgs e)
        {
            var authService = new AuthService("https://localhost:5268"); // Địa chỉ server

            // Lấy giá trị từ các trường trong form
            string email = tbEmail.Text;
            string password = tbMatKhau.Text;
            string username = tbHoTen.Text;
            string gender = cbGioiTinh.SelectedItem?.ToString();
            string role = cbVaiTro.SelectedItem?.ToString();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            // Ghi log dữ liệu gửi đi
            Console.WriteLine($"Payload gửi đến server: Email={email}, Password={password}, Username={username}, Gender={gender}, Role={role}");

            try
            {
                var response = await authService.RegisterAsync(email, password, username, gender, role, avatarPath);

                if (response.IsSuccess)
                {
                    MessageBox.Show("Đăng ký thành công!");
                }
                else
                {
                    MessageBox.Show($"Đăng ký thất bại! Lỗi từ server: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đăng ký thất bại! Lỗi: {ex.Message}");
            }
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btDangKy;
        }
    }


}
