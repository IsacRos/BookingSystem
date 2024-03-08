using BookingSystem.Server.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BookingSystem.Server.Data
{
    public class MongoDb<TEntity> where TEntity : EntityBase
    {
        private readonly IMongoCollection<TEntity> collection;
        public MongoDb(MongoConnection database)
        {
            collection = database.database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Insert(TEntity entity)
        {
            await collection.InsertOneAsync(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var restaurants =  await collection.Find(_ => true).ToListAsync();
            return restaurants;
        }
        public async Task<TEntity> GetById(string id)
        {
            var restaurant = await collection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
            return restaurant; 
        }
        /*        public async Task Update(TEntity entity)
                {
                    var filter = Builders<TEntity>.Filter.Eq("Id", entity.Id);
                    await collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
                }*/
        public async Task Delete(string id)
        {
            var restaurant = await collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            if (restaurant != null)
            {
                await collection.DeleteOneAsync(x => x.Id == restaurant.Id);
            }
            else throw new ArgumentException("Couldn't find restaurant to delete");
        }
        public async Task<IList<TEntity>> SearchFor(string key, string value)
        {
            
            var restaurants = await collection.Find(predicate).ToListAsync();
            return restaurants;
        }

    }
}
