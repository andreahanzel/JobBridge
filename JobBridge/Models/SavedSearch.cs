using System.ComponentModel.DataAnnotations;
using JobBridge.Data;

namespace JobBridge.Models
{
    public class SavedSearch
    {
        public int Id { get; set; }
        
        [Required]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = null!;
        
        [Required]
        [MaxLength(200)]
        public string SearchName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? JobTitle { get; set; }
        
        [MaxLength(100)]
        public string? Location { get; set; }
        
        [MaxLength(50)]
        public string? EmploymentType { get; set; }
        
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}