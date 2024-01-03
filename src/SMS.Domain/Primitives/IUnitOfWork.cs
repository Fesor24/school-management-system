namespace SMS.Domain.Primitives;
public interface IUnitOfWork
{
    Task<int> SaveEntitiesAsync(CancellationToken cancelleationToken = default);
}
