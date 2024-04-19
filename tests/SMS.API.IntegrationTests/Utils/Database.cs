using Bogus;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Infrastructure.Data;

namespace SMS.API.IntegrationTests.Utils;
internal static class Database
{
    private static readonly Faker<Department> _departmentGenerator = new Faker<Department>()
        .RuleFor(x => x.Id, faker => Guid.NewGuid())
        .RuleFor(x => x.Name, faker => faker.Lorem.Letter(10))
        .RuleFor(x => x.Code, faker => faker.Lorem.Letter(3));

    internal static void Initialize(SchoolDbContext context)
    {
        List<Department> departments = _departmentGenerator.GenerateBetween(2, 6);

        context.Departments.AddRange(departments);

        context.SaveChanges();
    }
}
