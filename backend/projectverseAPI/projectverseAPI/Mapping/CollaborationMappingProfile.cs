using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class CollaborationMappingProfile : AutoMapper.Profile
    {
        public CollaborationMappingProfile()
        {
            //CreateCollab
            CreateMap<CreateCollaborationRequestDTO, Collaboration>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    x => x.PeopleInvolved,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    x => x.Technologies,
                    opt => opt.MapFrom(src => 
                        src.Technologies.Select(tech => new Technology { Id = Guid.NewGuid(), Name = tech })));

            CreateMap<CreateCollaborationPositionDTO, CollaborationPosition>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()));

            //Update
            CreateMap<UpdateCollaborationRequestDTO, Collaboration>();
            CreateMap<UpdateCollaborationPositionDTO, CollaborationPosition>();

            //GetCollab
            CreateMap<Collaboration, CollaborationResponseDTO>();
            CreateMap<CollaborationPosition, CollaborationPositionDTO>();
            CreateMap<User, CollaborationAuthorDTO>();
            CreateMap<Collaboration, SingleCollaborationWithApplicantsResponseDTO>();

            //Applicants
            CreateMap<CollaborationApplicant, CollaborationApplicantDTO>()
                .ForMember(
                    x => x.ApplicantEmail,
                    opt => opt.MapFrom(src => src.ApplicantUser.Email))
                .ForMember(
                    x => x.ApplicantUserName,
                    opt => opt.MapFrom(src => src.ApplicantUser.UserName));
        }
    }
}
