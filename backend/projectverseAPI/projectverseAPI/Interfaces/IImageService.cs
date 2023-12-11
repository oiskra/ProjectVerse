namespace projectverseAPI.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> UploadUsersAvatar(IFormFile file);
    }
}
