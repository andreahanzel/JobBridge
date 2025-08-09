using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models;
using JobBridge.Models;

namespace JobBridge.Data;

public class JobBridgeContext : IdentityDbContext<User>
{
    public JobBridgeContext(DbContextOptions<JobBridgeContext> options) : base(options) // Constructor
    {
    }

  //  public new DbSet<User> Users { get; set; }
    public DbSet<JobSeeker> JobSeekers { get; set; } // Job seekers
    public DbSet<Employers> Employers { get; set; } // Employers
    public DbSet<JobPost> JobPosts { get; set; } // Job posts
    public DbSet<Field> Fields { get; set; } // Job fields
    public DbSet<Bookmark> Bookmarks { get; set; } // Bookmarks
    public DbSet<Application> Applications { get; set; } // Job applications
    public DbSet<ProfileView> ProfileViews { get; set; } // Profile views
    public DbSet<SavedSearch> SavedSearches { get; set; } // Saved searches
    public DbSet<JobView> JobViews { get; set; } // Job views

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

        // Relationship: ProfileView → JobSeeker (N:1)
        modelBuilder.Entity<ProfileView>()
            .HasOne(pv => pv.JobSeeker)
            .WithMany()
            .HasForeignKey(pv => pv.JobSeekerId);

        // Relationship: ProfileView → Employer (N:1)
        modelBuilder.Entity<ProfileView>()
            .HasOne(pv => pv.Employer)
            .WithMany()
            .HasForeignKey(pv => pv.EmployerId);

        // Relationship: SavedSearch → JobSeeker (N:1)
        modelBuilder.Entity<SavedSearch>()
            .HasOne(ss => ss.JobSeeker)
            .WithMany()
            .HasForeignKey(ss => ss.JobSeekerId);

        // Relationship: JobView → JobPost (N:1)
        modelBuilder.Entity<JobView>()
            .HasOne(jv => jv.JobPost)
            .WithMany()
            .HasForeignKey(jv => jv.JobPostId);

        // Relationship: JobView → JobSeeker (N:1) - Optional
        modelBuilder.Entity<JobView>()
            .HasOne(jv => jv.JobSeeker)
            .WithMany()
            .HasForeignKey(jv => jv.JobSeekerId);
    }
}