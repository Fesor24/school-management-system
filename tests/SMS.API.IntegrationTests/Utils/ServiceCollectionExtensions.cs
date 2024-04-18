using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMS.Infrastructure.Data;

namespace SMS.API.IntegrationTests.Utils;
public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<TContext>(this IServiceCollection services)
    {
        var dbDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<SchoolDbContext>));

        if(dbDescriptor is not null)
            services.Remove(dbDescriptor);
    }

    public static void DbContextInit<TContext>(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

        context.Database.Migrate();
    }
}
