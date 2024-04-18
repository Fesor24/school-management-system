using MediatR;
using SMS.API.Common.EndpointRouteMapper;
using SMS.Application.Common.Response;
using SMS.Domain.Shared;

namespace SMS.API.Extensions;
internal static class MediatrExtensions
{
    internal static void MediatorGet<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route, string routeName) 
        where TRequest: IRequest<Result<TResponse, Error>>
    {
        route = "api/" + endpointGroup + route;

        app.MapGet(route, EndpointForGetRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup)
            .WithName(routeName);
    }

    internal static void MediatorPost<TRequest, TResponse>(this WebApplication app, string endpointGroup,
        string route) 
        where TRequest : IRequest<Result<TResponse, Error>>
        where TResponse : CreateResponse
    {
        route = "api/" + endpointGroup + route;

        app.MapPost(route, EndpointForCreateRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup);
    }

    internal static void MediatorPut<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route) where TRequest : IRequest<Result<TResponse, Error>>
    {
        route = "api/" + endpointGroup + route;

        app.MapPut(route, EndpointForUpdateRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup);
    }

    internal static void MediatorDelete<TRequest, TResponse>(this WebApplication app, string endpointGroup,
        string route) where TRequest : IRequest<Result<TResponse, Error>>
    {
        route = "api/" + endpointGroup + route;

        app.MapDelete(route, EndpointForDeleteRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup);
    }

    private static async Task<IResult> EndpointForGetRequests<TRequest, TResponse>
        ([AsParameters] TRequest request, 
        ISender sender, CancellationToken cancellationToken) 
        where TRequest : IRequest<Result<TResponse, Error>>
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            suc => Results.Ok(suc),
            HandleError
            );
    }

    private static async Task<IResult> EndpointForCreateRequests<TRequest, TResponse>
        (TRequest request, 
        IMediator mediator, CancellationToken cancellationToken) 
        where TRequest : IRequest<Result<TResponse, Error>>
        where TResponse : CreateResponse
    {
        var response = await mediator.Send(request, cancellationToken);

        string routeName = EndpointRoutes.GetRouteName(typeof(TResponse));

        return response.Match(
            suc => Results.CreatedAtRoute(EndpointRoutes.GetRouteName(typeof(TResponse)), 
            new { response.Value.Id }, suc),
            HandleError
            );
    }

    private static async Task<IResult> EndpointForUpdateRequests<TRequest, TResponse>
       (TRequest request,
       ISender sender, CancellationToken cancellationToken)
       where TRequest : IRequest<Result<TResponse, Error>>
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            suc => Results.NoContent(),
            HandleError
            );
    }

    private static async Task<IResult> EndpointForDeleteRequests<TRequest, TResponse>(
        [AsParameters]TRequest request, ISender sender, CancellationToken cancellationToken
        ) where TRequest : IRequest<Result<TResponse, Error>>
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            suc => Results.NoContent(),
            HandleError
            );
    }

    private static IResult HandleError(Error error)
    {
        Type errorType = error.GetType();

        if (errorType == typeof(NotFoundError))
            return Results.NotFound(error);

        else if (errorType == typeof(ValidationError) || errorType == typeof(BadRequestError))
            return Results.BadRequest(error);

        else
            return Results.BadRequest(error);
    }
}
