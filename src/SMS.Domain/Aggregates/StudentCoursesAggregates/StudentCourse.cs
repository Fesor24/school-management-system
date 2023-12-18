using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Aggregates.StudentAggregates;
using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.StudentCoursesAggregates;
public sealed class StudentCourse : AggregateRoot
{
    public StudentCourse()
    {
        
    }

    public StudentCourse(Guid studentId, Guid courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }

    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Course Course { get; private set; }
    public Student Student { get; private set; }
}
