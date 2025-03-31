using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMenteeUI
{
    public partial class TrangCaNhan : Form
    {
        private DangNhap dn;
        public TrangCaNhan(DangNhap dn)
        {
            InitializeComponent();
            this.dn = dn;
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
            NhanTinControl nhantin = new NhanTinControl();
            nhantin.Dock = DockStyle.Fill;
            pContent.Controls.Add(nhantin);
        }

        private void TrangCaNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void TrangCaNhan_Load(object sender, EventArgs e)
        {
            TrangChuControl trangchu = new TrangChuControl();
            trangchu.Dock = DockStyle.Fill;
            pContent.Controls.Add(trangchu);
        }

        private void btDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.dn.Show();
        }

        private void pContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
