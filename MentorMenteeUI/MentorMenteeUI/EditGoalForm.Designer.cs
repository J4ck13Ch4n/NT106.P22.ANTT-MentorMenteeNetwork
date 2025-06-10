namespace MentorMenteeUI
{
    partial class EditGoalForm
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
            btAddGoal = new Button();
            rtbDetailedFeedback = new RichTextBox();
            label1 = new Label();
            tbStatus = new TextBox();
            label2 = new Label();
            cbFeedback = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // btAddGoal
            // 
            btAddGoal.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAddGoal.Location = new Point(359, 558);
            btAddGoal.Name = "btAddGoal";
            btAddGoal.Size = new Size(239, 66);
            btAddGoal.TabIndex = 34;
            btAddGoal.Text = "Chỉnh sửa";
            btAddGoal.UseVisualStyleBackColor = true;
            btAddGoal.Click += btAddGoal_Click;
            // 
            // rtbDetailedFeedback
            // 
            rtbDetailedFeedback.Font = new Font("Segoe UI", 18F);
            rtbDetailedFeedback.Location = new Point(359, 207);
            rtbDetailedFeedback.Name = "rtbDetailedFeedback";
            rtbDetailedFeedback.Size = new Size(604, 330);
            rtbDetailedFeedback.TabIndex = 32;
            rtbDetailedFeedback.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(70, 23);
            label1.Name = "label1";
            label1.Size = new Size(178, 41);
            label1.TabIndex = 30;
            label1.Text = "Tình Trạng:";
            // 
            // tbStatus
            // 
            tbStatus.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbStatus.Location = new Point(359, 24);
            tbStatus.Name = "tbStatus";
            tbStatus.Size = new Size(604, 47);
            tbStatus.TabIndex = 29;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(66, 207);
            label2.Name = "label2";
            label2.Size = new Size(271, 41);
            label2.TabIndex = 35;
            label2.Text = "Đánh Giá Chi Tiết:";
            // 
            // cbFeedback
            // 
            cbFeedback.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbFeedback.FormattingEnabled = true;
            cbFeedback.Items.AddRange(new object[] { "★", "★★", "★★★", "★★★★", "★★★★★" });
            cbFeedback.Location = new Point(359, 110);
            cbFeedback.Name = "cbFeedback";
            cbFeedback.Size = new Size(604, 49);
            cbFeedback.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(70, 113);
            label4.Name = "label4";
            label4.Size = new Size(152, 41);
            label4.TabIndex = 36;
            label4.Text = "Đánh giá:";
            // 
            // EditGoalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 660);
            Controls.Add(cbFeedback);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(btAddGoal);
            Controls.Add(rtbDetailedFeedback);
            Controls.Add(label1);
            Controls.Add(tbStatus);
            Name = "EditGoalForm";
            Text = "EditGoalForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btAddGoal;
        private RichTextBox rtbDetailedFeedback;
        private Label label1;
        private TextBox tbStatus;
        private Label label2;
        private ComboBox cbFeedback;
        private Label label4;
    }
}