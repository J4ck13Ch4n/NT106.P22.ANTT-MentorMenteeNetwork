using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMenteeUI
{
    public partial class TrangCaNhan : Form
    {
        private readonly string userId, userName;
        private readonly Form loginForm;
        public TrangCaNhan(string userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;
            lbTen.Text = userName;
        }

        private void btTrangChu_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            TrangChuControl trangchu = new TrangChuControl();
            trangchu.Dock = DockStyle.Fill;
            pContent.Controls.Add(trangchu);
        }

        private void btNhanTin_Click(object sender, EventArgs e)
        {
            pContent.Controls.Clear();
            NhanTinControl nhantin = new NhanTinControl(this.userId, this.loginForm, this.userName);
            nhantin.Dock = DockStyle.Fill;
            pContent.Controls.Add(nhantin);
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
            TrangChuControl trangchu = new TrangChuControl();
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
    }
}
