namespace MentorMenteeUI
{
    partial class KetNoiControl
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
            lbMentor = new ListBox();
            btConnect = new Button();
            SuspendLayout();
            // 
            // lbMentor
            // 
            lbMentor.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbMentor.FormattingEnabled = true;
            lbMentor.Items.AddRange(new object[] { "Mentor 1", "Mentor 2", "Mentor 3" });
            lbMentor.Location = new Point(67, 71);
            lbMentor.Name = "lbMentor";
            lbMentor.Size = new Size(889, 652);
            lbMentor.TabIndex = 0;
            // 
            // btConnect
            // 
            btConnect.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btConnect.Location = new Point(355, 777);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(351, 83);
            btConnect.TabIndex = 1;
            btConnect.Text = "Kết nối";
            btConnect.UseVisualStyleBackColor = true;
            // 
            // KetNoiControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(btConnect);
            Controls.Add(lbMentor);
            Name = "KetNoiControl";
            Size = new Size(1029, 917);
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbMentor;
        private Button btConnect;
    }
}
