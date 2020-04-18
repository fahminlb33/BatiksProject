using System;
using System.Threading;
using System.Threading.Tasks;
using BatiksProject.DataAccess.Entities;
using BatiksProject.DataAccess.Repositories;
using FakeItEasy;
using MongoDB.Driver;
using Xunit;

namespace BatiksProject.DataAccess.Tests
{
    public class ImageRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _database;
        private IUserRepository _repository;

        public ImageRepositoryTest(DatabaseFixture database)
        {
            _database = database;
            _repository = new UserRepository(database.Database);
        }

        [Fact(DisplayName = "Should throw on get error.")]
        public void GetTestThrow()
        {
            Assert.ThrowsAsync<Exception>(() => _repository.Get(""));
        }

        [Fact(DisplayName = "Should return success")]
        public async Task FindByUsernameTest()
        {
            var db = _database.Database;
            var userCollection = db.GetCollection<User>(nameof(User));
            userCollection.InsertOne(new User
            {
                Username = "fahmi",
                Password = "fahmi"
            });

            var result = await _repository.FindByUsername("fahmi");
            Assert.Equal("fahmi", result.Username);
        }
    }
}
