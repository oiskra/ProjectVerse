using projectverseAPI.DTOs.Project;
using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Post
{
    public class PostProjectDTO
    {
        public Guid Id { get; set; }
        public ProjectAuthorDTO Author { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public IList<ProjectTechnologyDTO> UsedTechnologies { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
