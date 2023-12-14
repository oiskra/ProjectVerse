using projectverseAPI.Data;
using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class UserProfileDataService : IUserProfileDataService
    {
        private readonly ProjectVerseContext _context;

        public UserProfileDataService(
            ProjectVerseContext context)
        {
            _context = context;
        }

        public Task<UserProfileData> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileData> Update(UpdateUserProfileDataRequestDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
