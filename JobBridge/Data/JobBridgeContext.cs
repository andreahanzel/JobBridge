using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models;

namespace JobBridge.Data;

public class JobBridgeContext : IdentityDbContext<User>
{
    public JobBridgeContext(DbContextOptions<JobBridgeContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<JobSeeker> JobSeekers { get; set; } // Added
    public DbSet<Employers> Employers { get; set; }
    public DbSet<JobPost> JobPosts { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Application> Applications { get; set; }// Added

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Application enum conversion int → string
        modelBuilder.Entity<Application>()
            .Property(a => a.Status)
            .HasConversion<string>();
        // Application → JobSeeker (N:1)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobSeeker)
            .WithMany(js => js.Applications)
            .HasForeignKey(a => a.JobSeekerId);

        // Application → JobPost (N:1)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobPost)
            .WithMany()
            .HasForeignKey(a => a.JobPostId);

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

        // Relationship: User → JobSeeker (1:1)
        modelBuilder.Entity<User>()
            .HasOne<JobSeeker>()
            .WithOne(js => js.User)
            .HasForeignKey<JobSeeker>(js => js.UserId);

        // Relationship: JobSeeker → Bookmarks (1:N)
        modelBuilder.Entity<JobSeeker>()
            .HasMany(js => js.Bookmarks)
            .WithOne(b => b.JobSeeker)
            .HasForeignKey(b => b.JobSeekerId);

        // Relationship: Bookmark → JobPost (N:1)
        modelBuilder.Entity<Bookmark>()
            .HasOne(b => b.JobPost)
            .WithMany()
            .HasForeignKey(b => b.JobPostId);
    }
}