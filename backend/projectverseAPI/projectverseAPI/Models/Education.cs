namespace projectverseAPI.Models
{
    public class Education
    {
        public Guid Id { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public string Course { get; set; }
        public string Major { get; set; }
        public string AcademicTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
