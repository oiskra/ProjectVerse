using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Project
{
    public class ProjectResponseDTO
    {
        public Guid Id { get; set; }
        public ProjectAuthorDTO Author { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public IList<ProjectTechnologyDTO> UsedTechnologies { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsPublished { get; set; }
    }
}
