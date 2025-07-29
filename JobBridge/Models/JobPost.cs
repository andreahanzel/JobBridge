namespace JobBridge.Data
{
    public class JobPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int Salary { get; set; }
        public string ApplicationLink { get; set; }
        public int NumberOfApplications { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime PostExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //foreigns
        public int EmployerId { get; set; }
        public Employers Employer { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }

    }
}