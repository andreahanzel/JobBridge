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
                    JobTitle = "Frontend Developer",
                    Department = "Engineering",
                    EmploymentType = "Full-Time",
                    ExperienceLevel = "Mid",
                    WorkArrangement = "Onsite",
                    Location = "New York, NY",
                    Timezone = "EST",
                    MinimumSalary = 55000,
                    MaximumSalary = 75000,
                    AdditionalCompensation = "Health insurance, 401k, stock options",
                    JobSummary = "We are looking for a skilled Frontend Developer to join our team and build amazing user interfaces using React.",
                    KeyResponsibilities = "• Develop responsive web applications using React\n• Collaborate with designers to implement UI/UX designs\n• Write clean, maintainable code\n• Participate in code reviews",
                    RequiredQualifications = "• 3+ years of experience with React\n• Strong knowledge of JavaScript, HTML, CSS\n• Experience with TypeScript\n• Bachelor's degree in Computer Science or related field",
                    PreferredQualifications = "• Experience with Redux or other state management\n• Knowledge of testing frameworks like Jest\n• Familiarity with CI/CD pipelines",
                    RequiredSkills = "React, JavaScript, TypeScript, HTML, CSS",
                    NiceToHaveSkills = "Redux, Jest, Webpack, Git",
                    ApplicationMethod = "internal",
                    ExternalApplicationUrl = "",
                    PostedDate = DateTime.UtcNow,
                    ApplicationDeadline = DateTime.UtcNow.AddDays(30),
                    PostExpirationDate = DateTime.UtcNow.AddDays(30),
                    NumberOfApplicants = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmployerId = employers[0].Id,
                    FieldId = fields[0].Id
                },
                new JobPost
                {
                    JobTitle = "Backend Developer",
                    Department = "Engineering",
                    EmploymentType = "Full-Time",
                    ExperienceLevel = "Senior",
                    WorkArrangement = "Remote",
                    Location = "Remote",
                    Timezone = "PST",
                    MinimumSalary = 70000,
                    MaximumSalary = 95000,
                    AdditionalCompensation = "Health insurance, unlimited PTO, remote work stipend",
                    JobSummary = "Join our team as a Senior Backend Developer to build scalable APIs and microservices using .NET Core.",
                    KeyResponsibilities = "• Design and implement RESTful APIs\n• Work with databases and data modeling\n• Optimize application performance\n• Mentor junior developers",
                    RequiredQualifications = "• 5+ years of experience with C# and .NET Core\n• Strong knowledge of Entity Framework\n• Experience with SQL databases\n• Experience with cloud platforms (Azure preferred)",
                    PreferredQualifications = "• Experience with microservices architecture\n• Knowledge of Docker and Kubernetes\n• Experience with DevOps practices",
                    RequiredSkills = "C#, .NET Core, Entity Framework, SQL, Azure",
                    NiceToHaveSkills = "Docker, Kubernetes, Redis, RabbitMQ",
                    ApplicationMethod = "external",
                    ExternalApplicationUrl = "https://careers.greensolutions.com/job/backend",
                    PostedDate = DateTime.UtcNow,
                    ApplicationDeadline = DateTime.UtcNow.AddDays(45),
                    PostExpirationDate = DateTime.UtcNow.AddDays(45),
                    NumberOfApplicants = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmployerId = employers[1].Id,
                    FieldId = fields[0].Id
                }
            };

            db.JobPosts.AddRange(jobPosts);
            db.SaveChanges();
        }
    }
}
