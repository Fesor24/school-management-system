using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Domain.DomainEvents.User;

namespace SMS.Application.User.EventHandlers;
internal class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"A user was created. Name: {notification.FirstName + notification.LastName}" +
            $"Email: {notification.EmailAddress}");

        // An email will be sent to the user...

        return Task.CompletedTask;
    }
}
