using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpsertInterestDTO : IIdentifiable
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}
