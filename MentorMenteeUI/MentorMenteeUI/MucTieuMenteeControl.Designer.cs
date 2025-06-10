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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvGoal = new DataGridView();
            STT = new DataGridViewTextBoxColumn();
            MucTieu = new DataGridViewTextBoxColumn();
            ThoiHan = new DataGridViewTextBoxColumn();
            MoTa = new DataGridViewTextBoxColumn();
            TinhTrang = new DataGridViewTextBoxColumn();
            DanhGia = new DataGridViewTextBoxColumn();
            btAddGoal = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGoal).BeginInit();
            SuspendLayout();
            // 
            // dgvGoal
            // 
            dgvGoal.AllowUserToAddRows = false;
            dgvGoal.AllowUserToDeleteRows = false;
            dgvGoal.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvGoal.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGoal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGoal.ColumnHeadersHeight = 50;
            dgvGoal.Columns.AddRange(new DataGridViewColumn[] { STT, MucTieu, ThoiHan, MoTa, TinhTrang, DanhGia });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvGoal.DefaultCellStyle = dataGridViewCellStyle2;
            dgvGoal.GridColor = Color.FromArgb(0, 132, 168);
            dgvGoal.Location = new Point(20, 78);
            dgvGoal.MultiSelect = false;
            dgvGoal.Name = "dgvGoal";
            dgvGoal.ReadOnly = true;
            dgvGoal.RowHeadersVisible = false;
            dgvGoal.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dgvGoal.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvGoal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoal.Size = new Size(1080, 819);
            dgvGoal.TabIndex = 0;
            // 
            // STT
            // 
            STT.FillWeight = 35F;
            STT.HeaderText = "STT";
            STT.MinimumWidth = 6;
            STT.Name = "STT";
            STT.ReadOnly = true;
            STT.Width = 60;
            // 
            // MucTieu
            // 
            MucTieu.FillWeight = 150F;
            MucTieu.HeaderText = "Mục Tiêu";
            MucTieu.MinimumWidth = 6;
            MucTieu.Name = "MucTieu";
            MucTieu.ReadOnly = true;
            MucTieu.Width = 240;
            // 
            // ThoiHan
            // 
            ThoiHan.FillWeight = 80F;
            ThoiHan.HeaderText = "Thời Hạn";
            ThoiHan.MinimumWidth = 6;
            ThoiHan.Name = "ThoiHan";
            ThoiHan.ReadOnly = true;
            ThoiHan.Width = 150;
            // 
            // MoTa
            // 
            MoTa.FillWeight = 180F;
            MoTa.HeaderText = "Mô Tả";
            MoTa.MinimumWidth = 6;
            MoTa.Name = "MoTa";
            MoTa.ReadOnly = true;
            MoTa.Width = 350;
            // 
            // TinhTrang
            // 
            TinhTrang.FillWeight = 150F;
            TinhTrang.HeaderText = "Tình Trạng";
            TinhTrang.MinimumWidth = 6;
            TinhTrang.Name = "TinhTrang";
            TinhTrang.ReadOnly = true;
            TinhTrang.Width = 140;
            // 
            // DanhGia
            // 
            DanhGia.HeaderText = "Đánh Giá";
            DanhGia.MinimumWidth = 6;
            DanhGia.Name = "DanhGia";
            DanhGia.ReadOnly = true;
            DanhGia.Width = 140;
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
            btAddGoal.Click += btAddGoal_Click;
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
            Load += MucTieuMenteeControl_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGoal).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvGoal;
        private Button btAddGoal;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn MucTieu;
        private DataGridViewTextBoxColumn ThoiHan;
        private DataGridViewTextBoxColumn MoTa;
        private DataGridViewTextBoxColumn TinhTrang;
        private DataGridViewTextBoxColumn DanhGia;
    }
}
