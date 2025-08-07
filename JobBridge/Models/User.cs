using Microsoft.AspNetCore.Identity;
using System;

namespace JobBridge.Data
{
    public class User : IdentityUser
    {
        public required string Role { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public new required string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}