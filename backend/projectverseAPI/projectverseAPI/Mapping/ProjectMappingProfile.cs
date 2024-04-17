using projectverseAPI.DTOs.Projects;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class ProjectMappingProfile : AutoMapper.Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectRequestDTO, Project>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    x => x.UsedTechnologies,
                    opt => opt.MapFrom(src =>
                        src.UsedTechnologies.Select(tech => new Technology { Id = Guid.NewGuid(), Name = tech })));

            CreateMap<UpdateProjectRequestDTO, Project>();

            CreateMap<Project, ProjectResponseDTO>();
            CreateMap<User, ProjectAuthorDTO>();

            CreateMap<Technology, ProjectTechnologyDTO>()
                .ReverseMap();
        }
    }
}
