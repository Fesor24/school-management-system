using MediatR;
using SMS.API.Common.EndpointRouteMapper;
using SMS.Application.Common.Response;
using SMS.Domain.Shared;

namespace SMS.API.Extensions;
public static class MediatrExtensions
{
    public static void MediatorGet<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route, string routeName = "") 
        where TRequest: IRequest<Result<TResponse, Error>>
    {
        var group = app.MapGroup(endpointGroup)
            .WithName(routeName);

        group.MapGet(route, EndpointForGetRequests<TRequest, TResponse>);
    }

    public static void MediatorPost<TRequest, TResponse>(this WebApplication app, string endpointGroup,
        string route) 
        where TRequest : IRequest<Result<TResponse, Error>>
        where TResponse : CreateResponse
    {
        var group = app.MapGroup(endpointGroup);

        group.MapPost(route, EndpointForCreateRequests<TRequest, TResponse>);
    }

    public static void MediatorPut<TRequest, TResponse>(this WebApplication app, string endpointGroup, 
        string route) where TRequest : IRequest<Result<TResponse, Error>>
    {
        var group = app.MapGroup(endpointGroup);

        group.MapPut(route, EndpointForUpdateRequests<TRequest, TResponse>);
    }

    public static void MediatorDelete<TRequest, TResponse>(this WebApplication app, string endpointGroup,
        string route) where TRequest : IRequest<Result<TResponse, Error>>
    {
        var group = app.MapGroup(endpointGroup);

        group.MapDelete(route, EndpointForDeleteRequests<TRequest, TResponse>);
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
        TRequest request, ISender sender, CancellationToken cancellationToken
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
        return error.Code switch
        {
            Domain.Errors.StatusCodes.NOTFOUND => Results.NotFound(error),
            Domain.Errors.StatusCodes.BADREQUEST => Results.BadRequest(error),
            _ => Results.BadRequest()
        };
    }
}
