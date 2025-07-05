using AxWMPLib;
using System.Drawing.Drawing2D;

namespace MentorMenteeUI
{
    partial class PostControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostControl));
            avatar = new PictureBox();
            name = new Label();
            content = new TextBox();
            picture = new PictureBox();
            create_at = new Label();
            btLike = new Button();
            commentList = new FlowLayoutPanel();
            video = new AxWindowsMediaPlayer();
            btComment = new Button();
            avatar_current = new PictureBox();
            commentContent = new TextBox();
            ((System.ComponentModel.ISupportInitialize)avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)video).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avatar_current).BeginInit();
            SuspendLayout();
            // 
            // avatar
            // 
            avatar.Location = new Point(20, 20);
            avatar.Name = "avatar";
            avatar.Size = new Size(35, 33);
            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar.TabIndex = 0;
            avatar.TabStop = false;
            // 
            // name
            // 
            name.AutoSize = true;
            name.Location = new Point(61, 19);
            name.Name = "name";
            name.Size = new Size(38, 15);
            name.TabIndex = 1;
            name.Text = "label1";
            // 
            // content
            // 
            content.ForeColor = Color.Gray;
            content.Location = new Point(61, 46);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(567, 42);
            content.TabIndex = 2;
            // 
            // picture
            // 
            picture.Location = new Point(61, 94);
            picture.Name = "picture";
            picture.Size = new Size(567, 179);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.TabIndex = 3;
            picture.TabStop = false;
            // 
            // create_at
            // 
            create_at.AutoSize = true;
            create_at.Location = new Point(579, 19);
            create_at.Name = "create_at";
            create_at.Size = new Size(38, 15);
            create_at.TabIndex = 4;
            create_at.Text = "label2";
            // 
            // btLike
            // 
            btLike.Location = new Point(74, 279);
            btLike.Name = "btLike";
            btLike.Size = new Size(75, 23);
            btLike.TabIndex = 5;
            btLike.Text = "Like";
            btLike.UseVisualStyleBackColor = true;
            // 
            // commentList
            // 
            commentList.AutoScroll = true;
            commentList.FlowDirection = FlowDirection.TopDown;
            commentList.Location = new Point(61, 308);
            commentList.Name = "commentList";
            commentList.Size = new Size(567, 232);
            commentList.TabIndex = 6;
            // 
            // video
            // 
            video.Enabled = true;
            video.Location = new Point(61, 94);
            video.Name = "video";
            video.OcxState = (AxHost.State)resources.GetObject("video.OcxState");
            video.Size = new Size(567, 179);
            video.TabIndex = 7;
            // 
            // btComment
            // 
            btComment.Location = new Point(542, 545);
            btComment.Name = "btComment";
            btComment.Size = new Size(75, 34);
            btComment.TabIndex = 8;
            btComment.Text = "Comment";
            btComment.UseVisualStyleBackColor = true;
            // 
            // avatar_current
            // 
            avatar_current.Location = new Point(64, 546);
            avatar_current.Name = "avatar_current";
            avatar_current.Size = new Size(35, 33);
            avatar_current.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar_current.TabIndex = 1;
            avatar_current.TabStop = false;
            // 
            // commentContent
            // 
            commentContent.ForeColor = Color.Gray;
            commentContent.Location = new Point(105, 546);
            commentContent.Multiline = true;
            commentContent.Name = "commentContent";
            commentContent.Size = new Size(420, 42);
            commentContent.TabIndex = 9;
            commentContent.Text = "Nhập bình luận...";
            // 
            // PostControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(commentContent);
            Controls.Add(avatar_current);
            Controls.Add(btComment);
            Controls.Add(video);
            Controls.Add(commentList);
            Controls.Add(btLike);
            Controls.Add(create_at);
            Controls.Add(picture);
            Controls.Add(content);
            Controls.Add(name);
            Controls.Add(avatar);
            Name = "PostControl";
            Size = new Size(643, 605);
            ((System.ComponentModel.ISupportInitialize)avatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)video).EndInit();
            ((System.ComponentModel.ISupportInitialize)avatar_current).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion




        private PictureBox avatar;
        private Label name;
        private TextBox content;
        private PictureBox picture;
        private Label create_at;
        private Button btLike;
        private FlowLayoutPanel commentList;
        private AxWindowsMediaPlayer video;
        private Button btComment;
        private PictureBox avatar_current;
        private TextBox commentContent;
    }
}
