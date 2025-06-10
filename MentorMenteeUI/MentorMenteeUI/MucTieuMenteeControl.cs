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
    public partial class MucTieuMenteeControl : UserControl
    {

        private readonly GoalService _goalService;
        private readonly int _menteeId;
        private readonly int _mentorId;

        public MucTieuMenteeControl(int menteeId)
        {
            InitializeComponent();
            _goalService = new GoalService();
            _menteeId = menteeId;
        }

        private async void MucTieuMenteeControl_Load(object sender, EventArgs e)
        {
            dgvGoal.Rows.Clear();
            var goals = await _goalService.GetGoalsForMenteeAsync(_menteeId);
            if (goals.Error == null)
            {
                int i = 0;
                foreach (var goal in goals.Result)
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

        private async void btAddGoal_Click(object sender, EventArgs e)
        {
            AddGoalForm addGoalForm = new AddGoalForm(_menteeId);
            addGoalForm.ShowDialog();
            if (addGoalForm.DialogResult == DialogResult.OK)
            {
                MucTieuMenteeControl_Load(sender, e);
            }
        }
    }
}
