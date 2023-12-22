using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Designer
{
    public class UpdateProfileDesignerRequestDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Theme { get; set; }
        public List<UpsertProfileComponentDTO> Components { get; set; }
    }
}
