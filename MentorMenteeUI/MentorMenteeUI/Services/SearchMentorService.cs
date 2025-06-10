using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MentorMenteeUI.Services
{
    class SearchMentorService
    {
        private readonly HttpClient _httpClient;

        public SearchMentorService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5268"); // Thay bằng địa chỉ server của bạn
        }

        public class MentorDto
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }

        public async Task<List<MentorDto>> SearchMentorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MentorDto>>("/api/user/search/mentor");
        }
    }
}
