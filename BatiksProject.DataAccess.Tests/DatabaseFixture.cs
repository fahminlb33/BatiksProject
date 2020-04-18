using System;
using BatiksProject.DataAccess.Entities;
using MongoDB.Driver;

namespace BatiksProject.DataAccess.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private static Random _random = new Random();
        private string _databaseName;
        private IMongoClient _client;

        public DatabaseFixture()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _databaseName = "test-" + _random.Next();

            Database = _client.GetDatabase(_databaseName);

            Database.CreateCollection(nameof(User));
            Database.CreateCollection(nameof(Product));
            Database.CreateCollection(nameof(Image));
        }

        public IMongoDatabase Database { get; }

        public void Dispose()
        {
            _client.DropDatabase(_databaseName);
        }
    }
}
