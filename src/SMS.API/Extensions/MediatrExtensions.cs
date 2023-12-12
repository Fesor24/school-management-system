using MediatR;
using SMS.API.Common.EndpointResponseMap;
using SMS.Application.Common.Response;
using SMS.Domain.Shared;

namespace SMS.API.Extensions;
public static class MediatrExtensions
{
    public static void MediatorGet<TRequest, TResponse, TValue, TError>(this WebApplication app, string endpointGroup, 
        string route, string routeName = "") 
        where TRequest: IRequest<TResponse> 
        where TResponse : Result<TValue, TError> 
        where TError : Error
    {
        var group = app.MapGroup(endpointGroup)
            .WithName(routeName);

        group.MapGet(route, EndpointWithParametersAttribute<TRequest, TResponse, TValue, TError>);
    }

    public static void MediatorPost<TRequest, TResponse, TValue, TError>(this WebApplication app, string endpointGroup,
        string route) 
        where TRequest : IRequest<TResponse>
        where TResponse : Result<TValue, TError>
        where TError : Error
        where TValue : CreateResponse
    {
        var group = app.MapGroup(endpointGroup);

        group.MapPost(route, EndpointWithoutParametersAttribute<TRequest, TResponse, TValue, TError>);
    }

    public static void MediatorPut<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route) where TRequest : IRequest<TResponse>
    {
        var group = app.MapGroup(endpointGroup);

        group.MapPut(route, EndpointWithoutParametersAttribute<TRequest, TResponse>);
    }

    internal static async Task<IResult> EndpointWithParametersAttribute<TRequest, TResponse, TValue, TError>
        ([AsParameters] TRequest request, 
        ISender sender, CancellationToken cancellationToken) 
        where TRequest : IRequest<TResponse>
        where TResponse : Result<TValue, TError>
        where TError : Error
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            suc => Results.Ok(suc),
            failure => Results.NotFound(failure)
            );
    }

    internal static async Task<IResult> EndpointWithoutParametersAttribute<TRequest, TResponse, TValue, TError>
        (TRequest request, 
        IMediator mediator, CancellationToken cancellationToken) 
        where TRequest : IRequest<TResponse>
        where TResponse : Result<TValue, TError>
        where TError : Error
        where TValue : CreateResponse
    {
        var response = await mediator.Send(request, cancellationToken);

        

        return response.Match(
            suc => Results.CreatedAtRoute(ResponseRoute.GetRouteName(typeof(TValue)), 
            new { response.Value.Id }, suc),
            failure => Results.BadRequest(failure)
            );
    }

    internal static async Task<IResult> EndpointWithoutParametersAttribute<TRequest, TResponse>
       (TRequest request,
       IMediator mediator, CancellationToken cancellationToken)
       where TRequest : IRequest<TResponse>
    {
        var response = await mediator.Send(request, cancellationToken);

        return Results.Ok(response);
    }
}
