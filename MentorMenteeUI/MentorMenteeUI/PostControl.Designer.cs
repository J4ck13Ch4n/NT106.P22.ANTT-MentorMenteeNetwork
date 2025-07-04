using AxWMPLib;

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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblLikeCount;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnUnlike;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.FlowLayoutPanel flowComments;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.PictureBox picImage;
        private AxWMPLib.AxWindowsMediaPlayer videoPlayer;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostControl));
            picAvatar = new PictureBox();
            lblUser = new Label();
            lblContent = new Label();
            lblLikeCount = new Label();
            btnLike = new Button();
            btnUnlike = new Button();
            btnDelete = new Button();
            flowComments = new FlowLayoutPanel();
            txtComment = new TextBox();
            btnAddComment = new Button();
            picImage = new PictureBox();
            videoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)videoPlayer).BeginInit();
            SuspendLayout();
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(3, 3);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(40, 40);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(50, 3);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(0, 15);
            lblUser.TabIndex = 1;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(3, 50);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(0, 15);
            lblContent.TabIndex = 2;
            // 
            // lblLikeCount
            // 
            lblLikeCount.AutoSize = true;
            lblLikeCount.Location = new Point(3, 75);
            lblLikeCount.Name = "lblLikeCount";
            lblLikeCount.Size = new Size(0, 15);
            lblLikeCount.TabIndex = 3;
            // 
            // btnLike
            // 
            btnLike.Location = new Point(120, 70);
            btnLike.Name = "btnLike";
            btnLike.Size = new Size(50, 25);
            btnLike.TabIndex = 4;
            btnLike.Text = "Like";
            btnLike.Click += btnLike_Click;
            // 
            // btnUnlike
            // 
            btnUnlike.Location = new Point(170, 70);
            btnUnlike.Name = "btnUnlike";
            btnUnlike.Size = new Size(60, 25);
            btnUnlike.TabIndex = 5;
            btnUnlike.Text = "Unlike";
            btnUnlike.Click += btnUnlike_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(240, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 25);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // flowComments
            // 
            flowComments.AutoScroll = true;
            flowComments.FlowDirection = FlowDirection.TopDown;
            flowComments.Location = new Point(3, 110);
            flowComments.Name = "flowComments";
            flowComments.Size = new Size(350, 120);
            flowComments.TabIndex = 7;
            // 
            // txtComment
            // 
            txtComment.Location = new Point(3, 240);
            txtComment.Name = "txtComment";
            txtComment.Size = new Size(250, 23);
            txtComment.TabIndex = 8;
            // 
            // btnAddComment
            // 
            btnAddComment.Location = new Point(260, 240);
            btnAddComment.Name = "btnAddComment";
            btnAddComment.Size = new Size(80, 27);
            btnAddComment.TabIndex = 9;
            btnAddComment.Text = "Bình luận";
            btnAddComment.Click += btnAddComment_Click;
            // 
            // picImage
            // 
            picImage.Location = new Point(3, 100);
            picImage.Name = "picImage";
            picImage.Size = new Size(200, 150);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            picImage.Visible = false;
            // 
            // axWindowsMediaPlayer1
            // 

            this.videoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).BeginInit();
            this.videoPlayer.Location = new System.Drawing.Point(3, 100);
            this.videoPlayer.Name = "videoPlayer";
            this.videoPlayer.Size = new System.Drawing.Size(300, 200);
            this.videoPlayer.Visible = false;
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).EndInit();

            this.Controls.Add(this.picImage);
            this.Controls.Add(this.videoPlayer);
            // 
            // PostControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(videoPlayer);
            Controls.Add(picAvatar);
            Controls.Add(lblUser);
            Controls.Add(lblContent);
            Controls.Add(lblLikeCount);
            Controls.Add(btnLike);
            Controls.Add(btnUnlike);
            Controls.Add(btnDelete);
            Controls.Add(flowComments);
            Controls.Add(txtComment);
            Controls.Add(btnAddComment);
            Name = "PostControl";
            Size = new Size(370, 280);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)videoPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion




    }
}
