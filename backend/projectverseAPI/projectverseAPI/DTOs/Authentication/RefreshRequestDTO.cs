namespace projectverseAPI.DTOs.Authentication
{
    public class RefreshRequestDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
