namespace MentorMenteeUI
{
    partial class AddGoalForm
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
            label4 = new Label();
            btAddGoal = new Button();
            dtpDeadline = new DateTimePicker();
            rtbDescription = new RichTextBox();
            label2 = new Label();
            label1 = new Label();
            tbTitle = new TextBox();
            cbMentor = new ComboBox();
            btReloadMentorList = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(131, 205);
            label4.Name = "label4";
            label4.Size = new Size(189, 41);
            label4.TabIndex = 27;
            label4.Text = "Tên Mentor:";
            // 
            // btAddGoal
            // 
            btAddGoal.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAddGoal.Location = new Point(420, 666);
            btAddGoal.Name = "btAddGoal";
            btAddGoal.Size = new Size(256, 86);
            btAddGoal.TabIndex = 26;
            btAddGoal.Text = "Tạo mục tiêu";
            btAddGoal.UseVisualStyleBackColor = true;
            btAddGoal.Click += btAddGoal_Click;
            // 
            // dtpDeadline
            // 
            dtpDeadline.CalendarFont = new Font("Segoe UI", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDeadline.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDeadline.Location = new Point(420, 103);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new Size(604, 47);
            dtpDeadline.TabIndex = 25;
            // 
            // rtbDescription
            // 
            rtbDescription.Font = new Font("Segoe UI", 18F);
            rtbDescription.Location = new Point(420, 301);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(604, 330);
            rtbDescription.TabIndex = 24;
            rtbDescription.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(131, 103);
            label2.Name = "label2";
            label2.Size = new Size(160, 41);
            label2.TabIndex = 23;
            label2.Text = "Thời gian:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(131, 20);
            label1.Name = "label1";
            label1.Size = new Size(205, 41);
            label1.TabIndex = 22;
            label1.Text = "Tên mục tiêu:";
            // 
            // tbTitle
            // 
            tbTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTitle.Location = new Point(420, 21);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new Size(604, 47);
            tbTitle.TabIndex = 21;
            // 
            // cbMentor
            // 
            cbMentor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbMentor.FormattingEnabled = true;
            cbMentor.Location = new Point(420, 202);
            cbMentor.Name = "cbMentor";
            cbMentor.Size = new Size(604, 49);
            cbMentor.TabIndex = 28;
            // 
            // btReloadMentorList
            // 
            btReloadMentorList.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btReloadMentorList.Location = new Point(1030, 205);
            btReloadMentorList.Name = "btReloadMentorList";
            btReloadMentorList.Size = new Size(50, 49);
            btReloadMentorList.TabIndex = 29;
            btReloadMentorList.Text = "↺";
            btReloadMentorList.UseVisualStyleBackColor = true;
            btReloadMentorList.Click += btReloadMentorList_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(131, 301);
            label3.Name = "label3";
            label3.Size = new Size(115, 41);
            label3.TabIndex = 30;
            label3.Text = "Mô Tả:";
            // 
            // AddGoalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 776);
            Controls.Add(label3);
            Controls.Add(btReloadMentorList);
            Controls.Add(cbMentor);
            Controls.Add(label4);
            Controls.Add(btAddGoal);
            Controls.Add(dtpDeadline);
            Controls.Add(rtbDescription);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbTitle);
            Name = "AddGoalForm";
            Text = "AddGoalForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private Button btAddGoal;
        private DateTimePicker dtpDeadline;
        private RichTextBox rtbDescription;
        private Label label2;
        private Label label1;
        private TextBox tbTitle;
        private ComboBox cbMentor;
        private Button btReloadMentorList;
        private Label label3;
    }
}