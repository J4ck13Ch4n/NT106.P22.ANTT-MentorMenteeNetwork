using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MentorMenteeUI.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl;
        public AuthService(string serverUrl)
        {
            _serverUrl = serverUrl;
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(string email, string password, string username, string gender, string role, string avatarPath)
        {
            try
            {
                string avatarBase64 = null;

                if (!string.IsNullOrEmpty(avatarPath))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(avatarPath);
                    avatarBase64 = Convert.ToBase64String(imageBytes); // Chuyển ảnh thành Base64
                }

                // Gửi yêu cầu POST đến API
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", new
                {
                    Email = email,
                    Password = password,
                    Username = username,
                    Gender = gender,
                    Role = role,
                    Avatar = avatarBase64 // Gửi ảnh dưới dạng Base64
                });

                // Đọc nội dung phản hồi từ server
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Server response: {responseContent}");

                // Kiểm tra trạng thái HTTP
                if (response.IsSuccessStatusCode)
                {
                    return (true, null); // Thành công
                }
                else
                {
                    return (false, responseContent); // Trả về lỗi từ server
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                return (false, ex.Message); // Trả về lỗi ngoại lệ
            }
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
                {
                    Email = email,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                    // Xóa dòng này vì không cần thiết và gây lỗi:
                    // if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token))
                    // {
                    //     Token = loginResult.Token;
                    // }
                    // Trả về loginResult, token sẽ lấy từ loginResult.Token ở nơi gọi
                    return loginResult;
                }
                else
                {
                    return new LoginResult { Success = false };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return new LoginResult { Success = false };
            }
        }


        public class LoginResult
        {
            public bool Success { get; set; }
            public string UserId { get; set; }
            public string FullName { get; set; }
            public string Token { get; set; } // Thêm dòng này
            public string Role { get; set; } // Thêm dòng này cho Goal
        }


        public async Task<bool> CheckServerConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/auth/ping");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Kết nối đến server thành công!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Kết nối đến server thất bại! Mã lỗi: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kết nối đến server: {ex.Message}");
                return false;
            }
        }
    }
}