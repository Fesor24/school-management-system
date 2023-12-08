﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Domain.Primitives;
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

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                migrations => migrations.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name))
            .AddInterceptors(
                sp.GetRequiredService<AuditableEntityInterceptor>(),
                sp.GetRequiredService<PublishDomainEventsInterceptor>()
                );
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
