using Microsoft.AspNetCore.Builder;

namespace SMS.Presentation.Abstractions;
internal interface IEndpointDefinition
{
    void RegisterEndpoints(WebApplication app);
}
