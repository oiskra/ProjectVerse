using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.User;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectVerseContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(
            ProjectVerseContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
                
            if (user is null)
                throw new ArgumentException("User doesn't exist.");

            return user;
        }

        public async Task<User> Update(UpdateUserRequestDTO userDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var userToUpdate = await _userManager.FindByIdAsync(userDTO.Id.ToString());

                if (userToUpdate is null)
                    throw new ArgumentException("User doesn't exist.");

                userToUpdate.UserName = userDTO.UserName;
                userToUpdate.Email = userDTO.Email;
                userToUpdate.Name = userDTO.Name;
                userToUpdate.Surname = userDTO.Surname;
                userToUpdate.Country = userDTO.Country;

                await _userManager.UpdateAsync(userToUpdate);
                
                await transaction.CommitAsync();

                return userToUpdate;
            }
            catch (ArgumentException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            
        }
    }
}
