using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobBridge.Data.Models; // <<< Make sure this using statement is present!

namespace JobBridge.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        // Add your DbSet for JobListing here
        public DbSet<JobListing> JobListings { get; set; } // <<< Add this line!

        // You can add more DbSets for other models as your project grows, e.g.,
        // public DbSet<Application> Applications { get; set; }
        // public DbSet<EmployerProfile> EmployerProfiles { get; set; }
    }
}