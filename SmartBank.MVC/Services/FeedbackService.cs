using MongoDB.Driver;
using SmartBank.MVC.Models;
using SmartBank.MVC.MongoDBSettings;

namespace SmartBank.MVC.Services
{
    public class FeedbackService
    {
        private readonly IMongoCollection<Feedback> _collection;

        public FeedbackService(IConfiguration config)
        {
            var settings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<Feedback>(settings.FeedbackCollection);
        }

        public async Task CreateAsync(Feedback feedback)
        {
            await _collection.InsertOneAsync(feedback);
        }
    }
}
