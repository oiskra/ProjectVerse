using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpsertSocialMediaDTO : IIdentifiable
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
    }
}
