using projectverseAPI.DTOs.Authentication;
using projectverseAPI.DTOs.User;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterDTO, User>()
                .ForMember(
                    x => x.Id,
                    cfg => cfg.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(
                    x => x.SecurityStamp,
                    cfg => cfg.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<User, UserResponseDTO>();
        }
    }
}
