using System.Drawing.Drawing2D;

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

        private void InitializeComponent()
        {
            avatar = new PictureBox();
            content = new TextBox();
            name = new Label();
            create_at = new Label();
            btDeleteComment = new Button();
            ((System.ComponentModel.ISupportInitialize)avatar).BeginInit();
            SuspendLayout();
            // 
            // avatar
            // 
            avatar.Location = new Point(8, 7);
            avatar.Name = "avatar";
            avatar.Size = new Size(35, 33);
            avatar.SizeMode = PictureBoxSizeMode.Zoom;
            avatar.TabIndex = 0;
            avatar.TabStop = false;
            // 
            // content
            // 
            content.Location = new Point(46, 45);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(377, 63);
            content.TabIndex = 1;
            // 
            // name
            // 
            name.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            name.Location = new Point(49, 7);
            name.Name = "name";
            name.Size = new Size(40, 15);
            name.TabIndex = 2;
            name.Text = "label1";
            // 
            // create_at
            // 
            create_at.ForeColor = SystemColors.GrayText;
            create_at.Location = new Point(49, 22);
            create_at.Name = "create_at";
            create_at.Size = new Size(97, 17);
            create_at.TabIndex = 3;
            create_at.Text = "label1";
            // 
            // btDeleteComment
            // 
            btDeleteComment.Location = new Point(399, 7);
            btDeleteComment.Name = "btDeleteComment";
            btDeleteComment.Size = new Size(24, 23);
            btDeleteComment.TabIndex = 4;
            btDeleteComment.Text = "X";
            btDeleteComment.UseVisualStyleBackColor = true;
            this.Controls.Add(this.btDeleteComment);
            // 
            // CommentControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            Controls.Add(btDeleteComment);
            Controls.Add(create_at);
            Controls.Add(name);
            Controls.Add(content);
            Controls.Add(avatar);
            Name = "CommentControl";
            Size = new Size(440, 119);
            ((System.ComponentModel.ISupportInitialize)avatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox avatar;
        private TextBox content;
        private Label name;
        private Label create_at;
        private Button btDeleteComment;
    }
}
