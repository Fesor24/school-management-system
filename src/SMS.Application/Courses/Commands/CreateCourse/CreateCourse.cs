using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Exceptions.Course;
using SMS.Domain.Exceptions.Department;
using SMS.Domain.Primitives;

namespace SMS.Application.Courses.Commands.CreateCourse;
public record CreateCourseCommand(string Name, string Code, int Unit, Guid DepartmentId) : IRequest<Unit>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.DepartmentId, cancellationToken) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        var result = department.AddCourse(request.Name, request.Code, request.Unit);

        if (result.IsFailure && result.Error.Equals(DomainErrors.Course.InvalidCourseUnit))
            throw new CourseInvalidUnitException(result.Error.Message);

        Course course = result.Value;

        _unitOfWork.DepartmentRepository.Update(department);

        return Unit.Value;
    }
}
