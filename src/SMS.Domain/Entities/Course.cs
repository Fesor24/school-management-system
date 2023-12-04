using SMS.Domain.Primitives;

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

    public static Course Create(Guid id, string courseName, string courseCode, int unit)
    {
        Course course = new(id, courseName, courseCode, unit);

        if (course.Unit > 4)
            throw new Exception("Course unit can not be more than 4");

        return course;
    }
}
