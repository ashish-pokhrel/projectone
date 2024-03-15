using System;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace oneapp.Services
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string ContainerName;
        private readonly string SharedAzureBlobKey;

        public FileService(string connectionString, string blobContainerName , string sharedAzureBlobKey)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            ContainerName = blobContainerName;
            SharedAzureBlobKey = sharedAzureBlobKey;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            if (!await blobContainerClient.ExistsAsync())
            {
                await blobContainerClient.CreateAsync();
                await blobContainerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return fileName;
        }

        public async Task<string> GetFullUrl(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient =  blobContainerClient.GetBlobClient(fileName);

            // Check if the blob exists
            bool exists = await blobClient.ExistsAsync();

            // Check if the blob exists
            if (!exists)
            {
                return null; 
            }

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = ContainerName,
                BlobName = blobClient.Name,
                Resource = "c", // "b" for blobs
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobContainerClient.AccountName, SharedAzureBlobKey));

            var imageUrlWithSas = $"{blobClient.Uri}?{sasToken}";

            return await Task.FromResult(imageUrlWithSas);
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException("File not found");
            }

            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteIfExistsAsync();
            }
        }
    }

}
