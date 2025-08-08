using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using JobBridge.Data;
using JobBridge.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JobBridge.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Use a service scope to get required services.
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var db = scope.ServiceProvider.GetRequiredService<JobBridgeContext>();

            // Seed roles if they don't exist.
            string[] roles = { "JobSeeker", "Employer", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // Define users to be seeded.
            var usersToSeed = new User[]
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
                },
                new User
                {
                    Role = "Employer",
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@example.com",
                    UserName = "bob.johnson@example.com",
                    Phone = "555-123-4567",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            // List to hold the User objects after creation/retrieval.
            var createdUsers = new List<User>();

            // Create users if they don't exist and add them to the createdUsers list.
            foreach (var user in usersToSeed)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    user.EmailConfirmed = true;
                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        if (user.Role != null)
                        {
                            await userManager.AddToRoleAsync(user, user.Role);
                        }
                        createdUsers.Add(user);
                        Console.WriteLine($"Created user: {user.Email} with role: {user.Role}");
                    }
                    else
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        Console.WriteLine($"Warning: Could not create user {user.Email}: {errors}");
                        existingUser = await userManager.FindByEmailAsync(user.Email);
                        if (existingUser != null)
                        {
                            createdUsers.Add(existingUser);
                        }
                    }
                }
                else
                {
                    // If the user already exists, add the existing object to the list.
                    createdUsers.Add(existingUser);
                    Console.WriteLine($"User {user.Email} already exists.");
                }
            }

            // Get the specific user objects from the list.
            var john = createdUsers.FirstOrDefault(u => u.Email == "john.doe@example.com");
            var jane = createdUsers.FirstOrDefault(u => u.Email == "jane.smith@example.com");
            var bob = createdUsers.FirstOrDefault(u => u.Email == "bob.johnson@example.com");

            if (john == null || jane == null || bob == null)
            {
                Console.WriteLine("Warning: Some users not found after creation/retrieval. Continuing with available users.");
            }

            // Seed Employers
            var employersToAdd = new List<Employers>();
            
            // Add employer for Bob (who has Employer role)
            if (bob != null && !db.Employers.Any(e => e.UserId == bob.Id))
            {
                employersToAdd.Add(new Employers
                {
                    Name = "TechNova",
                    Location = "New York",
                    NumberOfEmployees = 150,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = bob.Id
                });
            }
            
            // You can add another employer if needed
            if (john != null && !db.Employers.Any(e => e.UserId == john.Id))
            {
                employersToAdd.Add(new Employers
                {
                    Name = "GreenSolutions",
                    Location = "Remote",
                    NumberOfEmployees = 50,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = john.Id
                });
            }
            
            if (employersToAdd.Any())
            {
                db.Employers.AddRange(employersToAdd);
                await db.SaveChangesAsync();
                Console.WriteLine($"Added {employersToAdd.Count} employer records.");
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
                Console.WriteLine("Added field categories.");
            }

            // Get existing fields for foreign key relations
            var fieldsList = db.Fields.ToList();

            // Seed Job Posts only if we have employers and fields
            if (!db.JobPosts.Any() && employersList.Any() && fieldsList.Any())
            {
                var jobPosts = new List<JobPost>();
                
                if (employersList.Count > 0 && fieldsList.Count > 0)
                {
                    jobPosts.Add(new JobPost
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
                        ExternalApplicationUrl = "https://www.indeed.com",
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
                    });
                }
                
                if (employersList.Count > 0 && fieldsList.Count > 1)
                {
                    jobPosts.Add(new JobPost
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
                        ExternalApplicationUrl = "https://www.linkedin.com/jobs",
                        PostedDate = DateTime.UtcNow,
                        ApplicationDeadline = DateTime.UtcNow.AddDays(60),
                        PostExpirationDate = DateTime.UtcNow.AddDays(60),
                        IsFeatured = false,
                        IsUrgent = false,
                        IsActive = true,
                        NumberOfApplicants = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EmployerId = employersList[0].Id,
                        FieldId = fieldsList[1].Id
                    });
                }
                
                if (employersList.Count > 1 && fieldsList.Count > 0)
                {
                    jobPosts.Add(new JobPost
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
                        PreferredQualifications = "• Experience with microservices architecture\n• Knowledge of Docker and Kubernetes\n• Familiarity with DevOps practices",
                        RequiredSkills = "C#, .NET Core, Entity Framework, SQL, Azure",
                        NiceToHaveSkills = "Docker, Kubernetes, Redis, RabbitMQ",
                        ApplicationMethod = "External Link/Email",
                        ExternalApplicationUrl = "https://www.glassdoor.com",
                        PostedDate = DateTime.UtcNow,
                        ApplicationDeadline = DateTime.UtcNow.AddDays(60),
                        PostExpirationDate = DateTime.UtcNow.AddDays(60),
                        IsFeatured = true,
                        IsUrgent = false,
                        IsActive = true,
                        NumberOfApplicants = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EmployerId = employersList.Count > 1 ? employersList[1].Id : employersList[0].Id,
                        FieldId = fieldsList[0].Id
                    });
                }
                
                if (jobPosts.Any())
                {
                    db.JobPosts.AddRange(jobPosts);
                    await db.SaveChangesAsync();
                    Console.WriteLine($"Added {jobPosts.Count} job posts.");
                }
            }

            // Seed Job Seekers
            if (jane != null && !db.JobSeekers.Any(js => js.UserId == jane.Id))
            {
                var jobSeeker = new JobSeeker
                {
                    UserId = jane.Id,
                    ResumeUrl = "https://example.com/resume/jane-smith.pdf",
                    RememberMe = true
                };
                db.JobSeekers.Add(jobSeeker);
                await db.SaveChangesAsync();
                Console.WriteLine("Added JobSeeker record for Jane.");
            }

            // Get existing job seekers and job posts for applications
            var jobSeekersList = db.JobSeekers.ToList();
            var jobPostsList = db.JobPosts.ToList();

            // Seed Applications only if we have job seekers and job posts
            if (!db.Applications.Any() && jobSeekersList.Any() && jobPostsList.Any())
            {
                var applications = new List<Application>();
                
                if (jobSeekersList.Count > 0 && jobPostsList.Count > 0)
                {
                    applications.Add(new Application
                    {
                        JobSeekerId = jobSeekersList[0].Id,
                        JobPostId = jobPostsList[0].Id,
                        Status = ApplicationStatus.Pending,
                        AppliedDate = DateTime.UtcNow.AddDays(-5)
                    });
                }
                
                if (jobSeekersList.Count > 0 && jobPostsList.Count > 2)
                {
                    applications.Add(new Application
                    {
                        JobSeekerId = jobSeekersList[0].Id,
                        JobPostId = jobPostsList[2].Id,
                        Status = ApplicationStatus.Interview,
                        AppliedDate = DateTime.UtcNow.AddDays(-2)
                    });
                }
                
                if (applications.Any())
                {
                    db.Applications.AddRange(applications);
                    await db.SaveChangesAsync();
                    Console.WriteLine($"Added {applications.Count} applications.");
                }
            }
            
            Console.WriteLine("Database seeding completed successfully.");
        }
    }
}