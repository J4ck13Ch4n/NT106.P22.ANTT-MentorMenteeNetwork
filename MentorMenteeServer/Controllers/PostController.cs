using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Likes).ThenInclude(l => l.User)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        var postDtos = posts.Select(p => new PostDto
        {
            Id = p.Id,
            UserId = p.UserId,
            Content = p.Content,
            Image = p.Image,
            Video = p.Video,
            Visibility = p.Visibility,
            CreatedAt = p.CreatedAt,
            User = new UserSimpleDto
            {
                Id = p.User.Id,
                Username = p.User.Username,
                AvatarPath = p.User.AvatarPath
            },
            Comments = p.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                CommentText = c.CommentText,
                CreatedAt = c.CreatedAt,
                User = new UserSimpleDto
                {
                    Id = c.User.Id,
                    Username = c.User.Username,
                    AvatarPath = c.User.AvatarPath
                }
            }).ToList(),
            Likes = p.Likes.Select(l => new LikeDto
            {
                Id = l.Id,
                UserId = l.UserId,
                CreatedAt = l.CreatedAt,
                User = new UserSimpleDto
                {
                    Id = l.User.Id,
                    Username = l.User.Username,
                    AvatarPath = l.User.AvatarPath
                }
            }).ToList()
        }).ToList();

        return Ok(postDtos);
    }

    // POST: /api/posts
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto dto)
    {
        var post = new Post
        {
            UserId = dto.UserId,
            Content = dto.Content,
            Image = dto.Image,
            Video = dto.Video,
            Visibility = dto.Visibility,
            CreatedAt = DateTime.UtcNow
        };
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        // Lấy lại post với User để trả về DTO
        post = await _context.Posts.Include(p => p.User).FirstAsync(p => p.Id == post.Id);

        var postDto = new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            Image = post.Image,
            Video = post.Video,
            Visibility = post.Visibility,
            CreatedAt = post.CreatedAt,
            User = new UserSimpleDto
            {
                Id = post.User.Id,
                Username = post.User.Username,
                AvatarPath = post.User.AvatarPath
            },
            Comments = new List<CommentDto>(),
            Likes = new List<LikeDto>()
        };

        return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, postDto);
    }

    // DELETE: /api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        _context.Comments.RemoveRange(post.Comments);
        _context.Likes.RemoveRange(post.Likes);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: /api/posts/{id}/like
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikePost(int id, [FromBody] int userId)
    {
        if (!_context.Posts.Any(p => p.Id == id))
            return NotFound();

        if (_context.Likes.Any(l => l.PostId == id && l.UserId == userId))
            return BadRequest("Already liked");

        var like = new Like { PostId = id, UserId = userId, CreatedAt = DateTime.UtcNow };
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // DELETE: /api/posts/{id}/like
    [HttpDelete("{id}/like")]
    public async Task<IActionResult> UnlikePost(int id, [FromBody] int userId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == id && l.UserId == userId);
        if (like == null)
            return NotFound();

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: /api/posts/{id}/comments
    [HttpPost("{id}/comments")]
    public async Task<ActionResult<CommentDto>> AddComment(int id, [FromBody] CreateCommentDto dto)
    {
        if (!_context.Posts.Any(p => p.Id == id))
            return NotFound();

        var comment = new Comment
        {
            PostId = id,
            UserId = dto.UserId,
            CommentText = dto.CommentText,
            CreatedAt = DateTime.UtcNow
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(dto.UserId);

        var commentDto = new CommentDto
        {
            Id = comment.Id,
            UserId = comment.UserId,
            CommentText = comment.CommentText,
            CreatedAt = comment.CreatedAt,
            User = new UserSimpleDto
            {
                Id = user.Id,
                Username = user.Username,
                AvatarPath = user.AvatarPath
            }
        };

        return Ok(commentDto);
    }
}