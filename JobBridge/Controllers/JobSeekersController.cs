using JobBridge.Data;
using JobBridge.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBridge.Controllers
{
    [Route("jobseekers")]
    [ApiController]
    public class JobSeekersController : ControllerBase
    {
        private readonly JobBridgeContext _db;

        public JobSeekersController(JobBridgeContext db)
        {
            _db = db;
        }

        // GET /jobseekers
        [HttpGet]
        public async Task<ActionResult<List<JobSeeker>>> GetJobSeekers()
        {
            var jobSeekers = await _db.JobSeekers
                .Include(js => js.User)
                .OrderBy(js => js.Id)
                .ToListAsync();

            return Ok(jobSeekers);
        }

        // GET /jobseekers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobSeeker>> GetJobSeeker(int id)
        {
            var jobSeeker = await _db.JobSeekers
                .Include(js => js.User)
                .FirstOrDefaultAsync(js => js.Id == id);

            if (jobSeeker == null)
                return NotFound();

            return Ok(jobSeeker);
        }

        // POST /jobseekers
        [HttpPost]
        public async Task<ActionResult<JobSeeker>> CreateJobSeeker([FromBody] JobSeeker jobSeeker)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.JobSeekers.Add(jobSeeker);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobSeeker), new { id = jobSeeker.Id }, jobSeeker);
        }

        // PUT /jobseekers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobSeeker(int id, [FromBody] JobSeeker jobSeeker)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (jobSeeker.Id != id)
                return BadRequest("ID mismatch");

            var existingJobSeeker = await _db.JobSeekers.FindAsync(id);
            if (existingJobSeeker == null)
                return NotFound();

            // Update fields manually
            existingJobSeeker.ResumeUrl = jobSeeker.ResumeUrl;
            existingJobSeeker.RememberMe = jobSeeker.RememberMe;
            existingJobSeeker.UserId = jobSeeker.UserId;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /jobseekers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            var jobSeeker = await _db.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
                return NotFound();

            _db.JobSeekers.Remove(jobSeeker);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
