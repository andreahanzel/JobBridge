using System.ComponentModel.DataAnnotations;
namespace JobBridge.Data;

public class User
{
    public int Id { get; set; }

    public required string Role { get; set; }
    [Required(ErrorMessage = "First name is required")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Phone number is required")]
    public string Phone { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public bool RememberMe { get; set; }

}