using Microsoft.EntityFrameworkCore;

namespace JobBridge.Data;

public class JobBridgeContext : DbContext
{
    public JobBridgeContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employers> Employers { get; set; }
    public DbSet<JobPosts> JobPosts { get; set; }
}