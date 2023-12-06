using projectverseAPI.DTOs.Project;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IProjectService : 
        IGetAll<Project>,
        IGetAllByUserId<Project>,
        IGetById<Project>,
        ICreate<CreateProjectRequestDTO, Project>,
        IUpdate<UpdateProjectRequestDTO, Project>,
        IDelete
    {
       /* Task<Guid> CreateProject(CreateProjectRequestDTO project);
        Task UpdateProject(UpdateProjectRequestDTO project);
        Task<bool> DeleteProject(Guid projectId);*/
    }
}
