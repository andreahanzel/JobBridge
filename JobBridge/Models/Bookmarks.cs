using JobBridge.Data;
using System;

public class Bookmark
{
    public int Id { get; set; }

    // Foreign key to JobSeeker
    public int JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;

    // Foreign key to JobPost
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;

}