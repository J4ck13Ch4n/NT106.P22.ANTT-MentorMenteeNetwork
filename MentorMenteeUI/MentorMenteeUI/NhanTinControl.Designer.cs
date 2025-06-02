namespace MentorMenteeUI
{
    partial class NhanTinControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbDSTroChuyen = new ListBox();
            rtbKhungTroChuyen = new RichTextBox();
            tbTinNhan = new TextBox();
            bGui = new Button();
            tbNguoiNhan = new TextBox();
            lbUserSuggestions = new ListBox();
            SuspendLayout();
            // 
            // lbDSTroChuyen
            // 
            lbDSTroChuyen.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbDSTroChuyen.FormattingEnabled = true;
            lbDSTroChuyen.Items.AddRange(new object[] { "Trống" });
            lbDSTroChuyen.Location = new Point(23, 188);
            lbDSTroChuyen.Margin = new Padding(3, 4, 3, 4);
            lbDSTroChuyen.Name = "lbDSTroChuyen";
            lbDSTroChuyen.Size = new Size(230, 752);
            lbDSTroChuyen.TabIndex = 0;
            // 
            // rtbKhungTroChuyen
            // 
            rtbKhungTroChuyen.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbKhungTroChuyen.Location = new Point(259, 19);
            rtbKhungTroChuyen.Margin = new Padding(3, 4, 3, 4);
            rtbKhungTroChuyen.Name = "rtbKhungTroChuyen";
            rtbKhungTroChuyen.ReadOnly = true;
            rtbKhungTroChuyen.Size = new Size(835, 833);
            rtbKhungTroChuyen.TabIndex = 1;
            rtbKhungTroChuyen.Text = "";
            // 
            // tbTinNhan
            // 
            tbTinNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTinNhan.Location = new Point(259, 860);
            tbTinNhan.Margin = new Padding(3, 4, 3, 4);
            tbTinNhan.Name = "tbTinNhan";
            tbTinNhan.Size = new Size(684, 47);
            tbTinNhan.TabIndex = 2;
            // 
            // bGui
            // 
            bGui.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bGui.Location = new Point(949, 860);
            bGui.Margin = new Padding(3, 4, 3, 4);
            bGui.Name = "bGui";
            bGui.Size = new Size(145, 47);
            bGui.TabIndex = 3;
            bGui.Text = "Gửi";
            bGui.UseVisualStyleBackColor = true;
            bGui.Click += bGui_Click;
            // 
            // tbNguoiNhan
            // 
            tbNguoiNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbNguoiNhan.Location = new Point(23, 19);
            tbNguoiNhan.Margin = new Padding(3, 4, 3, 4);
            tbNguoiNhan.Name = "tbNguoiNhan";
            tbNguoiNhan.Size = new Size(230, 47);
            tbNguoiNhan.TabIndex = 1;
            // 
            // lbUserSuggestions
            // 
            lbUserSuggestions.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbUserSuggestions.Location = new Point(23, 74);
            lbUserSuggestions.Margin = new Padding(3, 4, 3, 4);
            lbUserSuggestions.Name = "lbUserSuggestions";
            lbUserSuggestions.Size = new Size(230, 106);
            lbUserSuggestions.TabIndex = 4;
            // 
            // NhanTinControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(lbUserSuggestions);
            Controls.Add(tbNguoiNhan);
            Controls.Add(bGui);
            Controls.Add(tbTinNhan);
            Controls.Add(rtbKhungTroChuyen);
            Controls.Add(lbDSTroChuyen);
            Margin = new Padding(3, 4, 3, 4);
            Name = "NhanTinControl";
            Size = new Size(1117, 917);
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private ListBox lbDSTroChuyen;
        private RichTextBox rtbKhungTroChuyen;
        private TextBox tbTinNhan;
        private Button bGui;
        private TextBox tbNguoiNhan;
        private ListBox lbUserSuggestions;
    }
}
