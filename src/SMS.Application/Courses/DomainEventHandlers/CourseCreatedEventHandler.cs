using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Domain.DomainEvents.Course;

namespace SMS.Application.Courses.DomainEventHandlers;
internal class CourseCreatedEventHandler : INotificationHandler<CourseCreatedEvent>
{
    private readonly ILogger<CourseCreatedEventHandler> _logger;

    public CourseCreatedEventHandler(ILogger<CourseCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CourseCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"A course was created. Name: {notification.CourseName}, Unit: {notification.Unit}, " +
            $"Code: {notification.CourseCode}");

        return Task.CompletedTask;
    }
}
