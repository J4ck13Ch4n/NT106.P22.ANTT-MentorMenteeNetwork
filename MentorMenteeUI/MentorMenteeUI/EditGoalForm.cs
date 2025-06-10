using MentorMenteeUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMenteeUI
{
    public partial class EditGoalForm : Form
    {
        private readonly GoalDto _goal;
        private readonly GoalService _goalService;
        public EditGoalForm(GoalDto goal)
        {
            InitializeComponent();
            this._goal = goal;
            _goalService = new GoalService();
        }

        private async void btAddGoal_Click(object sender, EventArgs e)
        {
            string feadback = cbFeedback.SelectedIndex > 0 ? cbFeedback.SelectedItem.ToString() : string.Empty;
            try
            {
                var updateGoal = new UpdateGoalRequest
                {
                    Status = tbStatus.Text,
                    Feedback = feadback + ". " + rtbDetailedFeedback.Text
                };

                var result = await _goalService.UpdateGoalAsync(_goal.Id, updateGoal);
                if (result.Success)
                {
                    MessageBox.Show("Cập nhật mục tiêu thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Cập nhật mục tiêu thất bại: {result.Error}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật mục tiêu: {ex.Message}");
                return;
            }
        }
    }
}
