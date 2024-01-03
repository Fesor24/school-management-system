using SMS.Domain.Aggregates.StudentCoursesAggregates;
using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.StudentAggregates;
public sealed class Student : AggregateRoot<Guid>
{
    private readonly List<StudentCourse> _courses = new();

    public Student()
    {
        
    }

    public Student(Guid id, Guid userId, Guid departmentId) : base(id)
    {
        UserId = userId;
        DepartmentId = departmentId;
    }

    public decimal GCPA { get; private set; } = 0m;

    public Guid UserId { get;private set; }

    public Guid DepartmentId { get; private set; }

    public IReadOnlyCollection<StudentCourse> Courses => _courses.AsReadOnly();
}
