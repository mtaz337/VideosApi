using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using VideoApi.Data.Entities;
using VideoApi.Data.Repositories;
using VideoApi.Utilities;

namespace VideoApi.Services
{
    public class VideoFileService : IVideoFileService
    {
        private readonly IVideoFileRepository _videoFileRepository;
        private readonly IDistributedCache _distributedCache;

        public VideoFileService(IVideoFileRepository videoFileRepository, IDistributedCache distributedCache)
        {
            _videoFileRepository = videoFileRepository;
            _distributedCache = distributedCache;
        }

        public async Task<bool> UploadVideoFileAsync(int productId, IFormFile file)
        {
            // Validate the input
            if (file == null || file.Length == 0)
                return false;

            // Save the file to a designated location
            var filePath = await FileUtility.SaveFileAsync(file);

            // Create a new VideoFile entity
            var videoFile = new VideoFile
            {
                ProductId = productId,
                FileName = file.FileName,
                FilePath = filePath
            };

            // Add the video file to the database
            var result = await _videoFileRepository.AddVideoFileAsync(videoFile);

            // Update the cache
            if (result)
                await UpdateCacheAsync(productId);

            return result;
        }

        public async Task<bool> DeleteVideoFileAsync(int productId, int videoFileId)
        {
            // Get the video file from the repository
            var videoFile = await _videoFileRepository.GetVideoFileByIdAsync(videoFileId);
            if (videoFile == null)
                return false;

            // Delete the physical file
            FileUtility.DeleteFile(videoFile.FilePath);

            // Remove the video file from the database
            var result = await _videoFileRepository.RemoveVideoFileAsync(videoFileId);

            // Update the cache
            if (result)
                await UpdateCacheAsync(productId);

            return result;
        }

        private async Task UpdateCacheAsync(int productId)
{
    // Get the video files for the product from the database
    var videoFiles = await _videoFileRepository.GetVideoFilesByProductIdAsync(productId);

    // Serialize the video files to byte array
    var serializedVideoFiles = JsonSerializer.SerializeToUtf8Bytes(videoFiles);

    // Specify caching options (e.g., expiration time)
    var cacheOptions = new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Set expiration time (e.g., 5 minutes)
    };

    // Store the video files in the cache
    await _distributedCache.SetAsync($"VideoFiles_{productId}", serializedVideoFiles, cacheOptions);
}

    }
}