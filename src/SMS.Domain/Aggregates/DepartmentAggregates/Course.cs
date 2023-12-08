using SMS.Domain.DomainEvents.Course;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
public sealed class Course : Entity
{
    public Course()
    {

    }

    private Course(Guid id, string courseName, string courseCode, int unit) : base(id)
    {
        CourseInfo = new(courseName, courseCode);
        Unit = unit;
    }

    public CourseInfo CourseInfo { get; private set; }

    public int Unit { get; private set; }

    public static Result<Course> Create(Guid id, string courseName, string courseCode, int unit)
    {
        Course course = new(id, courseName, courseCode, unit);

        if (course.Unit > 6)
            return Result.Failure<Course>(DomainErrors.Course.InvalidCourseUnit);

        course.AddDomainEvent(new CourseCreatedEvent(
            course.CourseInfo.Name, 
            course.CourseInfo.Code, 
            course.Unit));

        return course;
    }
}
