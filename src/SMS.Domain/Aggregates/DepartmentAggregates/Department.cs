using SMS.Domain.DomainEvents.Course;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
public class Department : AggregateRoot
{
    private readonly List<Course> _courses = new();

    public Department()
    {
        
    }

    public Department(Guid id, string name, string code) : base(id)
    {
        Name = name;
        Code = code;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }

    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public static Department Create(string name, string code)
    {
        var department = new Department(Guid.NewGuid(),name, code);

        return department;
    }

    public Result<Course> AddCourse(string courseName, string courseCode, int unit)
    {
        Shared.Result<Course> result = Course.Create(Guid.NewGuid(), courseName, courseCode, unit);

        if (result.IsFailure)
            return Result.Failure<Course>(result.Error);

        _courses.Add(result.Value);

        return result;
    }

    public Result RemoveCourse(Guid courseId)
    {
        Course? course = _courses.FirstOrDefault(x => x.Id == courseId);

        if (course is null)
            return Result.Failure(DomainErrors.Course.CourseNotFound);

        _courses.Remove(course);

        AddDomainEvent(new CourseDeletedEvent(courseId));

        return Result.Success();
    }

    public Result<Course> UpdateCourse(Guid id, string courseName, string courseCode, int unit)
    {
        Course? course = _courses.FirstOrDefault(x => x.Id == id);

        if (course is null)
            return Result.Failure<Course>(DomainErrors.Course.CourseNotFound);

        course.Update(courseName, courseCode, unit);

        return course;
    }
}
