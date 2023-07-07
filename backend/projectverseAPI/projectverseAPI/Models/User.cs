using Microsoft.AspNetCore.Identity;

namespace projectverseAPI.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Country { get; set; }
    }
}
