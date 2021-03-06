﻿using System.ComponentModel.DataAnnotations;

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
        public string UploadName { get; set; }

        [Required]
        public float[] Features { get; set; }
    }
}
