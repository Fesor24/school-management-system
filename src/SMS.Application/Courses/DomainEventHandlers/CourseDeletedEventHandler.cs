using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Domain.DomainEvents.Course;

namespace SMS.Application.Courses.DomainEventHandlers;
internal sealed class CourseDeletedEventHandler : INotificationHandler<CourseDeletedEvent>
{
    private readonly ILogger<CourseDeletedEventHandler> _logger;

    public CourseDeletedEventHandler(ILogger<CourseDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CourseDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Course with Id: {notification.CourseId} has been removed from database");

        return Task.CompletedTask;
    }
}
