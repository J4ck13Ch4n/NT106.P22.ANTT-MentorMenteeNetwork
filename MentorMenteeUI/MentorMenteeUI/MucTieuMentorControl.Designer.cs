namespace MentorMenteeUI
{
    partial class MucTieuMentorControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            STT = new DataGridViewTextBoxColumn();
            MucTieu = new DataGridViewTextBoxColumn();
            ThoiGian = new DataGridViewTextBoxColumn();
            MoTa = new DataGridViewTextBoxColumn();
            TinhTrang = new DataGridViewTextBoxColumn();
            DanhGia = new DataGridViewTextBoxColumn();
            btEditGoal = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { STT, MucTieu, ThoiGian, MoTa, TinhTrang, DanhGia });
            dataGridView1.GridColor = Color.FromArgb(0, 132, 168);
            dataGridView1.Location = new Point(18, 77);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 132, 168);
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1079, 819);
            dataGridView1.TabIndex = 1;
            // 
            // STT
            // 
            STT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            STT.FillWeight = 35F;
            STT.HeaderText = "STT";
            STT.MinimumWidth = 6;
            STT.Name = "STT";
            // 
            // MucTieu
            // 
            MucTieu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MucTieu.FillWeight = 150F;
            MucTieu.HeaderText = "Mục Tiêu";
            MucTieu.MinimumWidth = 6;
            MucTieu.Name = "MucTieu";
            // 
            // ThoiGian
            // 
            ThoiGian.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ThoiGian.FillWeight = 80F;
            ThoiGian.HeaderText = "Thời Gian";
            ThoiGian.MinimumWidth = 6;
            ThoiGian.Name = "ThoiGian";
            // 
            // MoTa
            // 
            MoTa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            MoTa.FillWeight = 180F;
            MoTa.HeaderText = "Mô Tả";
            MoTa.MinimumWidth = 6;
            MoTa.Name = "MoTa";
            // 
            // TinhTrang
            // 
            TinhTrang.FillWeight = 150F;
            TinhTrang.HeaderText = "Tình Trạng";
            TinhTrang.MinimumWidth = 6;
            TinhTrang.Name = "TinhTrang";
            TinhTrang.Width = 150;
            // 
            // DanhGia
            // 
            DanhGia.HeaderText = "Đánh Giá";
            DanhGia.MinimumWidth = 6;
            DanhGia.Name = "DanhGia";
            DanhGia.Width = 125;
            // 
            // btEditGoal
            // 
            btEditGoal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btEditGoal.Location = new Point(783, 16);
            btEditGoal.Name = "btEditGoal";
            btEditGoal.Size = new Size(314, 45);
            btEditGoal.TabIndex = 2;
            btEditGoal.Text = "Đánh Giá/Chỉnh sửa Mục Tiêu";
            btEditGoal.UseVisualStyleBackColor = true;
            // 
            // MucTieuMentorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(btEditGoal);
            Controls.Add(dataGridView1);
            Name = "MucTieuMentorControl";
            Size = new Size(1117, 917);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn MucTieu;
        private DataGridViewTextBoxColumn ThoiGian;
        private DataGridViewTextBoxColumn MoTa;
        private DataGridViewTextBoxColumn TinhTrang;
        private DataGridViewTextBoxColumn DanhGia;
        private Button btEditGoal;
    }
}
