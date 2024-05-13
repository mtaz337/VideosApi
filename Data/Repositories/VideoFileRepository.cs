using Microsoft.EntityFrameworkCore;
using VideoApi.Data.Entities;

namespace VideoApi.Data.Repositories
{
    public class VideoFileRepository : IVideoFileRepository
    {
        private readonly MyProjectContext _context;

        public VideoFileRepository(MyProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VideoFile>> GetVideoFilesByProductIdAsync(int productId)
        {
            return await _context.VideoFiles.Where(vf => vf.ProductId == productId).ToListAsync();
        }

        public async Task<VideoFile?> GetVideoFileByIdAsync(int videoFileId)
        {
            return await _context.VideoFiles.FindAsync(videoFileId);
        }

        public async Task<bool> AddVideoFileAsync(VideoFile videoFile)
        {
            _context.VideoFiles.Add(videoFile);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveVideoFileAsync(int videoFileId)
        {
            var videoFile = await _context.VideoFiles.FindAsync(videoFileId);
            if (videoFile == null)
                return false;

            _context.VideoFiles.Remove(videoFile);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}