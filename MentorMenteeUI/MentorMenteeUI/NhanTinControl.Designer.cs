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
            SuspendLayout();
            // 
            // lbDSTroChuyen
            // 
            lbDSTroChuyen.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbDSTroChuyen.FormattingEnabled = true;
            lbDSTroChuyen.Items.AddRange(new object[] { "Trống" });
            lbDSTroChuyen.Location = new Point(20, 14);
            lbDSTroChuyen.Name = "lbDSTroChuyen";
            lbDSTroChuyen.Size = new Size(202, 652);
            lbDSTroChuyen.TabIndex = 0;
            // 
            // rtbKhungTroChuyen
            // 
            rtbKhungTroChuyen.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbKhungTroChuyen.Location = new Point(417, 14);
            rtbKhungTroChuyen.Name = "rtbKhungTroChuyen";
            rtbKhungTroChuyen.ReadOnly = true;
            rtbKhungTroChuyen.Size = new Size(540, 626);
            rtbKhungTroChuyen.TabIndex = 1;
            rtbKhungTroChuyen.Text = "";
            //rtbKhungTroChuyen.TextChanged += rtbKhungTroChuyen_TextChanged;
            // 
            // tbTinNhan
            // 
            tbTinNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTinNhan.Location = new Point(227, 645);
            tbTinNhan.Name = "tbTinNhan";
            tbTinNhan.Size = new Size(599, 39);
            tbTinNhan.TabIndex = 2;
            // 
            // bGui
            // 
            bGui.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bGui.Location = new Point(830, 645);
            bGui.Name = "bGui";
            bGui.Size = new Size(127, 35);
            bGui.TabIndex = 3;
            bGui.Text = "Gửi";
            bGui.UseVisualStyleBackColor = true;
            bGui.Click += bGui_Click;
            // 
            // tbNguoiNhan
            // 
            tbNguoiNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbNguoiNhan.Location = new Point(228, 14);
            tbNguoiNhan.Name = "tbNguoiNhan";
            tbNguoiNhan.ReadOnly = false;
            tbNguoiNhan.Size = new Size(183, 39);
            tbNguoiNhan.TabIndex = 1;
            // 
            // NhanTinControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(tbNguoiNhan);
            Controls.Add(bGui);
            Controls.Add(tbTinNhan);
            Controls.Add(rtbKhungTroChuyen);
            Controls.Add(lbDSTroChuyen);
            Name = "NhanTinControl";
            Size = new Size(977, 688);
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private ListBox lbDSTroChuyen;
        private RichTextBox rtbKhungTroChuyen;
        private TextBox tbTinNhan;
        private Button bGui;
        private TextBox tbNguoiNhan;
    }
}
