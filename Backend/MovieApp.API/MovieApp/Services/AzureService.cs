using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace MovieApp.Services
{
    public class AzureService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "coverimages";

        public AzureService(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:Azure").Value!;
        }

        public async Task<string?> GetBlobUrlAsync(string blobName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
            {
                return blobClient.Uri.ToString();
            }

            return null;
        }
    }
}