namespace JobBridge.Data;

public class Field
{
    public int Id { get; set; }

    public string FieldTitle { get; set; }

    public ICollection<JobPost> JobPosts { get; set; }
}
