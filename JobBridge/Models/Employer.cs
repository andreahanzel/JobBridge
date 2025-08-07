namespace JobBridge.Data;

public class Employers
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public int NumberOfEmployees { get; set; }

    public  DateTime CreatedAt { get; set; }

    public  DateTime UpdatedAt { get; set; }
}

