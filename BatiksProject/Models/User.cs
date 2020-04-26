using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BatiksProject.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
