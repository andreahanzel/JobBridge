using JobBridge.Data;
using System;

namespace JobBridge.Data.Models
{
    public class Bookmarks
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int JobPostId { get; set; }
        public JobPost? JobPost { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}