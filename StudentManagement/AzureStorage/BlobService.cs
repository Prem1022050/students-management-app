using Azure.Storage.Blobs;

namespace StudentManagement.AzureStorage
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobService(IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("storageConnection");

            _containerName = "student-images";

            _blobServiceClient =
                new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var containerClient =
                _blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();

            var fileName = Guid.NewGuid().ToString() +
                           Path.GetExtension(file.FileName);

            var blobClient =
                containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(stream, true);

            return fileName;
        }

        public async Task<(Stream Stream, string ContentType)?> GetFileAsync(
    string fileName)
        {
            var containerClient =
                _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobClient =
                containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                return null;
            }

            var response =
                await blobClient.DownloadStreamingAsync();

            return (
                response.Value.Content,
                response.Value.Details.ContentType
            );
        }
    }
}
