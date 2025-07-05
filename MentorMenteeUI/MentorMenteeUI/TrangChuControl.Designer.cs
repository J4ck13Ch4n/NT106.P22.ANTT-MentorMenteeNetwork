using System.Drawing.Drawing2D;
using AxWMPLib;

namespace MentorMenteeUI
{
    partial class TrangChuControl
    {

        private System.ComponentModel.IContainer components = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrangChuControl));
            avatar = new PictureBox();
            content = new TextBox();
            btFile = new Button();
            previewPic = new PictureBox();
            btPost = new Button();
            postList = new FlowLayoutPanel();
            axWindowsMediaPlayer1 = new AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // avatar
            // 
            avatar.Location = new Point(52, 12);
            avatar.Name = "avatar";
            avatar.Size = new Size(46, 43);
            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar.TabIndex = 0;
            avatar.TabStop = false;
            // 
            // content
            // 
            content.ForeColor = Color.Gray;
            content.Location = new Point(122, 12);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(491, 94);
            content.TabIndex = 1;
            content.Text = "Bạn đang nghĩ gì?";
            // 
            // btFile
            // 
            btFile.Location = new Point(122, 112);
            btFile.Name = "btFile";
            btFile.Size = new Size(77, 29);
            btFile.TabIndex = 2;
            btFile.Text = "Ảnh/Video";
            btFile.UseVisualStyleBackColor = true;
            // 
            // previewPic
            // 
            previewPic.Location = new Point(649, 12);
            previewPic.Name = "previewPic";
            previewPic.Size = new Size(212, 123);
            previewPic.SizeMode = PictureBoxSizeMode.Zoom;
            previewPic.TabIndex = 3;
            previewPic.TabStop = false;
            // 
            // btPost
            // 
            btPost.Location = new Point(219, 112);
            btPost.Name = "btPost";
            btPost.Size = new Size(83, 29);
            btPost.TabIndex = 4;
            btPost.Text = "Đăng bài";
            btPost.UseVisualStyleBackColor = true;
            // 
            // postList
            // 
            postList.AutoScroll = true;
            postList.FlowDirection = FlowDirection.TopDown;
            postList.Location = new Point(20, 147);
            postList.Name = "postList";
            postList.Size = new Size(941, 530);
            postList.TabIndex = 5;
            postList.WrapContents = false;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(649, 12);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(212, 123);
            axWindowsMediaPlayer1.TabIndex = 6;
            // 
            // TrangChuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(axWindowsMediaPlayer1);
            Controls.Add(postList);
            Controls.Add(btPost);
            Controls.Add(previewPic);
            Controls.Add(btFile);
            Controls.Add(content);
            Controls.Add(avatar);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TrangChuControl";
            Size = new Size(977, 688);
            ((System.ComponentModel.ISupportInitialize)avatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox avatar;
        private TextBox content;
        private Button btFile;
        private PictureBox previewPic;
        private Button btPost;
        private FlowLayoutPanel postList;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}