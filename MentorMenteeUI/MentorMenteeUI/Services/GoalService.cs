using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MentorMenteeUI.Services
{
    public class GoalService
    {
        private readonly HttpClient _httpClient;

        public GoalService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5268"); // Thay bằng địa chỉ server của bạn
        }

        public async Task<(List<GoalDto> Result, string Error)> GetGoalsForMenteeAsync(int menteeId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/goals/mentee/{menteeId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<GoalDto>>();
                    return (result ?? new List<GoalDto>(), null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (null, $"Lỗi {response.StatusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (null, $"Lỗi khi gọi API: {ex.Message}");
            }
        }

        public async Task<(List<GoalDto> Result, string Error)> GetGoalsForMentorAsync(int mentorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/goals/mentor/{mentorId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<GoalDto>>();
                    return (result ?? new List<GoalDto>(), null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (null, $"Lỗi {response.StatusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (null, $"Lỗi khi gọi API: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Error)> AddGoalAsync(CreateGoalRequest goal)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/goals", goal);
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Lỗi {response.StatusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi gọi API: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Error)> UpdateGoalAsync(int goalId, UpdateGoalRequest update)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/goals/{goalId}", update);
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Lỗi {response.StatusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi gọi API: {ex.Message}");
            }
        }
    }

    // DTOs
    public class GoalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Feedback { get; set; }
        public int MenteeId { get; set; }
        public int MentorId { get; set; }
    }

    public class CreateGoalRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Feedback { get; set; }
        public int MenteeId { get; set; }
        public int MentorId { get; set; }
    }

    public class UpdateGoalRequest
    {
        public string Status { get; set; }
        public string Feedback { get; set; }
    }
}
