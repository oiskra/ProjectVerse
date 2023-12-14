using projectverseAPI.Models;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpdateUserProfileDataRequestDTO
    {
        public Guid Id { get; set; }
        public string AboutMe { get; set; }
        public string Achievements { get; set; }
        public string PrimaryTechnology { get; set; }
        public IList<UserTechnologyStack> KnownTechnologies { get; set; }
        public IList<Interest> Interests { get; set; }
        public IList<Education> Educations { get; set; }
        public IList<Certificate> Certificates { get; set; }
        public IList<SocialMedia> Socials { get; set; }
    }
}
