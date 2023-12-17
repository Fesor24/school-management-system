using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SMS.Application.Common.Behavior;
using System.Reflection;

namespace SMS.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            //config.AddBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
