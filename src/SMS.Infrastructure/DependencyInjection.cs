using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Aggregates.UserAggregates;
using SMS.Domain.Aggregates.UserRoleAggregates;
using SMS.Infrastructure.Data;
using SMS.Infrastructure.Data.DataSeed;
using SMS.Infrastructure.Data.Interceptors;
using SMS.Infrastructure.Repository;
using System.Reflection;

namespace SMS.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddSingleton<PublishDomainEventsInterceptor>();

        services.AddDbContext<SchoolDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                migrations => migrations.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name))
            .AddInterceptors(
                sp.GetRequiredService<AuditableEntityInterceptor>(),
                sp.GetRequiredService<PublishDomainEventsInterceptor>()
                );
        }).AddTransient<SchoolDbContextSeeder>();

        //services.AddDbContext<SchoolDbContext>((sp, opt) =>
        //{
        //    opt.UseInMemoryDatabase("SMS")
        //    .AddInterceptors(
        //        sp.GetRequiredService<AuditableEntityInterceptor>(),
        //        sp.GetRequiredService<PublishDomainEventsInterceptor>()
        //        );
        //});

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        services.AddIdentity<User, Role>(opt =>
        {
            opt.Password.RequiredLength = 7;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredUniqueChars = 0;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;

        }).AddEntityFrameworkStores<SchoolDbContext>();

        return services;
    }

    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        var schoolDbContextSeeder = scope.ServiceProvider.GetRequiredService<SchoolDbContextSeeder>();

        schoolDbContextSeeder.SeedDataAsync().GetAwaiter().GetResult();

        return builder;
    }
}
