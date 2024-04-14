using H1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace H1.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<User> _booksCollection;

        public MongoService(
            IOptions<HealthBuddyDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<User>(
                bookStoreDatabaseSettings.Value.HealthBuddyCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, User updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
