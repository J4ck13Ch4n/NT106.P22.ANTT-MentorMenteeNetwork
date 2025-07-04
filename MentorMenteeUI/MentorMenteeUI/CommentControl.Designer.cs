namespace MentorMenteeUI
{
    partial class CommentControl
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
        /// 
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Button btnDelete;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();

            // picAvatar
            this.picAvatar.Location = new System.Drawing.Point(3, 3);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(30, 30);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // lblUser
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(40, 3);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(50, 20);

            // lblContent
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(40, 23);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(72, 20);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(200, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 25);
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.btnDelete);
            this.Size = new System.Drawing.Size(270, 40);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
