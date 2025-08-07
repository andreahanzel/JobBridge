using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobBridge.Data.Models;

namespace JobBridge.Data.Models
{
    public class JobListing
    {
        // Primary Key
        public int Id { get; set; }

        // Core Job Details
        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string? Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        // Job attributes
        public string? EmploymentType { get; set; }
        public string? ExperienceLevel { get; set; }
        public string? SalaryRange { get; set; }

        // Foreign Key for Employer
        // public int EmployerId { get; set; }
        // [ForeignKey("EmployerId")]
        // public Employers? Employer { get; set; }

        // Example status
        public bool IsActive { get; set; } = true;
    }
}