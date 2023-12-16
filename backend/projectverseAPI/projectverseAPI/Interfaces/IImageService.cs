using Amazon.S3.Model;

namespace projectverseAPI.Interfaces
{
    public interface IImageService
    {
        Task UploadUsersProfileImage(Guid userId, IFormFile file);
        Task<GetObjectResponse> GetUsersProfileImage(Guid userId);
    }
}
