using System;
using System.Linq;

namespace JobBridge.Data
{
    public static class SeedData
    {
        public static void Initialize(JobBridgeContext db)
        {
            //avoid Duplication
            if (db.Users.Any() || db.Employers.Any() || db.JobPosts.Any()) return;

            // Users
            var users = new User[]
            {
                new User
                {
                    Role = "Admin",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Password = "password",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Role = "User",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Phone = "098-765-4321",
                    Password = "password",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            db.Users.AddRange(users);
            db.SaveChanges();

            // Employers
            var employers = new Employers[]
            {
                new Employers
                {
                    Name = "TechNova",
                    Location = "New York",
                    NumberOfEmployees = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Employers
                {
                    Name = "GreenSolutions",
                    Location = "Remote",
                    NumberOfEmployees = 50,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            db.Employers.AddRange(employers);
            db.SaveChanges();

            //Fields
            var fields = new Field[]
            {
                new Field { FieldTitle = "Software Development" },
                new Field { FieldTitle = "Marketing" },
                new Field { FieldTitle = "Business" },
                new Field { FieldTitle = "Sales" }
            };
            db.Fields.AddRange(fields);
            db.SaveChanges();

            // Jobs
            var jobPosts = new JobPost[]
            {
                new JobPost
                {
                    Title = "Frontend Developer",
                    Description = "Develop UI using React.",
                    Requirements = "Experience with React and TypeScript.",
                    Salary = 60000,
                    ApplicationLink = "https://careers.technova.com/job/frontend",
                    NumberOfApplications = 0,
                    DatePosted = DateTime.UtcNow,
                    PostExpirationDate = DateTime.UtcNow.AddDays(30),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmployerId = employers[0].Id,
                    FieldId = fields[0].Id // Assign FieldId
                },
                new JobPost
                {
                    Title = "Backend Developer",
                    Description = "Build APIs using .NET Core.",
                    Requirements = "Experience with C# and EF Core.",
                    Salary = 70000,
                    ApplicationLink = "https://careers.greensolutions.com/job/backend",
                    NumberOfApplications = 0,
                    DatePosted = DateTime.UtcNow,
                    PostExpirationDate = DateTime.UtcNow.AddDays(45),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmployerId = employers[1].Id,
                    FieldId = fields[0].Id // Assign FieldId
                }
            };

            db.JobPosts.AddRange(jobPosts);
            db.SaveChanges();
        }
    }
}
