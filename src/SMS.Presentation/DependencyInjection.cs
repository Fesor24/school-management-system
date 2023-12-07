using Microsoft.Extensions.DependencyInjection;

namespace SMS.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        //services.AddMediatR(config =>
        //{
        //    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        //});

        return services;
    }
}
