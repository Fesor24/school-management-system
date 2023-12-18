using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.StudentCourse.Commands.CreateStudentCourse;
public record CreateStudentCourseCommand(Guid CourseId, Guid StudentId) : IRequest<Result<Unit, Error>>;

internal sealed class CreateStudentCourseCommandHandler : IRequestHandler<CreateStudentCourseCommand, Result<Unit, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateStudentCourseCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Unit, Error>> Handle(CreateStudentCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _departmentRepository.GetCourseAsync(request.CourseId);

        if (course is null) return DomainErrors.Course.CourseNotFound(request.CourseId);

        return Unit.Value;
    }
}