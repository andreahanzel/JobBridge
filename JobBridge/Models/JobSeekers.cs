using System.ComponentModel.DataAnnotations;

namespace JobBridge.Data
{
    public class JobSeeker
    {
        public int Id { get; set; }

        // Link to the resume file or portfolio
        [Url(ErrorMessage = "ResumeUrl must be a valid URL")]
        [MaxLength(2083, ErrorMessage = "ResumeUrl length can't exceed 2083 characters")] // Typical max URL length
        public string? ResumeUrl { get; set; }

        // Remember me option for login
        public bool RememberMe { get; set; }

        // User relation (1:1)
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public User User { get; set; } = null!;

        // Bookmarks relation (1:N)
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

        // Applications relation (1:N)
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
