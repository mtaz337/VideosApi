using Microsoft.AspNetCore.Http;

namespace VideoApi.Services
{
    public interface IVideoFileService
    {
        Task<bool> UploadVideoFileAsync(int productId, IFormFile file);
        Task<bool> DeleteVideoFileAsync(int productId, int videoFileId);
    }
}