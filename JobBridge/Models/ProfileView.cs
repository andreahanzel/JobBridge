using System.ComponentModel.DataAnnotations;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Models
{
    public class ProfileView
    {
        public int Id { get; set; }
        
        [Required]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = null!;
        
        [Required]
        public int EmployerId { get; set; }
        public Employers Employer { get; set; } = null!;
        
        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    }
}