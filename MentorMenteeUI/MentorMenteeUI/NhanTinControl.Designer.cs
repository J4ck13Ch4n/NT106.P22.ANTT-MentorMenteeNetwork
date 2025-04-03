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
            lbDSTroChuyen.ItemHeight = 39;
            lbDSTroChuyen.Items.AddRange(new object[] { "Ten 1", "Ten 2", "Ten 3", "Ten 4", "Ten 5", "Ten 6", "Ten 1", "Ten 2", "Ten 3", "Ten 4", "Ten 5", "Ten 6", "Ten 1", "Ten 2", "Ten 3", "Ten 4", "Ten 5", "Ten 6" });
            lbDSTroChuyen.Location = new Point(18, 14);
            lbDSTroChuyen.Name = "lbDSTroChuyen";
            lbDSTroChuyen.Size = new Size(203, 628);
            lbDSTroChuyen.TabIndex = 0;
            // 
            // rtbKhungTroChuyen
            // 
            rtbKhungTroChuyen.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbKhungTroChuyen.Location = new Point(228, 14);
            rtbKhungTroChuyen.Name = "rtbKhungTroChuyen";
            rtbKhungTroChuyen.ReadOnly = true;
            rtbKhungTroChuyen.Size = new Size(651, 601);
            rtbKhungTroChuyen.TabIndex = 1;
            rtbKhungTroChuyen.Text = "";
            // 
            // tbTinNhan
            // 
            tbTinNhan.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTinNhan.Location = new Point(227, 627);
            tbTinNhan.Name = "tbTinNhan";
            tbTinNhan.Size = new Size(530, 37);
            tbTinNhan.TabIndex = 2;
            // 
            // bGui
            // 
            bGui.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bGui.Location = new Point(763, 627);
            bGui.Name = "bGui";
            bGui.Size = new Size(116, 39);
            bGui.TabIndex = 3;
            bGui.Text = "Gửi";
            bGui.UseVisualStyleBackColor = true;
            bGui.Click += bGui_Click;
            // 
            // NhanTinControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(bGui);
            Controls.Add(tbTinNhan);
            Controls.Add(rtbKhungTroChuyen);
            Controls.Add(lbDSTroChuyen);
            Name = "NhanTinControl";
            Size = new Size(900, 688);
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
