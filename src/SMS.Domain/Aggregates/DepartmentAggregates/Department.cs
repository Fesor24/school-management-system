using SMS.Domain.DomainEvents.Course;
using SMS.Domain.Errors;
using SMS.Domain.Models.Course;
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

    public static Result<Department, Error> Create(string name, string code, List<CourseModel> courses)
    {
        var department = new Department(Guid.NewGuid(), name, code);

        if (courses.Any())
        {
            foreach(var course in courses)
            {
                var result = department.AddCourse(course.Name, course.Code, course.Unit, department.Id);

                if (result.IsFailure) return result.Error;
            }
        }

        return department;
    }

    public Result<Course, Error> AddCourse(string courseName, string courseCode, int unit, Guid departmentId)
    {
        Result<Course, Error> result = Course.Create(Guid.NewGuid(), courseName, courseCode, 
            unit, departmentId);

        if (result.IsFailure)
            return result.Error;

        _courses.Add(result.Value);

        return result;
    }

    public Result RemoveCourse(Guid courseId)
    {
        Course? course = _courses.FirstOrDefault(x => x.Id == courseId);

        if (course is null)
            return DomainErrors.Course.CourseNotFound(courseId);

        _courses.Remove(course);

        AddDomainEvent(new CourseDeletedEvent(courseId));

        return Result.Success();
    }

    public Result<Course, Error> UpdateCourse(Guid id, string courseName, string courseCode, int unit)
    {
        Course? course = _courses.FirstOrDefault(x => x.Id == id);

        if (course is null)
            return DomainErrors.Course.CourseNotFound(id);

        course.Update(courseName, courseCode, unit);

        return course;
    }

    public Result<Department, Error> UpdateDepartment(string deptName, string deptCode)
    {
        if (string.IsNullOrWhiteSpace(deptName) || string.IsNullOrWhiteSpace(deptCode))
            return DomainErrors.Department.DepartmentBadRequest(deptCode, deptName);

        Name = deptName;
        Code = deptCode;

        return this;
    }
}
