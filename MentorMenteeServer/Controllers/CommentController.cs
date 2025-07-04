using MentorMenteeServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    // DELETE: /api/comments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
            return NotFound();

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}