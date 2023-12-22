using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.Models
{
    public class Technology
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UserTechnologyStack : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LevelOfAdvancement Level { get; set; }
    }

    public enum LevelOfAdvancement
    {
        Entry,
        Junior,
        Mid,
        Senior
    }
}
