using projectverseAPI.DTOs.Project;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjects();
        Task<List<Project>> GetAllProjectsByUserID(Guid userId);
        Task<Project?> GetProjectById(Guid projectId);
        Task<Guid> CreateProject(CreateProjectRequestDTO project);
        Task UpdateProject(UpdateProjectRequestDTO project);
        Task<bool> DeleteProject(Guid projectId);
    }
}
