using projectverseAPI.Models;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpdateUserProfileDataRequestDTO
    {
        public Guid? Id { get; set; }
        public string? AboutMe { get; set; }
        public string? Achievements { get; set; }
        public string? PrimaryTechnology { get; set; }
        public IList<UpsertUserTechnologyStackDTO>? KnownTechnologies { get; set; }
        public IList<UpsertInterestDTO>? Interests { get; set; }
        public IList<UpsertEducationDTO>? Educations { get; set; }
        public IList<UpsertCertificateDTO>? Certificates { get; set; }
        public IList<UpsertSocialMediaDTO>? Socials { get; set; }
    }
}
