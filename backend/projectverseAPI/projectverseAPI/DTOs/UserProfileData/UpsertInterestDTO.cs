using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpsertInterestDTO : IIdentifiableUpsert
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}
