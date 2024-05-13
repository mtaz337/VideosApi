using Microsoft.AspNetCore.Mvc;
using VideoApi.Data.Repositories;
using VideoApi.Services;

namespace VideoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoFilesController : ControllerBase
    {
        private readonly IVideoFileRepository _videoFileRepository;
        private readonly IVideoFileService _videoFileService;
        private readonly ICacheService _cacheService;

        public VideoFilesController(IVideoFileRepository videoFileRepository, IVideoFileService videoFileService, ICacheService cacheService)
        {
            _videoFileRepository = videoFileRepository;
            _videoFileService = videoFileService;
            _cacheService = cacheService;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> UploadVideoFile(int productId, IFormFile file)
        {
            // Validate the input
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            // Call the service to handle the upload
            var result = await _videoFileService.UploadVideoFileAsync(productId, file);

            if (!result)
                return BadRequest("Failed to upload the video file.");

            // Update the cache
            var videoFiles = await _videoFileRepository.GetVideoFilesByProductIdAsync(productId);
            await _cacheService.CacheVideoFilesAsync(productId, videoFiles);

            return Ok();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetVideoFiles(int productId)
        {
            var cachedVideoFiles = await _cacheService.GetCachedVideoFilesAsync(productId);
            return Ok(cachedVideoFiles);
        }

        [HttpDelete("{productId}/{videoFileId}")]
        public async Task<IActionResult> DeleteVideoFile(int productId, int videoFileId)
        {
            var result = await _videoFileService.DeleteVideoFileAsync(productId, videoFileId);
            if (!result)
                return NotFound("Video file not found.");

            // Update the cache
            var videoFiles = await _videoFileRepository.GetVideoFilesByProductIdAsync(productId);
            await _cacheService.CacheVideoFilesAsync(productId, videoFiles);

            return Ok();
        }
    }
}