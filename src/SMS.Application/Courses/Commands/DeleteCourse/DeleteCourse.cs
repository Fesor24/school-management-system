using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Exceptions.Course;
using SMS.Domain.Exceptions.Department;

namespace SMS.Application.Courses.Commands.DeleteCourse;
internal record DeleteCourseCommand(Guid CourseId, Guid DepartmentId) : IRequest<Unit>;

internal sealed class DeleteCourseCommadHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteCourseCommadHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentInfo(request.DepartmentId) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        var result = department.RemoveCourse(request.CourseId);

        if (result.IsFailure)
            throw new CourseNotFoundException(request.CourseId);

        _departmentRepository.Update(department);

        await _departmentRepository.UnitOfWork.SaveEntitiesAsync();

        return Unit.Value;
    }
}
