using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BatiksProject.Models
{
    public class Locality
    {
        [Key]
        public string Name { get; set; }

        public ICollection<Batik> Batiks { get; set; }
    }
}
