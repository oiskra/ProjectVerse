using Microsoft.AspNetCore.Identity;

namespace projectverseAPI.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Country { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public byte[]? Avatar { get; set; } 
    }
}
