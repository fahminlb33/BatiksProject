using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BatiksProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
    }
}
