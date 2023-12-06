using MediatR;
using SMS.Domain.Abstractions;
using SMS.Domain.Entities;

namespace SMS.Application.Courses.Commands.CreateCourse;
internal record CreateCourseCommand(string Name, string Code, int Unit) : IRequest<Guid>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    internal CreateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var result = Course.Create(
            Guid.NewGuid(),
            request.Name,
            request.Code,
            request.Unit);

        //if (result.IsFailure) return result.Error;

        var course = result.Value;

        await _unitOfWork.CourseRepository.AddAsync(course);

        return await _unitOfWork.Complete() > 0 ? course.Id : default;
    }
}
