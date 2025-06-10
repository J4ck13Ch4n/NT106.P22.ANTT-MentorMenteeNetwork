using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MentorMenteeUI.Services;

namespace MentorMenteeUI
{
    public partial class MucTieuMentorControl : UserControl
    {
        private readonly GoalService _goalService;
        private readonly int _mentorId;
        private int _menteeId;
        private List<GoalDto> _goals;
        public MucTieuMentorControl(int mentorId)
        {
            InitializeComponent();
            this._mentorId = mentorId;
            _goalService = new GoalService();

        }

        private async void MucTieuMentorControl_Load(object sender, EventArgs e)
        {
            dgvGoal.Rows.Clear();
            var goals = await _goalService.GetGoalsForMentorAsync(_mentorId);
            _goals = goals.Result;
            if (goals.Error == null)
            {
                int i = 0;
                foreach (var goal in _goals)
                {
                    dgvGoal.Rows.Add(
                        ++i,
                        goal.Title,
                        goal.Deadline.ToString("dd/MM/yyyy"),
                        goal.Description,
                        goal.Status,
                        goal.Feedback
                    );
                }
            }
        }

        private void btEditGoal_Click(object sender, EventArgs e)
        {
            var selectedGoal = dgvGoal.SelectedRows;
            if (selectedGoal.Count > 0)
            {
                int selectedIndex = selectedGoal[0].Index;
                var goal = _goals[selectedIndex];
                if (goal != null)
                {
                    EditGoalForm editGoalForm = new EditGoalForm(goal);
                    editGoalForm.ShowDialog();
                    if (editGoalForm.DialogResult == DialogResult.OK)
                    {
                        MucTieuMentorControl_Load(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mục tiêu để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
