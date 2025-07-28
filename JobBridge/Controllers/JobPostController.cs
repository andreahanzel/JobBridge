using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;

namespace JobBridge.Controllers;

[Route("jobs")]
[ApiController]
public class JobPostController : Controller
{
    private readonly JobBridgeContext _db;

    public JobPostController(JobBridgeContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employers>>> GetEmployers()
    {
        return (await _db.Employers.ToListAsync()).OrderByDescending(e => e.Name).ToList();
    }
}
