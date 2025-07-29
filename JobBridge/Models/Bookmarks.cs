namespace JobBridge.Data;

public class Bookmark
{
    public int Id { get; set; }
   
    // Foreign Keys
    public int UserId { get; set; }
    public User User { get; set; }

    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; }

}