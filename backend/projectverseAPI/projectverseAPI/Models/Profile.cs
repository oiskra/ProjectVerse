namespace projectverseAPI.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string AboutMe { get; set; }
        public string Achievements { get; set; }
        public string PrimaryTechnology { get; set; }
        public string SecondaryTechnology { get; set; }
        public IList<string> KnownTechnologies { get; set; }
        public IList<string> Interests { get; set; }
        public IList<Education> Educations { get; set; }
        public IList<Certificate> Certificates { get; set; }
    }
}
