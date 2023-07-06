using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class CollaborationMappingProfile : AutoMapper.Profile
    {
        public CollaborationMappingProfile()
        {
            //CreateCollab
            CreateMap<CreateCollaborationDTO, Collaboration>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<CreateCollaborationPositionDTO, CollaborationPosition>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()));

            //Update
            CreateMap<UpdateCollaborationDTO, Collaboration>();

            //GetCollab
            CreateMap<Collaboration, CollaborationDTO>();
            CreateMap<User, CollaborationAuthorDTO>();

        }
    }
}
