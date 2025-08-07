using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JobBridge.Data;
using JobBridge.Data.Models;

namespace JobBridge.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            using var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roles = { "JobSeeker", "Employer", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using var db = serviceProvider.GetRequiredService<JobBridgeContext>();
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
                    UserName = "john.doe@example.com",
                    Phone = "123-456-7890",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Role = "JobSeeker",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    UserName = "jane.smith@example.com",
                    Phone = "098-765-4321",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded && user.Role != null)
                {
                    await userManager.AddToRoleAsync(user, user.Role);
                }
            }
            await db.SaveChangesAsync();

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
            await db.SaveChangesAsync();

            // Fields
            var fields = new Field[]
            {
                new Field { FieldTitle = "Software Development" },
                new Field { FieldTitle = "Marketing" },
                new Field { FieldTitle = "Business" },
                new Field { FieldTitle = "Sales" }
            };
            db.Fields.AddRange(fields);
            await db.SaveChangesAsync();

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
                    FieldId = fields[0].Id
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
                    FieldId = fields[0].Id
                }
            };
            db.JobPosts.AddRange(jobPosts);
            await db.SaveChangesAsync();
        }
    }
}