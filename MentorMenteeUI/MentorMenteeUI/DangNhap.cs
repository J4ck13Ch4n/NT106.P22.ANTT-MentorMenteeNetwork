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
    public partial class DangNhap : Form
    {
        
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

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            TrangCaNhan trangCaNhan = new TrangCaNhan(this);
            trangCaNhan.Show();
            this.Hide();
        }
    }
}
