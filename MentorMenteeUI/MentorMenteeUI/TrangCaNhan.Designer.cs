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
            lbTen = new Label();
            cpbAvatar = new CirclePictureBox();
            btDangXuat = new Button();
            btTrangChu = new Button();
            btNhanTin = new Button();
            btCaiDat = new Button();
            pContent = new Panel();
            btGoal = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cpbAvatar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 152, 168);
            panel1.Controls.Add(btGoal);
            panel1.Controls.Add(lbTen);
            panel1.Controls.Add(cpbAvatar);
            panel1.Controls.Add(btDangXuat);
            panel1.Controls.Add(btTrangChu);
            panel1.Controls.Add(btNhanTin);
            panel1.Controls.Add(btCaiDat);
            panel1.Location = new Point(1, -1);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(184, 921);
            panel1.TabIndex = 0;
            // 
            // lbTen
            // 
            lbTen.AutoSize = true;
            lbTen.Font = new Font("Sans Serif Collection", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTen.ForeColor = Color.White;
            lbTen.Location = new Point(21, 138);
            lbTen.MaximumSize = new Size(125, 91);
            lbTen.Name = "lbTen";
            lbTen.Size = new Size(125, 91);
            lbTen.TabIndex = 2;
            lbTen.Text = "HoTen";
            // 
            // cpbAvatar
            // 
            cpbAvatar.Image = Properties.Resources.blankAvatar;
            cpbAvatar.Location = new Point(40, 14);
            cpbAvatar.Margin = new Padding(3, 4, 3, 4);
            cpbAvatar.Name = "cpbAvatar";
            cpbAvatar.Size = new Size(106, 107);
            cpbAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            cpbAvatar.TabIndex = 3;
            cpbAvatar.TabStop = false;
            // 
            // btDangXuat
            // 
            btDangXuat.BackColor = Color.FromArgb(0, 152, 168);
            btDangXuat.FlatAppearance.BorderSize = 0;
            btDangXuat.FlatStyle = FlatStyle.Flat;
            btDangXuat.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btDangXuat.ForeColor = Color.White;
            btDangXuat.Image = Properties.Resources.icons8_log_out_50;
            btDangXuat.Location = new Point(0, 649);
            btDangXuat.Margin = new Padding(3, 4, 3, 4);
            btDangXuat.Name = "btDangXuat";
            btDangXuat.Size = new Size(181, 96);
            btDangXuat.TabIndex = 0;
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
            btTrangChu.Location = new Point(0, 233);
            btTrangChu.Margin = new Padding(3, 4, 3, 4);
            btTrangChu.Name = "btTrangChu";
            btTrangChu.Size = new Size(181, 96);
            btTrangChu.TabIndex = 0;
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
            btNhanTin.Location = new Point(0, 337);
            btNhanTin.Margin = new Padding(3, 4, 3, 4);
            btNhanTin.Name = "btNhanTin";
            btNhanTin.Size = new Size(181, 96);
            btNhanTin.TabIndex = 0;
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
            btCaiDat.Location = new Point(0, 545);
            btCaiDat.Margin = new Padding(3, 4, 3, 4);
            btCaiDat.Name = "btCaiDat";
            btCaiDat.Size = new Size(181, 96);
            btCaiDat.TabIndex = 0;
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
            // btGoal
            // 
            btGoal.BackColor = Color.FromArgb(0, 152, 168);
            btGoal.FlatAppearance.BorderSize = 0;
            btGoal.FlatStyle = FlatStyle.Flat;
            btGoal.Font = new Font("Sans Serif Collection", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btGoal.ForeColor = Color.White;
            btGoal.Image = Properties.Resources.icons8_goal_50;
            btGoal.Location = new Point(0, 441);
            btGoal.Margin = new Padding(3, 4, 3, 4);
            btGoal.Name = "btGoal";
            btGoal.Size = new Size(181, 96);
            btGoal.TabIndex = 4;
            btGoal.TextAlign = ContentAlignment.BottomCenter;
            btGoal.UseVisualStyleBackColor = false;
            btGoal.Click += btGoal_Click;
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
        private Button btGoal;
    }
}