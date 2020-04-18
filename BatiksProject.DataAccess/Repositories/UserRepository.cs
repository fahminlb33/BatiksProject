using System.Collections.Generic;
using System.Threading.Tasks;
using BatiksProject.DataAccess.Entities;
using MongoDB.Driver;

namespace BatiksProject.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string id);
        Task<User> FindByUsername(string username);
        Task<IEnumerable<User>> GetAll();
        Task Insert(User user);
        Task Update(User user);
        Task Delete(User user);
    }

    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase mongo)
        {
            _collection = mongo.GetCollection<User>(nameof(User));
        }

        public async Task<User> Get(string id)
        {
            var cursor = await _collection.FindAsync(x => x.UserId.ToString() == id);
            return await cursor.SingleOrDefaultAsync();
        }

        public async Task<User> FindByUsername(string username)
        {
            var cursor = await _collection.FindAsync(x => x.Username == username);
            return await cursor.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var cursor = await _collection.FindAsync(FilterDefinition<User>.Empty);
            return await cursor.ToListAsync();
        }

        public async Task Insert(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task Update(User user)
        {
            var updateDef = Builders<User>.Update
                .Set(x => x.Username, user.Username)
                .Set(x => x.Password, user.Password);
            await _collection.UpdateOneAsync(x => x.UserId.ToString() == user.UserId.ToString(), updateDef);
        }

        public async Task Delete(User user)
        {
            await _collection.DeleteOneAsync(x => x.UserId.ToString() == user.UserId.ToString());
        }
    }
}
