using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookingSystem.Server.Data
{
    public class MongoConnection
    {
        public readonly IMongoDatabase database;
        public MongoConnection(IConfiguration config)
        {
            var settings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>()!;
            var client = new MongoClient(settings.AtlasURI);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
