using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
internal class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
