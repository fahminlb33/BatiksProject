using System.ComponentModel.DataAnnotations;

namespace BatiksProject.Models
{
    public class Batik
    {
        [Key]
        public int BatikId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public Locality Locality { get; set; }

        [Required]
        public string MinioObjectName { get; set; }

        [Required]
        public byte[] Features { get; set; }
    }
}
