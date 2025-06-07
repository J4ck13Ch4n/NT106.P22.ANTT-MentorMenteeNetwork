using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using MentorMenteeUI;

namespace MentorMenteeUI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5268") };
        public async Task<List<UserDto>> SearchUsersAsync(string keyword)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var response = await _httpClient.GetFromJsonAsync<List<UserDto>>($"https://localhost:5268/api/user/search?query={keyword}");
            return response ?? new List<UserDto>();
        }
    }

    public class PendingRequestDto
    {
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }

    public class FriendService
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5268") };
        public async Task<string> GetRelationshipStatus(int userId, int friendId)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var response = await _httpClient.GetAsync($"https://localhost:5268/api/friendship/status?userId={userId}&friendId={friendId}");
            if (!response.IsSuccessStatusCode) return "none";
            return await response.Content.ReadAsStringAsync();
        }
        public async Task SendFriendRequest(int userId, int friendId)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var data = new { SenderId = userId, ReceiverId = friendId, SenderUsername = "", ReceiverUsername = "" };
            await _httpClient.PostAsJsonAsync("https://localhost:5268/api/friendship/send-request", data);
        }
        public async Task<List<PendingRequestDto>> GetPendingRequests(int userId)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var response = await _httpClient.GetFromJsonAsync<List<PendingRequestDto>>($"https://localhost:5268/api/friendship/pending-request/{userId}");
            return response ?? new List<PendingRequestDto>();
        }

        public async Task<bool> AcceptFriendRequest(int senderId, int receiverId)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var data = new { SenderId = senderId, ReceiverId = receiverId, SenderUsername = "", ReceiverUsername = "" };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5268/api/friendship/accept-request", data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RejectFriendRequest(int senderId, int receiverId)
        {
            if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
            var data = new { SenderId = senderId, ReceiverId = receiverId, SenderUsername = "", ReceiverUsername = "" };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5268/api/friendship/reject-request", data);
            return response.IsSuccessStatusCode;
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
