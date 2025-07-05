using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MentorMenteeUI
{
    public class UserSimpleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AvatarPath { get; set; }
        public string Role { get; set; }
    }
    public class LikeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
    }
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSimpleDto User { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<LikeDto> Likes { get; set; }
    }
    public class CreatePostDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Visibility { get; set; }
        // Không cần Image/Video ở đây, vì gửi multipart form-data riêng
    }
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public string CommentText { get; set; }
    }

    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }

    public class UserDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        [JsonPropertyName("avatarPath")]
        public string AvatarPath { get; set; }
    }
}
