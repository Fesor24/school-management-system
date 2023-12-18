namespace SMS.Domain.Primitives;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveEntitiesAsync(CancellationToken cancelleationToken = default);
}
