namespace MentorMenteeUI
{
    partial class MucTieuMenteeControl
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgvGoal = new DataGridView();
            btAddGoal = new Button();
            STT = new DataGridViewTextBoxColumn();
            MucTieu = new DataGridViewTextBoxColumn();
            ThoiGian = new DataGridViewTextBoxColumn();
            MoTa = new DataGridViewTextBoxColumn();
            TinhTrang = new DataGridViewTextBoxColumn();
            DanhGia = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvGoal).BeginInit();
            SuspendLayout();
            // 
            // dgvGoal
            // 
            dgvGoal.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvGoal.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(0, 132, 168);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvGoal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvGoal.ColumnHeadersHeight = 50;
            dgvGoal.Columns.AddRange(new DataGridViewColumn[] { STT, MucTieu, ThoiGian, MoTa, TinhTrang, DanhGia });
            dgvGoal.GridColor = Color.FromArgb(0, 132, 168);
            dgvGoal.Location = new Point(20, 78);
            dgvGoal.MultiSelect = false;
            dgvGoal.Name = "dgvGoal";
            dgvGoal.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(0, 132, 168);
            dgvGoal.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvGoal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoal.Size = new Size(1079, 819);
            dgvGoal.TabIndex = 0;
            // 
            // btAddGoal
            // 
            btAddGoal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAddGoal.Location = new Point(930, 15);
            btAddGoal.Name = "btAddGoal";
            btAddGoal.Size = new Size(169, 45);
            btAddGoal.TabIndex = 1;
            btAddGoal.Text = "Thêm Mục Tiêu";
            btAddGoal.UseVisualStyleBackColor = true;
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
            // MucTieuMenteeControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 132, 168);
            Controls.Add(btAddGoal);
            Controls.Add(dgvGoal);
            Name = "MucTieuMenteeControl";
            Size = new Size(1117, 917);
            ((System.ComponentModel.ISupportInitialize)dgvGoal).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvGoal;
        private Button btAddGoal;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn MucTieu;
        private DataGridViewTextBoxColumn ThoiGian;
        private DataGridViewTextBoxColumn MoTa;
        private DataGridViewTextBoxColumn TinhTrang;
        private DataGridViewTextBoxColumn DanhGia;
    }
}
