using SMS.Domain.Aggregates.StudentCoursesAggregates;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
public sealed class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
{
    public StudentCourseRepository(SchoolDbContext context) : base(context)
    {
        
    }
}
