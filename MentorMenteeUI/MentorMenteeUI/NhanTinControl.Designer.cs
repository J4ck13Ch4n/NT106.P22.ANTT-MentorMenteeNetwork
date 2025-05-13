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
            SuspendLayout();
            // 
            // lbDSTroChuyen
            // 
            lbDSTroChuyen.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbDSTroChuyen.FormattingEnabled = true;
            lbDSTroChuyen.Items.AddRange(new object[] { "Tên 1", "Tên 2", "Tên 3" });
            lbDSTroChuyen.Location = new Point(21, 19);
            lbDSTroChuyen.Margin = new Padding(3, 4, 3, 4);
            lbDSTroChuyen.Name = "lbDSTroChuyen";
            lbDSTroChuyen.Size = new Size(231, 752);
            lbDSTroChuyen.TabIndex = 0;
            // 
            // rtbKhungTroChuyen
            // 
            rtbKhungTroChuyen.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbKhungTroChuyen.Location = new Point(261, 19);
            rtbKhungTroChuyen.Margin = new Padding(3, 4, 3, 4);
            rtbKhungTroChuyen.Name = "rtbKhungTroChuyen";
            rtbKhungTroChuyen.ReadOnly = true;
            rtbKhungTroChuyen.Size = new Size(743, 800);
            rtbKhungTroChuyen.TabIndex = 1;
            rtbKhungTroChuyen.Text = "";
            rtbKhungTroChuyen.TextChanged += rtbKhungTroChuyen_TextChanged;
            // 
            // tbTinNhan
            // 
            tbTinNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTinNhan.Location = new Point(259, 836);
            tbTinNhan.Margin = new Padding(3, 4, 3, 4);
            tbTinNhan.Name = "tbTinNhan";
            tbTinNhan.Size = new Size(605, 47);
            tbTinNhan.TabIndex = 2;
            // 
            // bGui
            // 
            bGui.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bGui.Location = new Point(872, 836);
            bGui.Margin = new Padding(3, 4, 3, 4);
            bGui.Name = "bGui";
            bGui.Size = new Size(133, 52);
            bGui.TabIndex = 3;
            bGui.Text = "Gửi";
            bGui.UseVisualStyleBackColor = true;
            bGui.Click += bGui_Click;
            // 
            // NhanTinControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(bGui);
            Controls.Add(tbTinNhan);
            Controls.Add(rtbKhungTroChuyen);
            Controls.Add(lbDSTroChuyen);
            Margin = new Padding(3, 4, 3, 4);
            Name = "NhanTinControl";
            Size = new Size(1029, 917);
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private ListBox lbDSTroChuyen;
        private RichTextBox rtbKhungTroChuyen;
        private TextBox tbTinNhan;
        private Button bGui;
    }
}
