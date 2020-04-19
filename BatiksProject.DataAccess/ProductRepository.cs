using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatiksProject.Infrastructure.Entities;
using MongoDB.Driver;

namespace BatiksProject.DataAccess
{
    public interface IProductRepository
    {
        Task<Product> Get(string id);
        Task<IEnumerable<Product>> GetAll();
        Task Insert(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private IMongoCollection<Product> _collection;

        public ProductRepository(IMongoDatabase mongo)
        {
            _collection = mongo.GetCollection<Product>(nameof(Product));
        }
        
        public async Task<Product> Get(string id)
        {
            var cursor = await _collection.FindAsync(x => x.ProductId.ToString() == id);
            return await cursor.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var cursor = await _collection.FindAsync(FilterDefinition<Product>.Empty);
            return await cursor.ToListAsync();
        }

        public async Task Insert(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task Update(Product product)
        {
            var updateDef = Builders<Product>.Update
                .Set(x => x.Title, product.Title)
                .Set(x => x.Description, product.Description)
                .Set(x => x.ImageIds, product.ImageIds);
            await _collection.UpdateOneAsync(x => x.ProductId == product.ProductId, updateDef);
        }

        public async Task Delete(Product product)
        {
            await _collection.DeleteOneAsync(x => x.ProductId == product.ProductId);
        }
    }
}
