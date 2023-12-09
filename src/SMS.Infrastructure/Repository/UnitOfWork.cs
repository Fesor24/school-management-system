using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Primitives;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        DepartmentRepository = new DepartmentRepository(context);
    }

    public IDepartmentRepository DepartmentRepository { get; init; }

    public async Task<int> Complete(CancellationToken cancelleationToken = default)
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose() =>
        _context.Dispose();
}
