using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Aggregates.UserAggregates;
using SMS.Domain.Aggregates.UserRoleAggregates;
using SMS.Domain.Aggregates.UserRoleClalimAggregates;
using SMS.Domain.Primitives;
using System.Reflection;

namespace SMS.Infrastructure.Data;
public class SchoolDbContext : IdentityDbContext<User, UserRole, Guid, IdentityUserClaim<Guid>, 
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, UserRoleClaim, IdentityUserToken<Guid>>, 
    IUnitOfWork
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

    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
