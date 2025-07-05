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
            countLike = new Label();
            role = new Label();
            btDeletePost = new Button();
            ((System.ComponentModel.ISupportInitialize)avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)video).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avatar_current).BeginInit();
            SuspendLayout();
            // 
            // avatar
            // 
            avatar.Location = new Point(15, 12);
            avatar.Name = "avatar";
            avatar.Size = new Size(35, 33);
            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar.TabIndex = 0;
            avatar.TabStop = false;
            // 
            // name
            // 
            name.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            name.Location = new Point(57, 12);
            name.Name = "name";
            name.Size = new Size(116, 20);
            name.TabIndex = 1;
            name.Text = "label1";
            // 
            // content
            // 
            content.ForeColor = Color.Gray;
            content.Location = new Point(403, 65);
            content.Multiline = true;
            content.Name = "content";
            content.ReadOnly = true;
            content.Size = new Size(447, 53);
            content.TabIndex = 2;
            // 
            // picture
            // 
            picture.Location = new Point(58, 65);
            picture.Name = "picture";
            picture.Size = new Size(321, 376);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.TabIndex = 3;
            picture.TabStop = false;
            // 
            // create_at
            // 
            create_at.ForeColor = SystemColors.GrayText;
            create_at.Location = new Point(57, 45);
            create_at.Name = "create_at";
            create_at.Size = new Size(97, 17);
            create_at.TabIndex = 4;
            create_at.Text = "label2";
            // 
            // btLike
            // 
            btLike.Location = new Point(57, 477);
            btLike.Name = "btLike";
            btLike.Size = new Size(75, 23);
            btLike.TabIndex = 5;
            btLike.Text = "Thích";
            btLike.UseVisualStyleBackColor = true;
            // 
            // commentList
            // 
            commentList.AutoScroll = true;
            commentList.BackColor = SystemColors.ButtonHighlight;
            commentList.FlowDirection = FlowDirection.TopDown;
            commentList.Location = new Point(403, 124);
            commentList.Name = "commentList";
            commentList.Size = new Size(447, 317);
            commentList.TabIndex = 6;
            commentList.WrapContents = false;
            // 
            // video
            // 
            video.Enabled = true;
            video.Location = new Point(57, 65);
            video.Name = "video";
            video.OcxState = (AxHost.State)resources.GetObject("video.OcxState");
            video.Size = new Size(340, 376);
            video.TabIndex = 7;
            video.Visible = false;
            // 
            // btComment
            // 
            btComment.Location = new Point(775, 448);
            btComment.Name = "btComment";
            btComment.Size = new Size(75, 34);
            btComment.TabIndex = 8;
            btComment.Text = "Bình luận";
            btComment.UseVisualStyleBackColor = true;
            // 
            // avatar_current
            // 
            avatar_current.Location = new Point(403, 448);
            avatar_current.Name = "avatar_current";
            avatar_current.Size = new Size(35, 33);
            avatar_current.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar_current.TabIndex = 1;
            avatar_current.TabStop = false;
            // 
            // commentContent
            // 
            commentContent.ForeColor = Color.Gray;
            commentContent.Location = new Point(453, 448);
            commentContent.Multiline = true;
            commentContent.Name = "commentContent";
            commentContent.Size = new Size(316, 53);
            commentContent.TabIndex = 9;
            // 
            // countLike
            // 
            countLike.AutoSize = true;
            countLike.Location = new Point(57, 446);
            countLike.Name = "countLike";
            countLike.Size = new Size(38, 15);
            countLike.TabIndex = 10;
            countLike.Text = "label2";
            // 
            // role
            // 
            role.AutoSize = true;
            role.BackColor = SystemColors.GradientInactiveCaption;
            role.ForeColor = SystemColors.InfoText;
            role.Location = new Point(57, 30);
            role.Name = "role";
            role.Size = new Size(38, 15);
            role.TabIndex = 11;
            role.Text = "label2";
            // 
            // btDeletePost
            // 
            btDeletePost.Location = new Point(824, 8);
            btDeletePost.Name = "btDeletePost";
            btDeletePost.Size = new Size(26, 24);
            btDeletePost.TabIndex = 12;
            btDeletePost.Text = "X";
            btDeletePost.UseVisualStyleBackColor = true;
            // 
            // PostControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            Controls.Add(btDeletePost);
            Controls.Add(role);
            Controls.Add(countLike);
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
            Size = new Size(868, 517);
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
        private Label countLike;
        private Label role;
        private Button btDeletePost;
    }
}
