using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Primitives;
using SMS.Infrastructure.Data;
using SMS.Shared.DependencyRegistrationTypes;

namespace SMS.Infrastructure.Repository;
internal class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository, IScopedRegistration
{
    private readonly SchoolDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public DepartmentRepository(SchoolDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddCourseAsync(Course coure, CancellationToken cancellationToken = default)
    {
        await _context.Courses.AddAsync(coure, cancellationToken);
    }

    public void UpdateCourse(Course coure)
    {
        _context.Courses.Update(coure);
    }

    public async Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default) => 
        await _context.Courses
        .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);

    public async Task<Course?> GetCourseByCodeAsync(string code, CancellationToken cancellationToken = default) =>
        await _context.Courses
        .FirstOrDefaultAsync(x => x.CourseInfo.Code == code);

    public async Task<Department?> GetDepartmentInfo(Guid departmentId, CancellationToken cancellationtoken = default) => 
        await _context.Departments
        .Include(x => x.Courses)
        .FirstOrDefaultAsync(x => x.Id == departmentId, cancellationtoken);
}
