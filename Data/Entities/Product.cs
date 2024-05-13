using System.ComponentModel.DataAnnotations;

namespace VideoApi.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Add other properties as needed
        public ICollection<VideoFile> VideoFiles { get; set; }
    }
}