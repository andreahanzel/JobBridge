using JobBridge.Data;
using JobBridge.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBridge.Controllers
{
    [Route("applications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly JobBridgeContext _db;

        public ApplicationsController(JobBridgeContext db)
        {
            _db = db;
        }

        // GET /applications
        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetApplications()
        {
            var applications = await _db.Applications
                .Include(a => a.JobSeeker)
                    .ThenInclude(js => js.User)
                .Include(a => a.JobPost)
                    .ThenInclude(jp => jp.Employer)
                .ToListAsync();

            return Ok(applications);
        }

        // GET /applications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _db.Applications
                .Include(a => a.JobSeeker)
                    .ThenInclude(js => js.User)
                .Include(a => a.JobPost)
                    .ThenInclude(jp => jp.Employer)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return NotFound();

            return Ok(application);
        }

        // POST /applications
        [HttpPost]
        public async Task<ActionResult<Application>> CreateApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            application.AppliedDate = DateTime.UtcNow;
            application.Status = ApplicationStatus.Pending;

            _db.Applications.Add(application);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        // PUT /applications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] Application application)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != application.Id)
                return BadRequest("ID mismatch");

            var existingApplication = await _db.Applications.FindAsync(id);
            if (existingApplication == null)
                return NotFound();

            // Update fields manually (except Id)
            existingApplication.JobSeekerId = application.JobSeekerId;
            existingApplication.JobPostId = application.JobPostId;
            existingApplication.Status = application.Status;
            existingApplication.AppliedDate = application.AppliedDate;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /applications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _db.Applications.FindAsync(id);
            if (application == null)
                return NotFound();

            _db.Applications.Remove(application);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
