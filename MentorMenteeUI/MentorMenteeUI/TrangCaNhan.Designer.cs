namespace MentorMenteeUI
{
    partial class TrangCaNhan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            cpbAvatar = new CirclePictureBox();
            lbTen = new Label();
            btDangXuat = new Button();
            btTrangChu = new Button();
            btNhanTin = new Button();
            btCaiDat = new Button();
            pContent = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cpbAvatar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 152, 168);
            panel1.Controls.Add(cpbAvatar);
            panel1.Controls.Add(lbTen);
            panel1.Controls.Add(btDangXuat);
            panel1.Controls.Add(btTrangChu);
            panel1.Controls.Add(btNhanTin);
            panel1.Controls.Add(btCaiDat);
            panel1.Location = new Point(1, 3);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(184, 917);
            panel1.TabIndex = 0;
            // 
            // cpbAvatar
            // 
            cpbAvatar.Image = Properties.Resources.blankAvatar;
            cpbAvatar.Location = new Point(29, 10);
            cpbAvatar.Margin = new Padding(3, 4, 3, 4);
            cpbAvatar.Name = "cpbAvatar";
            cpbAvatar.Size = new Size(125, 111);
            cpbAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            cpbAvatar.TabIndex = 3;
            cpbAvatar.TabStop = false;
            // 
            // lbTen
            // 
            lbTen.AutoSize = true;
            lbTen.Font = new Font("Sans Serif Collection", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTen.ForeColor = Color.White;
            lbTen.Location = new Point(29, 125);
            lbTen.MaximumSize = new Size(125, 91);
            lbTen.Name = "lbTen";
            lbTen.Size = new Size(125, 91);
            lbTen.TabIndex = 2;
            lbTen.Text = "HoTen";
            // 
            // btDangXuat
            // 
            btDangXuat.BackColor = Color.FromArgb(0, 152, 168);
            btDangXuat.FlatAppearance.BorderSize = 0;
            btDangXuat.FlatStyle = FlatStyle.Flat;
            btDangXuat.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btDangXuat.ForeColor = Color.White;
            btDangXuat.Image = Properties.Resources.icons8_log_out_50;
            btDangXuat.ImageAlign = ContentAlignment.TopCenter;
            btDangXuat.Location = new Point(0, 647);
            btDangXuat.Margin = new Padding(3, 4, 3, 4);
            btDangXuat.Name = "btDangXuat";
            btDangXuat.Size = new Size(181, 132);
            btDangXuat.TabIndex = 0;
            btDangXuat.Text = "Đăng Xuất";
            btDangXuat.TextAlign = ContentAlignment.BottomCenter;
            btDangXuat.UseVisualStyleBackColor = false;
            btDangXuat.Click += btDangXuat_Click;
            // 
            // btTrangChu
            // 
            btTrangChu.BackColor = Color.FromArgb(0, 152, 168);
            btTrangChu.FlatAppearance.BorderSize = 0;
            btTrangChu.FlatStyle = FlatStyle.Flat;
            btTrangChu.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btTrangChu.ForeColor = Color.White;
            btTrangChu.Image = Properties.Resources.icons8_home_50;
            btTrangChu.ImageAlign = ContentAlignment.TopCenter;
            btTrangChu.Location = new Point(3, 233);
            btTrangChu.Margin = new Padding(3, 4, 3, 4);
            btTrangChu.Name = "btTrangChu";
            btTrangChu.Size = new Size(178, 129);
            btTrangChu.TabIndex = 0;
            btTrangChu.Text = "Trang Chủ";
            btTrangChu.TextAlign = ContentAlignment.BottomCenter;
            btTrangChu.UseVisualStyleBackColor = false;
            btTrangChu.Click += btTrangChu_Click;
            // 
            // btNhanTin
            // 
            btNhanTin.BackColor = Color.FromArgb(0, 152, 168);
            btNhanTin.FlatAppearance.BorderSize = 0;
            btNhanTin.FlatStyle = FlatStyle.Flat;
            btNhanTin.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btNhanTin.ForeColor = Color.White;
            btNhanTin.Image = Properties.Resources.icons8_message_50;
            btNhanTin.ImageAlign = ContentAlignment.TopCenter;
            btNhanTin.Location = new Point(0, 370);
            btNhanTin.Margin = new Padding(3, 4, 3, 4);
            btNhanTin.Name = "btNhanTin";
            btNhanTin.Size = new Size(181, 127);
            btNhanTin.TabIndex = 0;
            btNhanTin.Text = "Nhắn Tin";
            btNhanTin.TextAlign = ContentAlignment.BottomCenter;
            btNhanTin.UseVisualStyleBackColor = false;
            btNhanTin.Click += btNhanTin_Click;
            // 
            // btCaiDat
            // 
            btCaiDat.BackColor = Color.FromArgb(0, 152, 168);
            btCaiDat.FlatAppearance.BorderSize = 0;
            btCaiDat.FlatStyle = FlatStyle.Flat;
            btCaiDat.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btCaiDat.ForeColor = Color.White;
            btCaiDat.Image = Properties.Resources.icons8_setting_50;
            btCaiDat.ImageAlign = ContentAlignment.TopCenter;
            btCaiDat.Location = new Point(0, 505);
            btCaiDat.Margin = new Padding(3, 4, 3, 4);
            btCaiDat.Name = "btCaiDat";
            btCaiDat.Size = new Size(181, 134);
            btCaiDat.TabIndex = 0;
            btCaiDat.Text = "Cài Đặt";
            btCaiDat.TextAlign = ContentAlignment.BottomCenter;
            btCaiDat.UseVisualStyleBackColor = false;
            btCaiDat.Click += btCaiDat_Click;
            // 
            // pContent
            // 
            pContent.Location = new Point(191, 3);
            pContent.Margin = new Padding(3, 4, 3, 4);
            pContent.Name = "pContent";
            pContent.Size = new Size(1117, 917);
            pContent.TabIndex = 1;
            pContent.Paint += pContent_Paint;
            // 
            // TrangCaNhan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            ClientSize = new Size(1309, 917);
            Controls.Add(pContent);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(3, 4, 3, 4);
            Name = "TrangCaNhan";
            Text = "TrangCaNhan";
            FormClosing += TrangCaNhan_FormClosing;
            Load += TrangCaNhan_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cpbAvatar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btHome;
        private Button btNhanTin;
        private Button btCaiDat;
        private Button btDangXuat;
        private CirclePictureBox cpbAvatar;
        private Button btTrangChu;
        private Panel pContent;
        private Label lbTen;
    }
}