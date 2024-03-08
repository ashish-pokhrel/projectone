using System;
namespace oneapp.Services
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<Stream> DownloadFileAsync(string fileName);
        Task<string> GetFullUrl(string file);
        Task DeleteFileAsync(string fileName);
    }
}

