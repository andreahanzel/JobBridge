using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobBridge.Data
{
    public class Employers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name max length is 100 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location max length is 200 characters")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        [StringLength(100, ErrorMessage = "Industry max length is 100 characters")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "NumberOfEmployees must be greater than zero")]
        public int NumberOfEmployees { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // User relation
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = string.Empty;


        public User User { get; set; } = null!;

        // JobPosts relation
        public ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
    }
}
