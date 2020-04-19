using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BatiksProject.Infrastructure.Entities
{
    public class Image
    {
        [BsonId]
        public ObjectId ImageId { get; set; }

        public string MinioObjectName { get; set; }

        public List<float> Features { get; set; }
    }
}
