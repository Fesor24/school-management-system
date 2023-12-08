namespace SMS.Domain.Primitives;
public interface IGenericRepository<TEntity> where TEntity : Entity, new()
{
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
