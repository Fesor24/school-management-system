namespace SMS.Domain.Abstractions;
public interface IUnitOfWork : IDisposable
{
    Task<int> Complete(CancellationToken cancelleationToken = default);
}
