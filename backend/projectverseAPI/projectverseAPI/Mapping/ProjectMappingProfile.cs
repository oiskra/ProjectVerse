using projectverseAPI.DTOs.Project;
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

            CreateMap<Project, ProjectResponseDTO>()
                .ForMember(
                    x => x.UserName,
                    opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(
                    x => x.UserId,
                    opt => opt.MapFrom(src => src.UserId));

            CreateMap<Technology, ProjectTechnologyDTO>()
                .ReverseMap();
        }
    }
}
