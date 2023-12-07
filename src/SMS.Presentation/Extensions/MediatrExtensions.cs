using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SMS.Presentation.Extensions;
public static class MediatrExtensions
{
    public static void MediatorGet<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route) where TRequest: IRequest<TResponse>
    {
        var group = app.MapGroup(endpointGroup);

        group.MapGet(route, EndpointWithParametersAttribute<TRequest, TResponse>);
    }

    public static void MediatorPost<TRequest, TResponse>(this WebApplication app, string endpointGroup,
        string route) where TRequest : IRequest<TResponse>
    {
        var group = app.MapGroup(endpointGroup);

        group.MapPost(route, EndpointWithoutParametersAttribute<TRequest, TResponse>);
    }

    internal static async Task<IResult> EndpointWithParametersAttribute<TRequest, TResponse>([AsParameters] TRequest request, 
        ISender sender, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        var response = await sender.Send(request, cancellationToken);

        return Results.Ok(response);
    }

    internal static async Task<IResult> EndpointWithoutParametersAttribute<TRequest, TResponse>(TRequest request, 
        IMediator mediator, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        var response = await mediator.Send(request, cancellationToken);

        return Results.Ok(response);
    }
}
