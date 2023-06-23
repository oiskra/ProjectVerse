
namespace projectverseAPI.Models
{
    public class ProfileComponent
    {
        public Guid Id { get; set; }
        public Component Component { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentTheme Theme { get; set; }
        public Guid ThemeId { get; set; }
    }
}
