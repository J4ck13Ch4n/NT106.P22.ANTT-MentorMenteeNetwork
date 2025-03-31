namespace MentorMenteeUI
{
    partial class DangNhap
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3 = new Panel();
            label4 = new Label();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            btDangNhap = new Button();
            tbMatKhau = new TextBox();
            tbEmail = new TextBox();
            label3 = new Label();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(panel3, 0, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Location = new Point(349, 64);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.5625F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 78.4375F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 131F));
            tableLayoutPanel1.Size = new Size(559, 712);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(label4);
            panel3.Controls.Add(linkLabel2);
            panel3.Controls.Add(linkLabel1);
            panel3.Location = new Point(61, 587);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(442, 117);
            panel3.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sans Serif Collection", 9.749998F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(15, 59);
            label4.Name = "label4";
            label4.Size = new Size(257, 42);
            label4.TabIndex = 1;
            label4.Text = "Chưa có tài khoản ?";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Sans Serif Collection", 9.749998F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkColor = Color.FromArgb(0, 116, 152);
            linkLabel2.Location = new Point(289, 59);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(123, 42);
            linkLabel2.TabIndex = 0;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Đăng ký";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Sans Serif Collection", 9.749998F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.FromArgb(0, 116, 152);
            linkLabel1.Location = new Point(113, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(221, 42);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Quên mật khẩu ?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(61, 4);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(442, 117);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(55, 17);
            label1.Name = "label1";
            label1.Size = new Size(357, 91);
            label1.TabIndex = 0;
            label1.Text = "Đăng Nhập";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(btDangNhap);
            panel2.Controls.Add(tbMatKhau);
            panel2.Controls.Add(tbEmail);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(3, 129);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(559, 447);
            panel2.TabIndex = 2;
            // 
            // btDangNhap
            // 
            btDangNhap.Anchor = AnchorStyles.None;
            btDangNhap.BackColor = Color.FromArgb(0, 116, 152);
            btDangNhap.FlatAppearance.BorderSize = 0;
            btDangNhap.FlatStyle = FlatStyle.Flat;
            btDangNhap.Font = new Font("Sans Serif Collection", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btDangNhap.ForeColor = Color.White;
            btDangNhap.Location = new Point(42, 337);
            btDangNhap.Margin = new Padding(3, 4, 3, 4);
            btDangNhap.Name = "btDangNhap";
            btDangNhap.Padding = new Padding(0, 4, 0, 0);
            btDangNhap.Size = new Size(472, 77);
            btDangNhap.TabIndex = 2;
            btDangNhap.Text = "ĐĂNG NHẬP";
            btDangNhap.UseVisualStyleBackColor = false;
            btDangNhap.Click += btDangNhap_Click;
            // 
            // tbMatKhau
            // 
            tbMatKhau.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMatKhau.Location = new Point(42, 211);
            tbMatKhau.Margin = new Padding(3, 4, 3, 4);
            tbMatKhau.Name = "tbMatKhau";
            tbMatKhau.PlaceholderText = "Nhập Mật Khẩu";
            tbMatKhau.Size = new Size(471, 66);
            tbMatKhau.TabIndex = 1;
            // 
            // tbEmail
            // 
            tbEmail.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbEmail.Location = new Point(42, 71);
            tbEmail.Margin = new Padding(3, 4, 3, 4);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Nhập Email";
            tbEmail.Size = new Size(471, 66);
            tbEmail.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ImageAlign = ContentAlignment.TopLeft;
            label3.Location = new Point(32, 169);
            label3.Name = "label3";
            label3.Size = new Size(160, 49);
            label3.TabIndex = 0;
            label3.Text = "Mật Khẩu:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ImageAlign = ContentAlignment.TopLeft;
            label2.Location = new Point(32, 29);
            label2.Name = "label2";
            label2.Size = new Size(107, 49);
            label2.TabIndex = 0;
            label2.Text = "Email:";
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            ClientSize = new Size(1245, 836);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DangNhap";
            Text = "DangNhap";
            Load += DangNhap_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private LinkLabel linkLabel1;
        private Button btDangNhap;
        private TextBox tbEmail;
        private Label label2;
        private TextBox tbMatKhau;
        private Label label3;
        private LinkLabel linkLabel2;
        private Label label4;
    }
}