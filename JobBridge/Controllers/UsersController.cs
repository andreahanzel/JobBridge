using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JobBridgeContext _context;

        public UsersController(JobBridgeContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}