using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Controllers;

[Route("employers")]
[ApiController]
public class EmployersController : Controller
{
    private readonly JobBridgeContext _db;

    public EmployersController(JobBridgeContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employers>>> GetEmployers()
    {
        return (await _db.Employers.ToListAsync()).OrderByDescending(e => e.Name).ToList();
    }
}
