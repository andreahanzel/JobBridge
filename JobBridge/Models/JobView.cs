using System.ComponentModel.DataAnnotations;
using JobBridge.Data;

namespace JobBridge.Models
{
    public class JobView
    {
        public int Id { get; set; }
        
        [Required]
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; } = null!;
        
        public int? JobSeekerId { get; set; }
        public JobSeeker? JobSeeker { get; set; }
        
        [Required]
        public DateTime ViewedAt { get; set; } = DateTime.Now;
        
        public string? IpAddress { get; set; }
    }
}