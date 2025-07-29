using JobBridge.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobBridge.Data;

public class JobBridgeContext : IdentityDbContext<ApplicationUser>
{
    public JobBridgeContext(DbContextOptions<JobBridgeContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employers> Employers { get; set; }
    public DbSet<JobPost> JobPosts { get; set; }
}