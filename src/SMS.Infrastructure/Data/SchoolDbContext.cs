using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Primitives;
using System.Reflection;

namespace SMS.Infrastructure.Data;
public class SchoolDbContext : DbContext, IUnitOfWork
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
        
    }

    internal const string DEFAULT_SCHEMA = "sms";
    internal const string USER_SCHEMA = "usr";
    internal const string STUDENT_SCHEMA = "std";

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveEntitiesAsync(CancellationToken cancelleationToken = default)
    {
        return await base.SaveChangesAsync();
    }
}
