using MediatR;
using SMS.Domain.Exceptions.Course;
using SMS.Domain.Primitives;

namespace SMS.Application.Courses.Commands.DeleteCourse;
internal record DeleteCourseCommand(Guid Id) : IRequest<Unit>;

internal sealed class DeleteCourseCommadHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommadHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.GetAsync(request.Id) ??
            throw new CourseNotFoundException(request.Id);

        _unitOfWork.CourseRepository.Delete(course);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
