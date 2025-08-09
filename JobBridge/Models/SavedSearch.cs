using System.ComponentModel.DataAnnotations;
using JobBridge.Data;

namespace JobBridge.Models
{
    public class SavedSearch // Represents a saved job search
    {
        public int Id { get; set; }
        
        [Required]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = null!; // Job seeker associated with the saved search

        [Required]
        [MaxLength(200)]
        public string SearchName { get; set; } = string.Empty; // Name of the saved search

        [MaxLength(100)]
        public string? JobTitle { get; set; }
        
        [MaxLength(100)]
        public string? Location { get; set; }
        
        [MaxLength(50)]
        public string? EmploymentType { get; set; }
        
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the saved search was created
    }
}