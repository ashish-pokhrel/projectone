using System;

namespace oneapp.Services
{
    public class FileServiceStub : IFileService
    {
        public Task DeleteFileAsync(string fileName)
        {
            return null;
        }

        public Task<Stream> DownloadFileAsync(string fileName)
        {
            return null;
        }

        public async Task<string> GetFullUrl(string file)
        {
            return await Task.FromResult("https://picsum.photos/200/300");
        }

        public Task<string> UploadFileAsync(IFormFile file)
        {
            return null;
        }
    }
}

