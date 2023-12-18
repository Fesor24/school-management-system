using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Infrastructure.Data;
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
        });

        //services.AddDbContext<SchoolDbContext>((sp, opt) =>
        //{
        //    opt.UseInMemoryDatabase("SMS")
        //    .AddInterceptors(
        //        sp.GetRequiredService<AuditableEntityInterceptor>(),
        //        sp.GetRequiredService<PublishDomainEventsInterceptor>()
        //        );
        //});

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}
