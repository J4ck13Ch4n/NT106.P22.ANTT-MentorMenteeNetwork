namespace MentorMenteeUI
{
    partial class KetBanControl
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
        private void InitializeComponent()
        {
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.btnKetBan = new System.Windows.Forms.Button();
            this.dgvLoiMoiKetBan = new System.Windows.Forms.DataGridView();
            this.btnChapNhan = new System.Windows.Forms.Button();
            this.btnTuChoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoiMoiKetBan)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(20, 20);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 23);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(230, 20);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(20, 60);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.RowTemplate.Height = 25;
            this.dgvKetQua.Size = new System.Drawing.Size(400, 200);
            this.dgvKetQua.TabIndex = 2;
            // 
            // btnKetBan
            // 
            this.btnKetBan.Location = new System.Drawing.Point(20, 270);
            this.btnKetBan.Name = "btnKetBan";
            this.btnKetBan.Size = new System.Drawing.Size(100, 30);
            this.btnKetBan.TabIndex = 3;
            this.btnKetBan.Text = "Kết bạn";
            this.btnKetBan.UseVisualStyleBackColor = true;
            this.btnKetBan.Click += new System.EventHandler(this.btnKetBan_Click);
            // 
            // dgvLoiMoiKetBan
            // 
            this.dgvLoiMoiKetBan.AllowUserToAddRows = false;
            this.dgvLoiMoiKetBan.AllowUserToDeleteRows = false;
            this.dgvLoiMoiKetBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoiMoiKetBan.Location = new System.Drawing.Point(20, 320);
            this.dgvLoiMoiKetBan.Name = "dgvLoiMoiKetBan";
            this.dgvLoiMoiKetBan.ReadOnly = true;
            this.dgvLoiMoiKetBan.RowTemplate.Height = 25;
            this.dgvLoiMoiKetBan.Size = new System.Drawing.Size(400, 150);
            this.dgvLoiMoiKetBan.TabIndex = 4;
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.Location = new System.Drawing.Point(20, 480);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(100, 30);
            this.btnChapNhan.TabIndex = 5;
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.UseVisualStyleBackColor = true;
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.Location = new System.Drawing.Point(130, 480);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(100, 30);
            this.btnTuChoi.TabIndex = 6;
            this.btnTuChoi.Text = "Từ chối";
            this.btnTuChoi.UseVisualStyleBackColor = true;
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // KetBanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTuChoi);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.dgvLoiMoiKetBan);
            this.Controls.Add(this.btnKetBan);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Name = "KetBanControl";
            this.Size = new System.Drawing.Size(450, 530);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoiMoiKetBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Button btnKetBan;
        private System.Windows.Forms.DataGridView dgvLoiMoiKetBan;
        private System.Windows.Forms.Button btnChapNhan;
        private System.Windows.Forms.Button btnTuChoi;
    }
}
