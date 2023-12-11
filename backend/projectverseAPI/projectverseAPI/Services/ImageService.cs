using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly ProjectVerseContext _context;

        public ImageService(
            ProjectVerseContext context)
        {
            _context = context;
        }

        public async Task<byte[]> UploadUsersAvatar(Guid usersId, IFormFile file)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                byte[] avatar = await ParseToByteArray(file);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == usersId.ToString());

                if (user is null)
                    throw new ArgumentException("User doesnt exist.");

                user.Avatar = avatar;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return avatar;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<byte[]> ParseToByteArray(IFormFile file)
        {
            byte[] pgmFileContent = null;
            if (file?.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                pgmFileContent = ms.ToArray();
            }

            return pgmFileContent;
        }
    }
}
