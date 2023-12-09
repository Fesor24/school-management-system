using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Domain.Primitives;
public interface IUnitOfWork : IDisposable
{
    public IDepartmentRepository DepartmentRepository { get; init; }
    Task<int> Complete(CancellationToken cancelleationToken = default);
}
