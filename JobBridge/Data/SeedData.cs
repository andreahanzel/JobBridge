namespace JobBridge.Data;

public static class SeedData
{
    public static void Initialize(JobBridgeContext db)
    {
        var users = new User[]
        {
            new User()
            {
                Id = 1,
                Role = "Admin",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                Password = "password",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User()
            {
                Id = 2,
                Role = "User",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "098-765-4321",
                Password = "password",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        };
        db.Users.AddRange(users);
        db.SaveChanges();
    }
}