using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Commands.UpdateCourse;
public record UpdateCourseCommand(Guid CourseId, string Name, string Code, int Unit) : IRequest<Result<Unit, Error>>;

internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<Unit, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateCourseCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _departmentRepository.GetCourseAsync(request.CourseId, cancellationToken);

        if (course is null) return DomainErrors.Course.CourseNotFound(request.CourseId);

        var courseByCode = await _departmentRepository.GetCourseByCodeAsync(request.Code, cancellationToken);

        if (courseByCode is not null) return Error.BadRequest("COURSE.EXIST", 
            $"Course with code: {request.Code} exist");

        var result = course.Update(request.Name, request.Code, request.Unit);

        if (result.IsFailure) return result.Error;

        _departmentRepository.UpdateCourse(result.Value);

        await _departmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Unit.Value;
    }
}
