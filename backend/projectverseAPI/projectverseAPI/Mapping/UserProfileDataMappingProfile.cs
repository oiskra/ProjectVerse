using AutoMapper;
using projectverseAPI.DTOs.User;
using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Models;

namespace projectverseAPI.Mapping
{
    public class UserProfileDataMappingProfile : Profile
    {
        public UserProfileDataMappingProfile()
        {
            CreateMap<UserProfileData, UserProfileDataResponseDTO>();
        }
    }
}
