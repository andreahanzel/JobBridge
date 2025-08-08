using System.Collections.Generic;

namespace JobBridge.Data
{
    public class Field // Represents a field of study or expertise
    {
        public int Id { get; set; } // Unique identifier for the field
        public string? FieldTitle { get; set; } // Title of the field
        public List<JobPost>? JobPosts { get; set; } // List of job posts associated with the field
    }
}