using VideoApi.Data.Entities;

namespace VideoApi.Services
{
    public interface ICacheService
    {
        Task CacheVideoFilesAsync(int productId, IEnumerable<VideoFile> videoFiles);
        Task<IEnumerable<VideoFile>> GetCachedVideoFilesAsync(int productId);
    }
}