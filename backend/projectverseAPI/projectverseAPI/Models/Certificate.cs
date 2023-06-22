namespace projectverseAPI.Models
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
