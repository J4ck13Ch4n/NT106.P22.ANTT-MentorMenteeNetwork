using Microsoft.AspNetCore.Mvc;
using MentorMenteeServer.Data;
using Microsoft.EntityFrameworkCore;

namespace MentorMenteeServer.Controllers
{
    [ApiController]
    [Route("api/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GoalsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/goals/mentee/{menteeId}
        [HttpGet("mentee/{menteeId}")]
        public async Task<IActionResult> GetGoalsByMentee(int menteeId)
        {
            var goals = await _context.Goals
                .Where(g => g.MenteeId == menteeId)
                .ToListAsync();
            return Ok(goals);
        }

        // 2. GET: api/goals/mentor/{mentorId}
        [HttpGet("mentor/{mentorId}")]
        public async Task<IActionResult> GetGoalsByMentor(int mentorId)
        {
            var goals = await _context.Goals
                .Where(g => g.MentorId == mentorId)
                .ToListAsync();
            return Ok(goals);
        }

        // 3. POST: api/goals
        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return Ok(goal);
        }

        // 4. PUT: api/goals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalRequest update)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
                return NotFound();

            // Cập nhật Status và Feedback
            goal.Status = update.Status;
            goal.Feedback = update.Feedback;
            await _context.SaveChangesAsync();

            return Ok(goal);
        }

        public class UpdateGoalRequest
        {
            public string Status { get; set; }
            public string Feedback { get; set; }
        }
    }
}