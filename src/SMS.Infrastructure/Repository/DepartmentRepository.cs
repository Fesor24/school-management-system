using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
internal class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default) => 
        await _context.Courses
        .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);

    public async Task<Department?> GetDepartmentInfo(Guid departmentId, CancellationToken cancellationtoken = default) => 
        await _context.Departments
        .Include(x => x.Courses)
        .FirstOrDefaultAsync(x => x.Id == departmentId, cancellationtoken);
}
