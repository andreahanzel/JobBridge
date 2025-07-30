using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;

namespace JobBridge.Controllers;

[Route("users")]
[ApiController]
public class UsersController : Controller
{
    private readonly JobBridgeContext _db;

    public UsersController(JobBridgeContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return (await _db.Users.ToListAsync()).OrderByDescending(u => u.LastName).ToList();
    }
}