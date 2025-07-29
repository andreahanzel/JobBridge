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

        var employers = new Employers[]
        {
            new Employers()
            {
                Id = 1,
                Name = "Tech Solutions",
                Location = "New York, NY",
                NumberOfEmployees = 50,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Employers()
            {
                Id = 2,
                Name = "Innovative Designs",
                Location = "San Francisco, CA",
                NumberOfEmployees = 30,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        };

        var jobPosts = new JobPost[]
        {
            new JobPost()
            {
                Id = 1,
                Title = "Software Engineer",
                Description = "Develop and maintain software applications.",
                Requirements = "Bachelor's degree in Computer Science or related field.",
                Salary = 80000,
                ApplicationLink = "https://example.com/apply",
                NumberOfApplications = 5,
                DatePosted = DateTime.UtcNow,
                PostExpirationDate = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new JobPost()
            {
                Id = 2,
                Title = "Graphic Designer",
                Description = "Create visual concepts to communicate ideas.",
                Requirements = "Proficiency in Adobe Creative Suite.",
                Salary = 60000,
                ApplicationLink = "https://example.com/apply",
                NumberOfApplications = 3,
                DatePosted = DateTime.UtcNow,
                PostExpirationDate = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        };

        db.Users.AddRange(users);
        db.Employers.AddRange(employers);
        db.JobPosts.AddRange(jobPosts);
        db.SaveChanges();
    }
}