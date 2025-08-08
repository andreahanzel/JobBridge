using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;

namespace JobBridge.Controllers
{
    [Route("api/[controller]")] // API route for users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JobBridgeContext _db;

        public UsersController(JobBridgeContext db)
        {
            _db = db;
        }

        // GET /users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _db.Users
                .OrderByDescending(u => u.LastName)
                .ToListAsync();

            return Ok(users);
        }

        // GET /users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST /users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT /users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest("ID mismatch");

            var existingUser = await _db.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            // Update fields
            existingUser.Role = user.Role;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.UpdatedAt = DateTime.UtcNow;

            _db.Users.Update(existingUser);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
