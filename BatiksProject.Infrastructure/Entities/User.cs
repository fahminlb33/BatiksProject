using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BatiksProject.Infrastructure.Entities
{
    public class User
    {
        [BsonId] 
        public ObjectId UserId { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
