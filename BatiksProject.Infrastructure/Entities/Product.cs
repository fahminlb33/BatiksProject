using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BatiksProject.Infrastructure.Entities
{
    public class Product
    {
        [BsonId]
        public ObjectId ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> ImageIds { get; set; }
    }
}
