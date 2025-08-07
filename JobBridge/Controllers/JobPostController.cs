using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Controllers;

[Route("jobposts")]
[ApiController]
public class JobPostController : Controller
{
    private readonly JobBridgeContext _db;

    public JobPostController(JobBridgeContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<JobPost>>> GetJobPosts()
    {
        return await _db.JobPosts.OrderByDescending(jp => jp.CreatedAt).ToListAsync();
    }
}
