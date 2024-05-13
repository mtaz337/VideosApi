 using System.ComponentModel.DataAnnotations;
 namespace VideoApi.Data.Entities
{
public class VideoFile
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required] 
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
    }