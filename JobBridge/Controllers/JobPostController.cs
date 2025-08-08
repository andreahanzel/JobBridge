using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobPostController : Controller
{
    private readonly JobBridgeContext _db;

    public JobPostController(JobBridgeContext db)
    {
        _db = db;
    }

    // GET /api/jobpost
    [HttpGet]
    public async Task<ActionResult<List<JobPost>>> GetJobPosts()
    {
        return await _db.JobPosts.OrderByDescending(jp => jp.CreatedAt).ToListAsync();
    }

    // GET /api/jobpost/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<JobPost>> GetJobPost(int id)
    {
        var jobPost = await _db.JobPosts.FindAsync(id);

        if (jobPost == null)
            return NotFound();

        return Ok(jobPost);
    }

    // POST /api/jobpost
    [HttpPost]
    public async Task<ActionResult<JobPost>> CreateJobPost([FromBody] JobPost jobPost)
    {
        if (jobPost == null)
            return BadRequest("JobPost is null");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        jobPost.CreatedAt = DateTime.UtcNow;
        jobPost.UpdatedAt = DateTime.UtcNow;
        jobPost.PostedDate = DateTime.UtcNow;

        _db.JobPosts.Add(jobPost);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJobPost), new { id = jobPost.Id }, jobPost);
    }

    // PUT /api/jobpost/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobPost(int id, [FromBody] JobPost jobPost)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingJobPost = await _db.JobPosts.FindAsync(id);
        if (existingJobPost == null)
            return NotFound();

        // Update fields
        existingJobPost.JobTitle = jobPost.JobTitle;
        existingJobPost.Department = jobPost.Department;
        existingJobPost.EmploymentType = jobPost.EmploymentType;
        existingJobPost.ExperienceLevel = jobPost.ExperienceLevel;
        existingJobPost.WorkArrangement = jobPost.WorkArrangement;
        existingJobPost.Location = jobPost.Location;
        existingJobPost.Timezone = jobPost.Timezone;
        existingJobPost.MinimumSalary = jobPost.MinimumSalary;
        existingJobPost.MaximumSalary = jobPost.MaximumSalary;
        existingJobPost.AdditionalCompensation = jobPost.AdditionalCompensation;
        existingJobPost.JobSummary = jobPost.JobSummary;
        existingJobPost.KeyResponsibilities = jobPost.KeyResponsibilities;
        existingJobPost.RequiredQualifications = jobPost.RequiredQualifications;
        existingJobPost.PreferredQualifications = jobPost.PreferredQualifications;
        existingJobPost.RequiredSkills = jobPost.RequiredSkills;
        existingJobPost.NiceToHaveSkills = jobPost.NiceToHaveSkills;
        existingJobPost.ApplicationMethod = jobPost.ApplicationMethod;
        existingJobPost.ExternalApplicationUrl = jobPost.ExternalApplicationUrl;
        existingJobPost.ApplicationDeadline = jobPost.ApplicationDeadline;
        existingJobPost.PostExpirationDate = jobPost.PostExpirationDate;
        existingJobPost.IsFeatured = jobPost.IsFeatured;
        existingJobPost.IsUrgent = jobPost.IsUrgent;
        existingJobPost.IsActive = jobPost.IsActive;
        existingJobPost.EmployerId = jobPost.EmployerId;
        existingJobPost.FieldId = jobPost.FieldId;
        existingJobPost.UpdatedAt = DateTime.UtcNow;

        _db.JobPosts.Update(existingJobPost);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/jobpost/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobPost(int id)
    {
        var jobPost = await _db.JobPosts.FindAsync(id);
        if (jobPost == null)
            return NotFound();

        _db.JobPosts.Remove(jobPost);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
