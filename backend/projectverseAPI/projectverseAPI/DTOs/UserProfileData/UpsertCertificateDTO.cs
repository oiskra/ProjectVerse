using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.DTOs.UserProfileData
{
    public class UpsertCertificateDTO : IIdentifiable
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
