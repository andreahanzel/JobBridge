using Microsoft.EntityFrameworkCore;

namespace JobBridge.Data
{
    public class JobBridgeContext : DbContext
    {
        public JobBridgeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship: JobPost → Employers
            modelBuilder.Entity<JobPost>()
                .HasOne(j => j.Employer)
                .WithMany()
                .HasForeignKey(j => j.EmployerId);

            // Relationship: JobPost → Fields
            modelBuilder.Entity<JobPost>()
                .HasOne(j => j.Field)
                .WithMany(f => f.JobPosts)
                .HasForeignKey(j => j.FieldId);

            // Relationship: Bookmarks → Users
            modelBuilder.Entity<Bookmarks>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            // Relationship: Bookmarks → JobPosts
            modelBuilder.Entity<Bookmarks>()
                .HasOne(b => b.JobPost)
                .WithMany()
                .HasForeignKey(b => b.JobPostId);
        }
    }
}
