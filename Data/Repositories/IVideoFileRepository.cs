using VideoApi.Data.Entities;

namespace VideoApi.Data.Repositories
{
    
    public interface IVideoFileRepository
    {
        Task<IEnumerable<VideoFile>> GetVideoFilesByProductIdAsync(int productId);
        Task<VideoFile?> GetVideoFileByIdAsync(int videoFileId);
        Task<bool> AddVideoFileAsync(VideoFile videoFile);
        Task<bool> RemoveVideoFileAsync(int videoFileId);
        // Add other methods as needed
    }
}