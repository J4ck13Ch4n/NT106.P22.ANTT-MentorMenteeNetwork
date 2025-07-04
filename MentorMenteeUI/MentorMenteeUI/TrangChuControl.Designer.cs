namespace MentorMenteeUI
{
    partial class TrangChuControl
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

        private System.Windows.Forms.FlowLayoutPanel flowPosts;
        private System.Windows.Forms.TextBox txtPostContent;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnChooseVideo;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.TextBox txtVideoPath;
        private void InitializeComponent()
        {
            flowPosts = new FlowLayoutPanel();
            txtPostContent = new TextBox();
            btnPost = new Button();
            SuspendLayout();
            // 
            // flowPosts
            // 
            flowPosts.AutoScroll = true;
            flowPosts.FlowDirection = FlowDirection.TopDown;
            flowPosts.Location = new Point(9, 52);
            flowPosts.Margin = new Padding(3, 2, 3, 2);
            flowPosts.Name = "flowPosts";
            flowPosts.Size = new Size(960, 628);
            flowPosts.TabIndex = 0;
            flowPosts.WrapContents = false;
            // 
            // txtPostContent
            // 
            txtPostContent.Location = new Point(9, 8);
            txtPostContent.Margin = new Padding(3, 2, 3, 2);
            txtPostContent.Name = "txtPostContent";
            txtPostContent.Size = new Size(623, 23);
            txtPostContent.TabIndex = 1;
            // 
            // btnPost
            // 
            btnPost.Location = new Point(671, 8);
            btnPost.Margin = new Padding(3, 2, 3, 2);
            btnPost.Name = "btnPost";
            btnPost.Size = new Size(70, 23);
            btnPost.TabIndex = 2;
            btnPost.Text = "Đăng bài";
            btnPost.Click += btnPost_Click;

            // btnChooseImage
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnChooseImage.Location = new System.Drawing.Point(10, 40);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(100, 27);
            this.btnChooseImage.Text = "Chọn ảnh";
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);

            // btnChooseVideo
            this.btnChooseVideo = new System.Windows.Forms.Button();
            this.btnChooseVideo.Location = new System.Drawing.Point(120, 40);
            this.btnChooseVideo.Name = "btnChooseVideo";
            this.btnChooseVideo.Size = new System.Drawing.Size(100, 27);
            this.btnChooseVideo.Text = "Chọn video";
            this.btnChooseVideo.Click += new System.EventHandler(this.btnChooseVideo_Click);

            // txtImagePath (ẩn)
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.txtImagePath.Location = new System.Drawing.Point(230, 40);
            this.txtImagePath.Size = new System.Drawing.Size(250, 27);
            this.txtImagePath.Visible = false;

            // txtVideoPath (ẩn)
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.txtVideoPath.Location = new System.Drawing.Point(490, 40);
            this.txtVideoPath.Size = new System.Drawing.Size(250, 27);
            this.txtVideoPath.Visible = false;

            // Thêm vào Controls
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.btnChooseVideo);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.txtVideoPath);
            // 
            // TrangChuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(flowPosts);
            Controls.Add(txtPostContent);
            Controls.Add(btnPost);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TrangChuControl";
            Size = new Size(977, 688);
            Load += TrangChuControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
