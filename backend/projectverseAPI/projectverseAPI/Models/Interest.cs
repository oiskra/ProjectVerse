using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.Models
{

    public class Interest : IIdentifiable
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
