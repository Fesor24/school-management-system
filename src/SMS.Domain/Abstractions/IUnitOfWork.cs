namespace SMS.Domain.Abstractions;
public interface IUnitOfWork : IDisposable
{
    public ICourseRepository CourseRepository { get; init; }
    Task<int> Complete(CancellationToken cancelleationToken = default);
}
