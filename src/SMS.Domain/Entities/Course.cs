using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Entities;
public sealed class Course : AggregateRoot
{
    public Course()
    {
        
    }

    private Course(Guid id, string courseName, string courseCode, int unit) : base(id)
    {
        Name = courseName;
        Code = courseCode;
        Unit = unit;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Unit { get; private set; }

    public static Result<Course> Create(Guid id, string courseName, string courseCode, int unit)
    {
        Course course = new(id, courseName, courseCode, unit);

        if (course.Unit > 6)
            throw new Exception("Course unit can not be more than 6");

        return course;
    }
}
