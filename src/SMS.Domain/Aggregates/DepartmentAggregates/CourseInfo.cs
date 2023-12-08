using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.DepartmentAggregates;
public class CourseInfo : ValueObject
{
    public CourseInfo(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Code;
    }
}
