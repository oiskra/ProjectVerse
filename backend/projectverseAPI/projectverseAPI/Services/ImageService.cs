using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        public ImageService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GetObjectResponse> GetUsersProfileImage(Guid userId)
        {
            var credentials = new BasicAWSCredentials(
                _configuration.GetSection("S3")["accessKey"],
                _configuration.GetSection("S3")["secret"]);

            try
            {
                using var client = new AmazonS3Client(credentials, RegionEndpoint.EUCentral1);

                var profileImage = await client.GetObjectAsync(
                    _configuration.GetSection("S3")["bucketName"],
                    $"profile_images/{userId}");

                return profileImage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UploadUsersProfileImage(Guid userId, IFormFile file)
        {
            var credentials = new BasicAWSCredentials(
                _configuration.GetSection("S3")["accessKey"],
                _configuration.GetSection("S3")["secret"]);
            
            try
            {
                var putObjectRequest = new TransferUtilityUploadRequest()
                {
                    BucketName = _configuration.GetSection("S3")["bucketName"],
                    Key = $"profile_images/{userId}",
                    ContentType = file.ContentType,
                    InputStream = file.OpenReadStream(),
                    CannedACL = S3CannedACL.NoACL
                };
               
                using var client = new AmazonS3Client(credentials, RegionEndpoint.EUCentral1);

                var transferUtility = new TransferUtility(client);

                await transferUtility.UploadAsync(putObjectRequest);
            }
            catch (Exception)
            {
                throw;
            }

            
        }
    }
}
