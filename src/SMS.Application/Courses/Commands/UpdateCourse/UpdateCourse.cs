using MediatR;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Commands.UpdateCourse;
public record UpdateCourseCommand(Guid CourseId, string Name, string Code, int Unit) : IRequest<Result<Unit, Error>>;

internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.DepartmentRepository.GetCourseAsync(request.CourseId, cancellationToken);

        if (course is null) return DomainErrors.Course.CourseNotFound(request.CourseId);

        var courseByCode = await _unitOfWork.DepartmentRepository.GetCourseByCodeAsync(request.Code, cancellationToken);

        if (courseByCode is not null) return new Error(StatusCodes.BADREQUEST, 
            $"Course with code: {request.Code} exist");

        var result = course.Update(request.Name, request.Code, request.Unit);

        if (result.IsFailure) return result.Error;

        _unitOfWork.DepartmentRepository.UpdateCourse(result.Value);

        await _unitOfWork.Complete(cancellationToken);

        return Unit.Value;
    }
}
