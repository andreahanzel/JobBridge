using JobBridge.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models;

namespace JobBridge.Controllers // Handles employer-related actions
{
    [Route("api/[controller]")] // API route for employers
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly JobBridgeContext _db;

        public EmployersController(JobBridgeContext db)
        {
            _db = db;
        }

        // GET /employers
        [HttpGet]
        public async Task<ActionResult<List<Employers>>> GetEmployers()
        {
            var employers = await _db.Employers
                .OrderByDescending(e => e.Name)
                .ToListAsync();

            return Ok(employers);
        }

        // GET /employers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employers>> GetEmployer(int id)
        {
            var employer = await _db.Employers.FindAsync(id);

            if (employer == null)
                return NotFound();

            return Ok(employer);
        }

        // POST /employers
        [HttpPost]
        public async Task<ActionResult<Employers>> CreateEmployer([FromBody] Employers employer)
        {
            if (employer == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            employer.CreatedAt = DateTime.UtcNow;
            employer.UpdatedAt = DateTime.UtcNow;

            _db.Employers.Add(employer);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployer), new { id = employer.Id }, employer);
        }

        // PUT /employers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, [FromBody] Employers employer)
        {
            if (employer == null || employer.Id != id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployer = await _db.Employers.FindAsync(id);
            if (existingEmployer == null)
                return NotFound();

            // Update fields
            existingEmployer.Name = employer.Name;
            existingEmployer.Location = employer.Location;
            existingEmployer.Industry = employer.Industry;
            existingEmployer.NumberOfEmployees = employer.NumberOfEmployees;
            existingEmployer.UserId = employer.UserId;
            existingEmployer.UpdatedAt = DateTime.UtcNow;

            _db.Employers.Update(existingEmployer);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /employers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var employer = await _db.Employers.FindAsync(id);
            if (employer == null)
                return NotFound();

            _db.Employers.Remove(employer);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
