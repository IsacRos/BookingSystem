using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BookingSystem.Infrastructure.Data;

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
