using FluentAssertions;
using FluentAssertions.Execution;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;
using CourseEntity = SMS.Domain.Aggregates.DepartmentAggregates.Course;

namespace SMS.Domain.UnitTests.Course;
public class CourseTests
{
    [Fact]
    public void TwoCourseInfoInstance_Equal_IfTheyContainSameValues()
    {
        CourseInfo info1 = new("Math101", "MTH");
        CourseInfo info2 = new("Math101", "MTH");

        bool res = info1 == info2;

        res.Should().BeTrue();
    }

    [Fact]
    public void TwoCourseInfoInstance_NotEqual_IfTheyContainDifferentValues()
    {
        CourseInfo info1 = new("Math101", "MTH");
        CourseInfo info2 = new("ENG101", "MTH");

        bool res = info1 == info2;

        res.Should().BeFalse();
    }

    [Fact]
    public void TwoCourseEntityInstance_Equal_IfTheyContainSameValues()
    {
        Guid identity = Guid.NewGuid();

        Guid departmentId = Guid.NewGuid();

        Result<CourseEntity, Error> course1 = CourseEntity.Create(identity, "Company Law", "CIL502", 4, departmentId);
        Result<CourseEntity, Error> course2 = CourseEntity.Create(identity, "Company Law", "CIL502", 4, departmentId);

        course1.IsSuccess.Should().BeTrue();
        course2.IsSuccess.Should().BeTrue();

        bool res = course1.Value!.Equals(course2.Value);

        res.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(7)]
    public void CreateCourse_WithInvalidUnit_ReturnInvalidCourseUnitError(int courseUnit)
    {
        Result<CourseEntity, Error> res = CourseEntity.Create(Guid.NewGuid(), "Advanced Maths", "MTH101", 
            courseUnit, Guid.NewGuid());

        using var _ = new AssertionScope();

        res.IsFailure.Should().BeTrue();
        res.Error.Should().Be(DomainErrors.Course.InvalidCourseUnit);
    }
}
