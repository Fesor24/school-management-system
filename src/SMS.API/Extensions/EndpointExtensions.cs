using SMS.API.Abstractions;
using System.Reflection;

namespace SMS.API.Extensions;
public static class EndpointExtensions
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        var endpointDefinitions = Assembly.GetExecutingAssembly()
            .GetExportedTypes()
            .Where(x => x.IsAssignableTo(typeof(IEndpointDefinition)) && !x.IsAbstract && !x.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpoint in endpointDefinitions)
            endpoint.RegisterEndpoints(app);
    }
}
