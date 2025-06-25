namespace MentorMenteeUI
{
    partial class CaiDatControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Bio = new RichTextBox();
            Gender = new ComboBox();
            Role = new ComboBox();
            Email = new TextBox();
            Username = new TextBox();
            btLuu = new Button();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // Bio
            // 
            Bio.Location = new Point(267, 441);
            Bio.Margin = new Padding(3, 4, 3, 4);
            Bio.Name = "Bio";
            Bio.Size = new Size(575, 143);
            Bio.TabIndex = 19;
            Bio.Text = "";
            // 
            // Gender
            // 
            Gender.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Gender.FormattingEnabled = true;
            Gender.Location = new Point(269, 243);
            Gender.Margin = new Padding(3, 4, 3, 4);
            Gender.Name = "Gender";
            Gender.Size = new Size(575, 59);
            Gender.TabIndex = 17;
            // 
            // Role
            // 
            Role.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Role.FormattingEnabled = true;
            Role.Location = new Point(267, 139);
            Role.Margin = new Padding(3, 4, 3, 4);
            Role.Name = "Role";
            Role.Size = new Size(575, 59);
            Role.TabIndex = 18;
            // 
            // Email
            // 
            Email.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Email.Location = new Point(269, 348);
            Email.Margin = new Padding(3, 4, 3, 4);
            Email.Name = "Email";
            Email.Size = new Size(575, 47);
            Email.TabIndex = 15;
            // 
            // Username
            // 
            Username.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Username.Location = new Point(267, 49);
            Username.Margin = new Padding(3, 4, 3, 4);
            Username.Name = "Username";
            Username.Size = new Size(575, 47);
            Username.TabIndex = 16;
            // 
            // btLuu
            // 
            btLuu.BackColor = Color.FromArgb(0, 132, 168);
            btLuu.FlatAppearance.BorderSize = 0;
            btLuu.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btLuu.ForeColor = Color.White;
            btLuu.Location = new Point(451, 642);
            btLuu.Margin = new Padding(3, 4, 3, 4);
            btLuu.Name = "btLuu";
            btLuu.Size = new Size(144, 69);
            btLuu.TabIndex = 13;
            btLuu.Text = "Lưu";
            btLuu.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(135, 427);
            label7.Name = "label7";
            label7.Size = new Size(91, 68);
            label7.TabIndex = 6;
            label7.Text = "Mô tả:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(150, 341);
            label5.Name = "label5";
            label5.Size = new Size(91, 68);
            label5.TabIndex = 9;
            label5.Text = "Email:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(113, 243);
            label4.Name = "label4";
            label4.Size = new Size(120, 68);
            label4.TabIndex = 10;
            label4.Text = "Giới tính:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(139, 139);
            label3.Name = "label3";
            label3.Size = new Size(100, 68);
            label3.TabIndex = 11;
            label3.Text = "Vai trò:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(98, 49);
            label2.Name = "label2";
            label2.Size = new Size(128, 68);
            label2.TabIndex = 12;
            label2.Text = "Họ và tên:";
            // 
            // CaiDatControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(Bio);
            Controls.Add(Gender);
            Controls.Add(Role);
            Controls.Add(Email);
            Controls.Add(Username);
            Controls.Add(btLuu);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CaiDatControl";
            Size = new Size(1117, 917);
            Load += CaiDatControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox Bio;
        private ComboBox Gender;
        private ComboBox Role;
        private TextBox Email;
        private TextBox Username;
        private Button btLuu;
        private Label label7;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
