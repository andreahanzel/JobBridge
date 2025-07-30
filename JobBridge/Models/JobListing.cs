using System.ComponentModel.DataAnnotations;

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
        public string? Location { get; set; } // E.g., "New York, NY", "Remote"

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        // Job attributes
        public string? EmploymentType { get; set; } // E.g., "Full-time", "Part-time", "Contract"
        public string? ExperienceLevel { get; set; } // E.g., "Entry-level", "Mid", "Senior"
        public string? SalaryRange { get; set; } // E.g., "$50,000 - $70,000"

        // Foreign Key for Employer (if you create an EmployerProfile later)
        // public string? EmployerId { get; set; } // Link to ApplicationUser or a separate Employer entity
        // [ForeignKey("EmployerId")]
        // public ApplicationUser? Employer { get; set; }

        // Example status
        public bool IsActive { get; set; } = true;
    }
}