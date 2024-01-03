using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Aggregates.StudentCoursesAggregates;
using SMS.Domain.Aggregates.UserAggregates;
using SMS.Domain.Aggregates.UserRoleAggregates;
using SMS.Domain.Aggregates.UserRoleClalimAggregates;
using SMS.Domain.Primitives;
using System.Reflection;

namespace SMS.Infrastructure.Data;
public class SchoolDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, 
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, UserRoleClaim, IdentityUserToken<Guid>>, 
    IUnitOfWork
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
        
    }

    #region Schemas

    internal const string DEFAULT_SCHEMA = "sms";
    internal const string USER_SCHEMA = "usr";
    internal const string STUDENT_SCHEMA = "std";

    #endregion

    #region Entities
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
