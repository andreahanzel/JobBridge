using System.ComponentModel.DataAnnotations;

namespace JobBridge.Data
{
    public class JobPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Employment type is required")]
        public string EmploymentType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Experience level is required")]
        public string ExperienceLevel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Work arrangement is required")]
        public string WorkArrangement { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public string AdditionalCompensation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job summary is required")]
        public string JobSummary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Key responsibilities are required")]
        public string KeyResponsibilities { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required qualifications are required")]
        public string RequiredQualifications { get; set; } = string.Empty;
        public string PreferredQualifications { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public string NiceToHaveSkills { get; set; } = string.Empty;

        [Required(ErrorMessage = "Application method is required")]
        public string ApplicationMethod { get; set; } = string.Empty;

        [Url(ErrorMessage = "Please enter a valid URL starting with http:// or https://")]
        public string? ExternalApplicationUrl { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime PostExpirationDate { get; set; }
        public bool Featured { get; set; } = false;
        public bool Urgent { get; set; } = false;
        public int NumberOfApplicants { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //foreigns
        public int EmployerId { get; set; }
        public Employers Employer { get; set; } = null!;
        public int FieldId { get; set; }
        public Field Field { get; set; } = null!;
    }
}