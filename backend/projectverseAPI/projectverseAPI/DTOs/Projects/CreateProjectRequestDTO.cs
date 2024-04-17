namespace projectverseAPI.DTOs.Projects
{
    public class CreateProjectRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProjectUrl { get; set; }
        public List<string>? UsedTechnologies { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsPublished { get; set; }
    }
}
