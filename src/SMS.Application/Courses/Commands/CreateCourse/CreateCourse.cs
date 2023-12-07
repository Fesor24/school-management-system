using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Domain.Abstractions;
using SMS.Domain.DomainEvents;
using SMS.Domain.Entities;

namespace SMS.Application.Courses.Commands.CreateCourse;
public record CreateCourseCommand(string Name, string Code, int Unit) : IRequest<Guid>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    internal CreateCourseCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher, ILogger<CreateCourseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var result = Course.Create(
            Guid.NewGuid(),
            request.Name,
            request.Code,
            request.Unit);

        if (result.IsFailure)
        {
            _logger.LogError(result.Error);

            return default;
        }

        var course = result.Value;

        await _unitOfWork.CourseRepository.AddAsync(course);

        var res = await _unitOfWork.Complete(cancellationToken) > 0;

        if (res)
            await _publisher.Publish(new CourseCreatedEvent(course.Name, course.Code, course.Unit));

        return res ? course.Id : default;
    }
}
