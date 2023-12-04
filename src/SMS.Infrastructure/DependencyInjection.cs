using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Infrastructure.Data;
using SMS.Infrastructure.Data.Interceptors;

namespace SMS.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddSingleton<PublishDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .AddInterceptors(
                sp.GetRequiredService<AuditableEntityInterceptor>(),
                sp.GetRequiredService<PublishDomainEventsInterceptor>()
                );
        });

        return services;
    }
}
