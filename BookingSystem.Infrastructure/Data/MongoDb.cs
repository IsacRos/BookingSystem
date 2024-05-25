using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BookingSystem.Infrastructure.Data;

public class MongoDb<TEntity> : IMongoDb<TEntity> where TEntity : EntityBase
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
        var entities = await collection.Find(_ => true).ToListAsync();
        return entities;
    }
    public async Task<TEntity> GetById(string id)
    {
        var entity = await collection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        return entity;
    }
    public async Task Update(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", entity.Id);
        await collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
    }
    public async Task Delete(string id)
    {
        var entity = await collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        if (entity != null)
        {
            await collection.DeleteOneAsync(x => x.Id == entity.Id);
        }
        else throw new ArgumentException("Couldn't find entity to delete");
    }
    public async Task<IEnumerable<TEntity>> SearchFor<TField>(string fieldName, TField fieldValue)
    {
        var filter = Builders<TEntity>.Filter.Eq<TField>(fieldName, fieldValue);
        var result = await collection.Find(filter).ToListAsync();
        return result;
    }
    public async Task<IEnumerable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> predicate)
    {
        var queryable = collection.AsQueryable<TEntity>();
        var result = await Task.Run(() => queryable.Where(predicate).ToList());
        return result;
    }
}
