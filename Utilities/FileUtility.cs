using Microsoft.AspNetCore.Http;
using System.IO;

namespace VideoApi.Utilities
{
    public static class FileUtility
    {
        private static readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public static async Task<string> SaveFileAsync(IFormFile file)
        {
            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}