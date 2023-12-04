using SMS.Domain.Primitives;

namespace SMS.Domain.Abstractions;
public interface IGenericRepository<TEntity> where TEntity: Entity, new()
{
    Task<List<TEntity>> GetAll();

    Task<TEntity> GetAsync(Guid id);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
