using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Designer
{
    public class ProfileDesignerResponseDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Theme { get; set; }
        public List<ProfileComponentDTO> Components { get; set; }
    }
}
