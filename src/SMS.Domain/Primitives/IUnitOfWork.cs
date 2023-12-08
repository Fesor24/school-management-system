using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Domain.Primitives;
public interface IUnitOfWork : IDisposable
{
    public ICourseRepository CourseRepository { get; init; }
    Task<int> Complete(CancellationToken cancelleationToken = default);
}
