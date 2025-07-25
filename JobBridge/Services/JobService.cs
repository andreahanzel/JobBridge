using System.Collections.Generic;
using System.Threading.Tasks;
using JobBridge.Data.Models; // Needed to reference JobListing model
using System.Linq;

namespace JobBridge.Services
{
    public class JobService
    {
        // Constructor (You'll inject your ApplicationDbContext here later for data access)
        // private readonly ApplicationDbContext _dbContext;
        public JobService(/* ApplicationDbContext dbContext */)
        {
            // _dbContext = dbContext;
        }

        // Mock Data (replace with actual database calls later)
        private List<JobListing> _mockJobListings = new List<JobListing>
        {
            new JobListing { Id = 1, Title = "Software Engineer", CompanyName = "Tech Solutions", Location = "Remote", PostedDate = DateTime.UtcNow.AddDays(-5), Description = "Develop awesome software.", EmploymentType = "Full-time", ExperienceLevel = "Mid-level", SalaryRange = "$80,000 - $120,000" },
            new JobListing { Id = 2, Title = "UX Designer", CompanyName = "Creative Co.", Location = "New York, NY", PostedDate = DateTime.UtcNow.AddDays(-10), Description = "Design user-friendly interfaces.", EmploymentType = "Full-time", ExperienceLevel = "Senior", SalaryRange = "$90,000 - $130,000" },
            new JobListing { Id = 3, Title = "Data Analyst", CompanyName = "Data Insights Inc.", Location = "Chicago, IL", PostedDate = DateTime.UtcNow.AddDays(-3), Description = "Analyze complex datasets.", EmploymentType = "Full-time", ExperienceLevel = "Entry-level", SalaryRange = "$60,000 - $80,000" }
        };


        public async Task<List<JobListing>> GetAllJobListingsAsync()
        {
            Console.WriteLine("Getting all job listings...");
            await Task.Delay(100); // Simulate async operation
            return _mockJobListings;
        }

        public async Task<JobListing?> GetJobListingByIdAsync(int id)
        {
            Console.WriteLine($"Getting job listing with ID: {id}");
            await Task.Delay(100); // Simulate async operation
            return _mockJobListings.FirstOrDefault(j => j.Id == id);
        }

        public async Task<JobListing> CreateJobListingAsync(JobListing newJob)
        {
            Console.WriteLine($"Creating new job: {newJob.Title}");
            await Task.Delay(100); // Simulate async operation
            newJob.Id = _mockJobListings.Any() ? _mockJobListings.Max(j => j.Id) + 1 : 1; // Assign a dummy ID
            _mockJobListings.Add(newJob);
            return newJob;
        }

        public async Task<bool> UpdateJobListingAsync(JobListing updatedJob)
        {
            Console.WriteLine($"Updating job: {updatedJob.Title}");
            await Task.Delay(100); // Simulate async operation
            var existingJob = _mockJobListings.FirstOrDefault(j => j.Id == updatedJob.Id);
            if (existingJob != null)
            {
                existingJob.Title = updatedJob.Title;
                existingJob.CompanyName = updatedJob.CompanyName;
                existingJob.Location = updatedJob.Location;
                existingJob.Description = updatedJob.Description;
                existingJob.EmploymentType = updatedJob.EmploymentType;
                existingJob.ExperienceLevel = updatedJob.ExperienceLevel;
                existingJob.SalaryRange = updatedJob.SalaryRange;
                existingJob.IsActive = updatedJob.IsActive;
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteJobListingAsync(int id)
        {
            Console.WriteLine($"Deleting job with ID: {id}");
            await Task.Delay(100); // Simulate async operation
            var jobToRemove = _mockJobListings.FirstOrDefault(j => j.Id == id);
            if (jobToRemove != null)
            {
                _mockJobListings.Remove(jobToRemove);
                return true;
            }
            return false;
        }
    }
}