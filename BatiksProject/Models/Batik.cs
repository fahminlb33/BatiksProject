using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BatiksProject.Models
{
    public class Batik
    {
        [Key]
        public int BatikId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Locality Locality { get; set; }

        public string MinioObjectName { get; set; }

        public List<float> Features { get; set; }
    }
}
