using System.Text;
using System.Text.Json;

namespace MentorMenteeUI.Services
{
    public class PostAPI
    {
        private readonly HttpClient _client;
        public PostAPI(string baseUrl)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<PostDto>> GetPostsAsync()
        {
            var res = await _client.GetAsync("api/posts");
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PostDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<PostDto> CreatePostAsync(CreatePostDto dto, string imagePath = null, string videoPath = null)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.UserId.ToString()), "UserId");
            form.Add(new StringContent(dto.Content ?? ""), "Content");
            form.Add(new StringContent(dto.Visibility ?? "public"), "Visibility");
            if (!string.IsNullOrEmpty(imagePath))
                form.Add(new StreamContent(File.OpenRead(imagePath)), "image", Path.GetFileName(imagePath));
            if (!string.IsNullOrEmpty(videoPath))
                form.Add(new StreamContent(File.OpenRead(videoPath)), "video", Path.GetFileName(videoPath));
            var res = await _client.PostAsync("api/posts", form);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PostDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task LikePostAsync(int postId, int userId)
        {
            var content = new StringContent(userId.ToString(), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync($"api/posts/{postId}/like", content);
            res.EnsureSuccessStatusCode();
        }

        public async Task UnlikePostAsync(int postId, int userId)
        {
            var content = new StringContent(userId.ToString(), Encoding.UTF8, "application/json");
            var req = new HttpRequestMessage(HttpMethod.Delete, $"api/posts/{postId}/like")
            {
                Content = content
            };
            var res = await _client.SendAsync(req);
            res.EnsureSuccessStatusCode();
        }

        public async Task<CommentDto> AddCommentAsync(int postId, CreateCommentDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync($"api/posts/{postId}/comments", content);
            res.EnsureSuccessStatusCode();
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CommentDto>(resJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task DeletePostAsync(int postId)
        {
            var res = await _client.DeleteAsync($"api/posts/{postId}");
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var res = await _client.DeleteAsync($"api/comments/{commentId}");
            res.EnsureSuccessStatusCode();
        }
    }
}
