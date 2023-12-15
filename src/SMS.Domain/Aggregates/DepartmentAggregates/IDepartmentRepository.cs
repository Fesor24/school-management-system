using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
public interface IDepartmentRepository : IGenericRepository<Department>
{
    Task<Department?> GetDepartmentInfo(Guid departmentId, CancellationToken cancellationtoken = default);

    Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default);

    Task AddCourseAsync(Course coure, CancellationToken cancellationToken = default);
}
