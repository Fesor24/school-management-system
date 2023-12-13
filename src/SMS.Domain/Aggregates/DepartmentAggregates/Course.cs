using SMS.Domain.DomainEvents.Course;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
public sealed class Course : BaseAuditableEntity
{
    protected Course()
    {

    }

    private Course(Guid id, string courseName, string courseCode, int unit) : base(id)
    {
        CourseInfo = new(courseName, courseCode);
        Unit = unit;
    }

    public CourseInfo CourseInfo { get; private set; }

    public int Unit { get; private set; }

    public static Result<Course, Error> Create(Guid id, string courseName, 
        string courseCode, int unit)
    {
        Course course = new(id, courseName, courseCode, unit);

        if (course.Unit < 1 || course.Unit > 6)
            return DomainErrors.Course.InvalidCourseUnit;

        course.AddDomainEvent(new CourseCreatedEvent(
            course.CourseInfo.Name,
            course.CourseInfo.Code,
            course.Unit));

        return course;
    }

    public Result<Course, Error> Update(string courseName, string courseCode, int unit)
    {
        if (unit < 1 || unit > 6)
            return DomainErrors.Course.InvalidCourseUnit;

        Unit = unit;
        CourseInfo = new(courseName, courseCode);

        return this;
    }
}
