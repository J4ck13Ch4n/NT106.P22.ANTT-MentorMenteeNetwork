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
    public partial class AddGoalForm : Form
    {
        private readonly int _menteeId;
        private int _mentorId;
        private readonly GoalService _goalService;
        //private readonly List<MentorDto> _mentors;
        private readonly SearchMentorService _searchMentorService;

        public AddGoalForm(int menteeId)
        {
            InitializeComponent();
            this._menteeId = menteeId;
            _goalService = new GoalService();
            _searchMentorService = new SearchMentorService();

        }
        public class MentorDto
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }

        private async void btAddGoal_Click(object sender, EventArgs e)
        {
            _mentorId = cbMentor.SelectedIndex > 0 ? int.Parse(cbMentor.SelectedItem.ToString().Split(',')[0].Replace("ID: ", "")) : 0;

            try
            {
                var goal = new CreateGoalRequest
                {
                    Title = tbTitle.Text,
                    Description = rtbDescription.Text,
                    Deadline = dtpDeadline.Value,
                    Status = "Chưa hoàn thành",
                    Feedback = string.Empty,
                    MenteeId = _menteeId,
                    MentorId = _mentorId
                };

                var (success, error) = await _goalService.AddGoalAsync(goal);
                if (success)
                {
                    MessageBox.Show("Thêm mục tiêu thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Lỗi khi thêm mục tiêu: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private async void btReloadMentorList_Click(object sender, EventArgs e)
        {
            try
            {
                var mentors = await _searchMentorService.SearchMentorsAsync();
                cbMentor.Items.Clear();

                if (mentors != null && mentors.Count > 0)
                {
                    foreach (var mentor in mentors)
                    {
                        cbMentor.Items.Add($"ID: {mentor.Id}, Username: {mentor.Username}");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mentor nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mentor: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
    }
}
