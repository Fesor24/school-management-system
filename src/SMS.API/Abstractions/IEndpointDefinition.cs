namespace SMS.API.Abstractions;
internal interface IEndpointDefinition
{
    void RegisterEndpoints(WebApplication app);
}
