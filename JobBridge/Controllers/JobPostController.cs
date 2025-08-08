using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Controllers;

[Route("api/[controller]")] // API route for job posts
[ApiController]
public class JobPostController : ControllerBase // Base class for API controllers
{
    private readonly JobBridgeContext _db; // Database context

    public JobPostController(JobBridgeContext db) // Constructor
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<JobPost>>> GetJobPosts() // Get all job posts
    {
        return await _db.JobPosts.OrderByDescending(jp => jp.CreatedAt).ToListAsync(); // Get all job posts
    }
}
