using System.Collections.Generic;

namespace JobBridge.Data
{
    public class Field
    {
        public int Id { get; set; }
        public string? FieldTitle { get; set; }
        public List<JobPost>? JobPosts { get; set; }
    }
}