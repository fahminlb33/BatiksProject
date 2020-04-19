using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BatiksProject.Infrastructure;
using BatiksProject.Infrastructure.Entities;
using Minio;
using MongoDB.Driver;

namespace BatiksProject.DataAccess
{
    public interface IImageRepository
    {
        Task<Image> Get(string id);
        Task<IEnumerable<Image>> GetAll();
        Task Insert(Stream stream, List<float> features);
        Task Update(string id, Stream stream, List<float> features);
        Task Delete(Image image);

        string GetPublicUrl(Image image);
    }

    public class ImageRepository : IImageRepository
    {
        private MinioClient _minioClient;
        private BatikConfiguration _config;
        private IMongoCollection<Image> _collection;

        public ImageRepository(BatikConfiguration config, MinioClient minio, IMongoDatabase mongo)
        {
            _config = config;
            _minioClient = minio;
            _collection = mongo.GetCollection<Image>(nameof(Image));
        }

        public async Task<Image> Get(string id)
        {
            var cursor = await _collection.FindAsync(x => x.ImageId.ToString() == id);
            return await cursor.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            var cursor = await _collection.FindAsync(FilterDefinition<Image>.Empty);
            return await cursor.ToListAsync();
        }

        public async Task Insert(Stream stream, List<float> features)
        {
            var image = new Image
            {
                MinioObjectName = $"{new Guid()}.jpg",
                Features = features
            };

            await _minioClient.PutObjectAsync(_config.MinioBucketName, image.MinioObjectName, stream, stream.Length);
            await _collection.InsertOneAsync(image);
        }

        public async Task Update(string id, Stream stream, List<float> features)
        {
            var image = await Get(id);

            await _minioClient.RemoveObjectAsync(_config.MinioBucketName, image.MinioObjectName);
            await _minioClient.PutObjectAsync(_config.MinioBucketName, image.MinioObjectName, stream, stream.Length);

            var updateDef = Builders<Image>.Update
                .Set(x => x.Features, features);
            await _collection.UpdateOneAsync(x => x.ImageId.ToString() == id, updateDef);
        }

        public async Task Delete(Image image)
        {
            await _minioClient.RemoveObjectAsync(_config.MinioBucketName, image.MinioObjectName);
            await _collection.DeleteOneAsync(x => x.ImageId == image.ImageId);
        }

        public string GetPublicUrl(Image image)
        {
            return $"http://{_config.MinioServerHost}/{_config.MinioBucketName}/{image.MinioObjectName}";
        }
    }
}
