using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Domain.DomainEvents;

namespace SMS.Application.Courses.Events;
internal class CourseCreatedEventHandler : INotificationHandler<CourseCreatedEvent>
{
    private readonly ILogger<CourseCreatedEventHandler> _logger;

    public CourseCreatedEventHandler(ILogger<CourseCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CourseCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"A course was created. Name: {notification.CourseName}, Unit: {notification.Unit}, " +
            $"Code: {notification.CourseCode}");

        _logger.LogInformation($"A course was created. Name: {notification.CourseName}, Unit: {notification.Unit}, " +
            $"Code: {notification.CourseCode}");

        return Task.CompletedTask;
    }
}
