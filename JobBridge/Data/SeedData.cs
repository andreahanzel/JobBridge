using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
            var usersList = new List<User>
            {
                await userManager.FindByEmailAsync("john.doe@example.com"),
                await userManager.FindByEmailAsync("jane.smith@example.com")
            };

            // Seed Employers
            if (!db.Employers.Any())
            {
                var employers = new Employers[]
                {
                    new Employers
                    {
                        Name = "TechNova",
                        Location = "New York",
                        NumberOfEmployees = 150,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        UserId = usersList[0].Id // Associate to John Doe user
                    },
                    new Employers
                    {
                        Name = "GreenSolutions",
                        Location = "Remote",
                        NumberOfEmployees = 50,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        UserId = usersList[1].Id // Associate to Jane Smith user
                    }
                };
                db.Employers.AddRange(employers);
                await db.SaveChangesAsync();
            }

            // Get existing employers for foreign key relations
            var employersList = db.Employers.ToList();

            // Seed Fields
            if (!db.Fields.Any())
            {
                var fields = new Field[]
                {
                    new Field { FieldTitle = "Software Development" },
                    new Field { FieldTitle = "Marketing" },
                    new Field { FieldTitle = "Business" },
                    new Field { FieldTitle = "Sales" }
                };
                db.Fields.AddRange(fields);
                await db.SaveChangesAsync();
            }

            // Get existing fields for foreign key relations
            var fieldsList = db.Fields.ToList();

            // Seed Job Posts
            if (!db.JobPosts.Any())
            {
                var jobPosts = new JobPost[]
                {
                    new JobPost
                    {
                        JobTitle = "Frontend Developer",
                        Department = "Engineering",
                        EmploymentType = "Full-Time",
                        ExperienceLevel = "Mid Level",
                        WorkArrangement = "On-site",
                        Location = "New York, NY",
                        Timezone = "EST (Eastern)",
                        MinimumSalary = 55000,
                        MaximumSalary = 75000,
                        AdditionalCompensation = "Health insurance, 401k, stock options",
                        JobSummary = "We are looking for a skilled Frontend Developer to join our team and build amazing user interfaces using React.",
                        KeyResponsibilities = "• Develop responsive web applications using React\n• Collaborate with designers to implement UI/UX designs\n• Write clean, maintainable code\n• Participate in code reviews",
                        RequiredQualifications = "• 3+ years of experience with React\n• Strong knowledge of JavaScript, HTML, CSS\n• Experience with TypeScript\n• Bachelor's degree in Computer Science or related field",
                        PreferredQualifications = "• Experience with Redux or other state management\n• Knowledge of testing frameworks like Jest\n• Familiarity with CI/CD pipelines",
                        RequiredSkills = "React, JavaScript, TypeScript, HTML, CSS",
                        NiceToHaveSkills = "Redux, Jest, Webpack, Git",
                        ApplicationMethod = "JobBridge Application System",
                        ExternalApplicationUrl = null,
                        PostedDate = DateTime.UtcNow,
                        ApplicationDeadline = DateTime.UtcNow.AddDays(30),
                        PostExpirationDate = DateTime.UtcNow.AddDays(30),
                        NumberOfApplicants = 0,
                        IsFeatured = false,
                        IsUrgent = true,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EmployerId = employersList[0].Id,
                        FieldId = fieldsList[0].Id
                    },
                    new JobPost
                    {
                        JobTitle = "Digital Marketing Specialist",
                        Department = "Marketing",
                        EmploymentType = "Part-Time",
                        ExperienceLevel = "Entry Level",
                        WorkArrangement = "Hybrid",
                        Location = "San Francisco, CA",
                        Timezone = "PST (Pacific)",
                        MinimumSalary = 30000,
                        MaximumSalary = 45000,
                        AdditionalCompensation = "Health insurance, flexible hours",
                        JobSummary = "Join our marketing team as a Digital Marketing Specialist to help drive online campaigns and increase brand awareness.",
                        KeyResponsibilities = "• Plan and execute digital marketing campaigns\n• Manage social media accounts\n• Analyze campaign performance\n• Collaborate with content creators",
                        RequiredQualifications = "• 1-2 years of experience in digital marketing\n• Knowledge of SEO, SEM, and social media marketing\n• Strong analytical skills\n• Bachelor's degree in Marketing or related field",
                        PreferredQualifications = "• Experience with Google Analytics and AdWords\n• Familiarity with email marketing platforms\n• Creative thinking and problem-solving skills",
                        RequiredSkills = "SEO, SEM, Social Media, Google Analytics",
                        NiceToHaveSkills = "AdWords, Email Marketing, Content Creation",
                        ApplicationMethod = "JobBridge Application System",
                        ExternalApplicationUrl = null,
                        PostedDate = DateTime.UtcNow,
                        ApplicationDeadline = DateTime.UtcNow.AddDays(60),
                        PostExpirationDate = DateTime.UtcNow.AddDays(60),
                        IsFeatured = false,
                        IsUrgent = false,
                        IsActive = false,
                        NumberOfApplicants = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EmployerId = employersList[0].Id,
                        FieldId = fieldsList[1].Id
                    },
                    new JobPost
                    {
                        JobTitle = "Backend Developer",
                        Department = "Engineering",
                        EmploymentType = "Full-Time",
                        ExperienceLevel = "Senior Level",
                        WorkArrangement = "Remote",
                        Location = "Remote",
                        Timezone = "CST (Central)",
                        MinimumSalary = 70000,
                        MaximumSalary = 95000,
                        AdditionalCompensation = "Health insurance, unlimited PTO, remote work stipend",
                        JobSummary = "Join our team as a Senior Backend Developer to build scalable APIs and microservices using .NET Core.",
                        KeyResponsibilities = "• Design and implement RESTful APIs\n• Work with databases and data modeling\n• Optimize application performance\n• Mentor junior developers",
                        RequiredQualifications = "• 5+ years of experience with C# and .NET Core\n• Strong knowledge of Entity Framework\n• Experience with SQL databases\n• Experience with cloud platforms (Azure preferred)",
                        PreferredQualifications = "• Experience with microservices architecture\n• Knowledge of Docker and Kubernetes\n• Experience with DevOps practices",
                        RequiredSkills = "C#, .NET Core, Entity Framework, SQL, Azure",
                        NiceToHaveSkills = "Docker, Kubernetes, Redis, RabbitMQ",
                        ApplicationMethod = "External Link/Email",
                        ExternalApplicationUrl = "https://careers.greensolutions.com/job/backend",
                        PostedDate = DateTime.UtcNow,
                        ApplicationDeadline = DateTime.UtcNow.AddDays(60),
                        PostExpirationDate = DateTime.UtcNow.AddDays(60),
                        IsFeatured = true,
                        IsUrgent = false,
                        IsActive = true,
                        NumberOfApplicants = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EmployerId = employersList[1].Id,
                        FieldId = fieldsList[0].Id
                    }
                };
                db.JobPosts.AddRange(jobPosts);
                await db.SaveChangesAsync();
            }

            // Seed Job Seekers
            if (!db.JobSeekers.Any())
            {
                var jobSeekers = new JobSeeker[]
                {
                    new JobSeeker
                    {
                        UserId = usersList[1].Id, // Reference to Jane Smith
                        ResumeUrl = "https://example.com/resume/jane-smith.pdf",
                        RememberMe = true
                    }
                };
                db.JobSeekers.AddRange(jobSeekers);
                await db.SaveChangesAsync();
            }

            // Get existing job seekers for applications
            var jobSeekersList = db.JobSeekers.ToList();
            var jobPostsList = db.JobPosts.ToList();

            // Seed Applications
            if (!db.Applications.Any())
            {
                var applications = new Application[]
                {
                    new Application
                    {
                        JobSeekerId = jobSeekersList[0].Id,
                        JobPostId = jobPostsList[0].Id,
                        Status = ApplicationStatus.Pending,
                        AppliedDate = DateTime.UtcNow.AddDays(-5)
                    },
                    new Application
                    {
                        JobSeekerId = jobSeekersList[0].Id,
                        JobPostId = jobPostsList[2].Id,
                        Status = ApplicationStatus.Interview,
                        AppliedDate = DateTime.UtcNow.AddDays(-2)
                    }
                };
                db.Applications.AddRange(applications);
                await db.SaveChangesAsync();
            }
        }
    }
}