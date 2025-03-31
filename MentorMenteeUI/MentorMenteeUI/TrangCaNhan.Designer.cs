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
            circlePictureBox1 = new CirclePictureBox();
            label1 = new Label();
            btDangXuat = new Button();
            btTrangChu = new Button();
            btNhanTin = new Button();
            btCaiDat = new Button();
            pContent = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circlePictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 152, 168);
            panel1.Controls.Add(circlePictureBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btDangXuat);
            panel1.Controls.Add(btTrangChu);
            panel1.Controls.Add(btNhanTin);
            panel1.Controls.Add(btCaiDat);
            panel1.Location = new Point(1, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 688);
            panel1.TabIndex = 0;
            // 
            // circlePictureBox1
            // 
            circlePictureBox1.Image = Properties.Resources.blankAvatar;
            circlePictureBox1.Location = new Point(11, 57);
            circlePictureBox1.Name = "circlePictureBox1";
            circlePictureBox1.Size = new Size(100, 83);
            circlePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            circlePictureBox1.TabIndex = 3;
            circlePictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(111, 57);
            label1.Name = "label1";
            label1.Size = new Size(100, 72);
            label1.TabIndex = 2;
            label1.Text = "HoTen";
            // 
            // btDangXuat
            // 
            btDangXuat.BackColor = Color.FromArgb(0, 152, 168);
            btDangXuat.FlatAppearance.BorderSize = 0;
            btDangXuat.FlatStyle = FlatStyle.Flat;
            btDangXuat.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btDangXuat.ForeColor = Color.White;
            btDangXuat.Location = new Point(0, 395);
            btDangXuat.Name = "btDangXuat";
            btDangXuat.Size = new Size(237, 58);
            btDangXuat.TabIndex = 0;
            btDangXuat.Text = "Đăng Xuất";
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
            btTrangChu.Location = new Point(3, 175);
            btTrangChu.Name = "btTrangChu";
            btTrangChu.Size = new Size(234, 58);
            btTrangChu.TabIndex = 0;
            btTrangChu.Text = "Trang Chủ";
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
            btNhanTin.Location = new Point(3, 248);
            btNhanTin.Name = "btNhanTin";
            btNhanTin.Size = new Size(234, 58);
            btNhanTin.TabIndex = 0;
            btNhanTin.Text = "Nhắn Tin";
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
            btCaiDat.Location = new Point(3, 321);
            btCaiDat.Name = "btCaiDat";
            btCaiDat.Size = new Size(234, 58);
            btCaiDat.TabIndex = 0;
            btCaiDat.Text = "Cài Đặt";
            btCaiDat.UseVisualStyleBackColor = false;
            // 
            // pContent
            // 
            pContent.Location = new Point(244, 2);
            pContent.Name = "pContent";
            pContent.Size = new Size(900, 688);
            pContent.TabIndex = 1;
            pContent.Paint += pContent_Paint;
            // 
            // TrangCaNhan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            ClientSize = new Size(1145, 688);
            Controls.Add(pContent);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            Name = "TrangCaNhan";
            Text = "TrangCaNhan";
            FormClosing += TrangCaNhan_FormClosing;
            Load += TrangCaNhan_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)circlePictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btHome;
        private Button btNhanTin;
        private Button btCaiDat;
        private Button btDangXuat;
        private Label label1;
        private CirclePictureBox circlePictureBox1;
        private Button btTrangChu;
        private Panel pContent;
    }
}