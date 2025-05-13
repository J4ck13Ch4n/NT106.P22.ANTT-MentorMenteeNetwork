namespace MentorMenteeUI
{
    partial class DoiMatKhau
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
            btDangNhap = new Button();
            tbMaXacThuc = new TextBox();
            tbEmail = new TextBox();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(btDangNhap);
            panel2.Controls.Add(tbMaXacThuc);
            panel2.Controls.Add(tbEmail);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(3, 157);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(559, 551);
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
            btDangNhap.Location = new Point(42, 377);
            btDangNhap.Margin = new Padding(3, 4, 3, 4);
            btDangNhap.Name = "btDangNhap";
            btDangNhap.Padding = new Padding(0, 4, 0, 0);
            btDangNhap.Size = new Size(472, 77);
            btDangNhap.TabIndex = 3;
            btDangNhap.Text = "ĐỔI MẬT KHẨU";
            btDangNhap.UseVisualStyleBackColor = false;
            // 
            // tbMaXacThuc
            // 
            tbMaXacThuc.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMaXacThuc.Location = new Point(42, 211);
            tbMaXacThuc.Margin = new Padding(3, 4, 3, 4);
            tbMaXacThuc.Name = "tbMaXacThuc";
            tbMaXacThuc.PlaceholderText = "Nhập Mã Xác Thực";
            tbMaXacThuc.Size = new Size(471, 70);
            tbMaXacThuc.TabIndex = 2;
            // 
            // tbEmail
            // 
            tbEmail.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbEmail.Location = new Point(42, 71);
            tbEmail.Margin = new Padding(3, 4, 3, 4);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Nhập Email";
            tbEmail.Size = new Size(471, 70);
            tbEmail.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ImageAlign = ContentAlignment.TopLeft;
            label3.Location = new Point(32, 169);
            label3.Name = "label3";
            label3.Size = new Size(148, 68);
            label3.TabIndex = 0;
            label3.Text = "Mã Xác Thực:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ImageAlign = ContentAlignment.TopLeft;
            label2.Location = new Point(32, 29);
            label2.Name = "label2";
            label2.Size = new Size(85, 68);
            label2.TabIndex = 0;
            label2.Text = "Email:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(61, 18);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(442, 117);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(31, 19);
            label1.Name = "label1";
            label1.Size = new Size(280, 126);
            label1.TabIndex = 0;
            label1.Text = "Đổi Mật Khẩu";
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
            tableLayoutPanel1.Location = new Point(358, 84);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.5625F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 78.4375F));
            tableLayoutPanel1.Size = new Size(559, 712);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // DoiMatKhau
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            ClientSize = new Size(1274, 880);
            Controls.Add(tableLayoutPanel1);
            ForeColor = SystemColors.ActiveCaptionText;
            Margin = new Padding(3, 4, 3, 4);
            Name = "DoiMatKhau";
            Text = "DoiMatKhau";
            Load += DoiMatKhau_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Button btDangNhap;
        private TextBox tbMaXacThuc;
        private TextBox tbEmail;
        private Label label3;
        private Label label2;
        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}