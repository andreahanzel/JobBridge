using System.ComponentModel.DataAnnotations;

namespace JobBridge.Data;

public class Application
{
    public int Id { get; set; }

    // Foreign key to JobSeeker
    public int JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;

    // Foreign key to JobPost
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;

    // Date when application was submitted
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

    // Application status (Pending, Reviewed, Interview, etc.)
    [Required]
    
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
}

public enum ApplicationStatus
{
    Pending,
    Reviewed,
    Interview,
    Offered,
    Rejected
}