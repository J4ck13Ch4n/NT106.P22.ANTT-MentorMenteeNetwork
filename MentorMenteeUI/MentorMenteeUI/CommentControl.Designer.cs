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
            ((System.ComponentModel.ISupportInitialize)avatar).BeginInit();
            SuspendLayout();
            // 
            // avatar
            // 
            avatar.Location = new Point(20, 15);
            avatar.Name = "avatar";
            avatar.Size = new Size(35, 33);
            avatar.TabIndex = 0;
            avatar.TabStop = false;
            // 
            // content
            // 
            content.Location = new Point(58, 53);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(252, 54);
            content.TabIndex = 1;
            // 
            // name
            // 
            name.AutoSize = true;
            name.Location = new Point(61, 24);
            name.Name = "name";
            name.Size = new Size(38, 15);
            name.TabIndex = 2;
            name.Text = "label1";
            // 
            // create_at
            // 
            create_at.AutoSize = true;
            create_at.Location = new Point(261, 24);
            create_at.Name = "create_at";
            create_at.Size = new Size(38, 15);
            create_at.TabIndex = 3;
            create_at.Text = "label1";
            // 
            // CommentControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(create_at);
            Controls.Add(name);
            Controls.Add(content);
            Controls.Add(avatar);
            Name = "CommentControl";
            Size = new Size(328, 125);
            ((System.ComponentModel.ISupportInitialize)avatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox avatar;
        private TextBox content;
        private Label name;
        private Label create_at;
    }
}
