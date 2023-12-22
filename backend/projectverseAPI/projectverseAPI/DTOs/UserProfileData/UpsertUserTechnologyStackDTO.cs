using projectverseAPI.Interfaces.Marker;
using projectverseAPI.Models;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpsertUserTechnologyStackDTO : IIdentifiableUpsert
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public LevelOfAdvancement? Level { get; set; }
    }
}
