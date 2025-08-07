using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models;

namespace JobBridge.Data
{
    public class JobBridgeContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public JobBridgeContext(DbContextOptions<JobBridgeContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback to appsettings.json connection string if not configured via DI
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Bookmarks> Bookmarks { get; set; }
    }
}