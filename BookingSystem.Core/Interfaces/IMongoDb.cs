using BookingSystem.Core.Entities;
using System.Linq.Expressions;

namespace BookingSystem.Core.Interfaces;

public interface IMongoDb<TEntity> where TEntity : EntityBase
{
    Task Insert(TEntity entity);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(string id);
    Task Update(TEntity entity);
    Task Delete(string id);
    Task<IEnumerable<TEntity>> SearchFor<TField>(string fieldName, TField fieldValue);
    Task<IEnumerable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> predicate);

}
