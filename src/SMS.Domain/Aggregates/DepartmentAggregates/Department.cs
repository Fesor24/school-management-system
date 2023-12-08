using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
internal class Department : AggregateRoot
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
}
