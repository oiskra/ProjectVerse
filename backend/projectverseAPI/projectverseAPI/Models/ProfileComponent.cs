
namespace projectverseAPI.Models
{
    public class ProfileComponent
    {
        public Guid Id { get; set; }
        public ComponentType Component { get; set; }
        public Guid ComponentId { get; set; }
        public ComponentTheme Theme { get; set; }
        public Guid ThemeId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
