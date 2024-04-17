using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Designer
{
    public class UpsertProfileComponentDTO
    {
        public Guid Id { get; set; }
        public ComponentTypeDTO ComponentType { get; set; }
        public bool? isNew { get; set; }
        public int ColStart { get; set; }
        public int ColEnd { get; set; }
        public int RowStart { get; set; }
        public int RowEnd { get; set; }
        public string? Data { get; set; } //JSON
    }
}
