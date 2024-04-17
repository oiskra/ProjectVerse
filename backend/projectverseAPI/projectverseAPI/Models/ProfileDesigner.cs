namespace projectverseAPI.Models
{
    public class ProfileDesigner
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Theme { get; set; }
        public List<ProfileComponent> Components { get; set; }
    }
}
