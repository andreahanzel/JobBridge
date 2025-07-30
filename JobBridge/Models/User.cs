namespace JobBridge.Data;

public class User
{
    public int Id { get; set; }
    
    public required string Role { get; set; }
    
    public required string FirstName { get; set; }

    public string LastName { get; set; }
    
    public required string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}