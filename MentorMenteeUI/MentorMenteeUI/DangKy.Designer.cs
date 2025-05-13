namespace MentorMenteeUI
{
    partial class DangKy
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
            panel2 = new Panel();
            cbVaiTro = new ComboBox();
            cbGioiTinh = new ComboBox();
            circlePictureBox1 = new CirclePictureBox();
            btChonAvatar = new Button();
            btDangKy = new Button();
            tbMatKhau = new TextBox();
            tbEmail = new TextBox();
            label5 = new Label();
            tbHoTen = new TextBox();
            label4 = new Label();
            label6 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circlePictureBox1).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(cbVaiTro);
            panel2.Controls.Add(cbGioiTinh);
            panel2.Controls.Add(circlePictureBox1);
            panel2.Controls.Add(btChonAvatar);
            panel2.Controls.Add(btDangKy);
            panel2.Controls.Add(tbMatKhau);
            panel2.Controls.Add(tbEmail);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(tbHoTen);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(3, 115);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(559, 789);
            panel2.TabIndex = 2;
            // 
            // cbVaiTro
            // 
            cbVaiTro.Font = new Font("Sans Serif Collection", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbVaiTro.FormattingEnabled = true;
            cbVaiTro.Items.AddRange(new object[] { "Mentor", "Mentee" });
            cbVaiTro.Location = new Point(41, 551);
            cbVaiTro.Margin = new Padding(3, 4, 3, 4);
            cbVaiTro.Name = "cbVaiTro";
            cbVaiTro.Size = new Size(471, 56);
            cbVaiTro.TabIndex = 5;
            // 
            // cbGioiTinh
            // 
            cbGioiTinh.Font = new Font("Sans Serif Collection", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbGioiTinh.FormattingEnabled = true;
            cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cbGioiTinh.Location = new Point(41, 247);
            cbGioiTinh.Margin = new Padding(3, 4, 3, 4);
            cbGioiTinh.Name = "cbGioiTinh";
            cbGioiTinh.Size = new Size(471, 56);
            cbGioiTinh.TabIndex = 2;
            // 
            // circlePictureBox1
            // 
            circlePictureBox1.Image = Properties.Resources.blankAvatar;
            circlePictureBox1.Location = new Point(165, 4);
            circlePictureBox1.Margin = new Padding(3, 4, 3, 4);
            circlePictureBox1.Name = "circlePictureBox1";
            circlePictureBox1.Size = new Size(106, 97);
            circlePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            circlePictureBox1.TabIndex = 3;
            circlePictureBox1.TabStop = false;
            // 
            // btChonAvatar
            // 
            btChonAvatar.Anchor = AnchorStyles.None;
            btChonAvatar.BackColor = Color.FromArgb(0, 116, 152);
            btChonAvatar.FlatAppearance.BorderSize = 0;
            btChonAvatar.FlatStyle = FlatStyle.Flat;
            btChonAvatar.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btChonAvatar.ForeColor = Color.White;
            btChonAvatar.Location = new Point(278, 24);
            btChonAvatar.Margin = new Padding(3, 4, 3, 4);
            btChonAvatar.Name = "btChonAvatar";
            btChonAvatar.Padding = new Padding(0, 4, 0, 0);
            btChonAvatar.Size = new Size(86, 65);
            btChonAvatar.TabIndex = 2;
            btChonAvatar.Text = "Chọn";
            btChonAvatar.UseVisualStyleBackColor = false;
            btChonAvatar.Click += btChonAvatar_Click;
            // 
            // btDangKy
            // 
            btDangKy.Anchor = AnchorStyles.None;
            btDangKy.BackColor = Color.FromArgb(0, 116, 152);
            btDangKy.FlatAppearance.BorderSize = 0;
            btDangKy.FlatStyle = FlatStyle.Flat;
            btDangKy.Font = new Font("Sans Serif Collection", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btDangKy.ForeColor = Color.White;
            btDangKy.Location = new Point(41, 681);
            btDangKy.Margin = new Padding(3, 4, 3, 4);
            btDangKy.Name = "btDangKy";
            btDangKy.Padding = new Padding(0, 4, 0, 0);
            btDangKy.Size = new Size(472, 77);
            btDangKy.TabIndex = 6;
            btDangKy.Text = "ĐĂNG KÝ";
            btDangKy.UseVisualStyleBackColor = false;
            btDangKy.Click += btDangKy_Click;
            // 
            // tbMatKhau
            // 
            tbMatKhau.Font = new Font("Sans Serif Collection", 9.749998F);
            tbMatKhau.Location = new Point(41, 445);
            tbMatKhau.Margin = new Padding(3, 4, 3, 4);
            tbMatKhau.Name = "tbMatKhau";
            tbMatKhau.PlaceholderText = "Nhập Mật Khẩu";
            tbMatKhau.Size = new Size(471, 51);
            tbMatKhau.TabIndex = 4;
            tbMatKhau.UseSystemPasswordChar = true;
            // 
            // tbEmail
            // 
            tbEmail.Font = new Font("Sans Serif Collection", 9.749998F);
            tbEmail.Location = new Point(41, 344);
            tbEmail.Margin = new Padding(3, 4, 3, 4);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Nhập Email";
            tbEmail.Size = new Size(471, 51);
            tbEmail.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ImageAlign = ContentAlignment.TopLeft;
            label5.Location = new Point(31, 404);
            label5.Name = "label5";
            label5.Size = new Size(120, 68);
            label5.TabIndex = 0;
            label5.Text = "Mật Khẩu:";
            // 
            // tbHoTen
            // 
            tbHoTen.Font = new Font("Sans Serif Collection", 9.749998F);
            tbHoTen.Location = new Point(41, 141);
            tbHoTen.Margin = new Padding(3, 4, 3, 4);
            tbHoTen.Name = "tbHoTen";
            tbHoTen.PlaceholderText = "Nhập Họ Và Tên";
            tbHoTen.Size = new Size(471, 51);
            tbHoTen.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ImageAlign = ContentAlignment.TopLeft;
            label4.Location = new Point(31, 303);
            label4.Name = "label4";
            label4.Size = new Size(85, 68);
            label4.TabIndex = 0;
            label4.Text = "Email:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ImageAlign = ContentAlignment.TopLeft;
            label6.Location = new Point(31, 505);
            label6.Name = "label6";
            label6.Size = new Size(95, 68);
            label6.TabIndex = 0;
            label6.Text = "Vai Trò:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ImageAlign = ContentAlignment.TopLeft;
            label3.Location = new Point(31, 201);
            label3.Name = "label3";
            label3.Size = new Size(114, 68);
            label3.TabIndex = 0;
            label3.Text = "Giới Tính:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ImageAlign = ContentAlignment.TopLeft;
            label2.Location = new Point(31, 100);
            label2.Name = "label2";
            label2.Size = new Size(123, 68);
            label2.TabIndex = 0;
            label2.Text = "Họ Và Tên:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(61, 4);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(442, 103);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(90, 23);
            label1.Name = "label1";
            label1.Size = new Size(194, 126);
            label1.TabIndex = 0;
            label1.Text = "Đăng Ký";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Location = new Point(347, 32);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.3167152F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 87.68328F));
            tableLayoutPanel1.Size = new Size(559, 909);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // DangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            ClientSize = new Size(1245, 959);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DangKy";
            Text = "DangKy";
            Load += DangKy_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)circlePictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void BtChonAvatar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Panel panel2;
        private Button btDangKy;
        private TextBox tbHoTen;
        private Label label3;
        private Label label2;
        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox tbMatKhau;
        private TextBox tbEmail;
        private Label label5;
        private Label label4;
        private Button btChonAvatar;
        private CirclePictureBox circlePictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label6;
        private ComboBox cbVaiTro;
        private ComboBox cbGioiTinh;
    }
}