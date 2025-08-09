using System.ComponentModel.DataAnnotations;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Models // ViewModels for the application
{
    public class ProfileView // ViewModel for the profile
    {
        public int Id { get; set; }
        
        [Required]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = null!; // Job seeker associated with the profile

        [Required]
        public int EmployerId { get; set; }
        public Employers Employer { get; set; } = null!; // Employer associated with the profile

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow; // Timestamp when the profile was viewed
    }
}