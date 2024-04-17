using AutoMapper;
using projectverseAPI.DTOs.Designer;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class ProfileDesignerMappingProfile : Profile
    {
        public ProfileDesignerMappingProfile()
        {
            CreateMap<ProfileDesigner, ProfileDesignerResponseDTO>();
            CreateMap<ProfileComponent, ProfileComponentDTO>()
                .ForMember(
                    x => x.ComponentType,
                    opt => opt.MapFrom(src => new ComponentTypeDTO
                    {
                        Category = src.Category,
                        Type = src.Type,
                    }))
                .ForMember(
                    x => x.ColStart,
                    opt => opt.MapFrom(src => src.ColumnStart))
                .ForMember(
                    x => x.ColEnd,
                    opt => opt.MapFrom(src => src.ColumnEnd));

            CreateMap<UpsertProfileComponentDTO, ProfileComponent>()
                .ForMember(
                    x => x.Category,
                    opt => opt.MapFrom(src => src.ComponentType.Category))
                .ForMember(
                    x => x.Type,
                    opt => opt.MapFrom(src => src.ComponentType.Type))
                .ForMember(
                    x => x.ColumnStart,
                    opt => opt.MapFrom(src => src.ColStart))
                .ForMember(
                    x => x.ColumnEnd,
                    opt => opt.MapFrom(src => src.ColEnd));

        }
    }
}
