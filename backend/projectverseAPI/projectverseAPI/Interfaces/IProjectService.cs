﻿using projectverseAPI.DTOs.Projects;
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
    { }
}
